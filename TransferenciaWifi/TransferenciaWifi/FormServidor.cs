using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransferenciaWifi
{
    // ------------------------------------------------------------------
    //  APP DE LA PC: SERVIDOR DE TRANSFERENCIA DE ARCHIVOS
    //
    //  Protocolo de archivos (TCP, puerto 5000), igual en ambas direcciones:
    //    [4 bytes]  largo del nombre del archivo (entero, orden de red)
    //    [N bytes]  nombre del archivo en UTF-8
    //    [8 bytes]  tamaño del archivo en bytes (entero largo, orden de red)
    //    [...]      los bytes del archivo
    //
    //  Descubrimiento automático (UDP, puerto 5001):
    //    Mientras el servidor está activo y nadie está conectado, la PC
    //    "grita" cada segundo a toda la red:  TRANSFERWIFI|NOMBRE-PC|5000
    //    El celular escucha ese mensaje y así sabe la IP sin escribirla.
    // ------------------------------------------------------------------
    public class FormServidor : Form
    {
        // ===== Configuración =====
        private const int PUERTO = 5000;                  // transferencia de archivos (TCP)
        private const int PUERTO_DESCUBRIMIENTO = 5001;   // anuncios "aquí estoy" (UDP)
        private const int TAM_BLOQUE = 1024 * 1024;       // bloques de 1 MB
        private const string NOMBRE_REGLA_FIREWALL = "TransferenciaWifi";

        // ===== Controles de la ventana =====
        private Label lblEstado, lblIPs, lblCarpeta, lblArchivo, lblVelocidad;
        private Button btnIniciar, btnCarpeta, btnEnviar, btnPrueba;
        private ProgressBar barra;
        private TextBox txtLog;
        private System.Windows.Forms.Timer timerAnuncio;

        // ===== Red =====
        private TcpListener servidor;
        private TcpClient clienteActual;
        private NetworkStream flujoActual;
        private bool escuchando;
        private readonly SemaphoreSlim candadoEnvio = new SemaphoreSlim(1, 1);

        private string carpetaDestino;

        // Una red "útil" de la PC (no virtual)
        private class RedLocal
        {
            public string Nombre;
            public bool EsWifi;
            public IPAddress Ip;
            public IPAddress Mascara;
        }

        public FormServidor()
        {
            Text = "Transferencia Wi-Fi - PC";
            ClientSize = new Size(640, 520);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9.5f);

            carpetaDestino = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads", "Recibidos Wi-Fi");
            Directory.CreateDirectory(carpetaDestino);

            CrearControles();
            ActualizarCarpeta();
            MostrarIPs();

            // Anuncio "aquí estoy" cada segundo
            timerAnuncio = new System.Windows.Forms.Timer { Interval = 1000 };
            timerAnuncio.Tick += (s, e) => Anunciar();

            NetworkChange.NetworkAddressChanged += (s, e) =>
            {
                if (IsHandleCreated && !IsDisposed)
                    BeginInvoke(new Action(MostrarIPs));
            };

            FormClosing += (s, e) => DetenerServidor();
        }

        // ==================================================================
        //  INTERFAZ
        // ==================================================================
        private void CrearControles()
        {
            lblEstado = new Label
            {
                Location = new Point(15, 15),
                Size = new Size(610, 24),
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                Text = "Servidor detenido",
                ForeColor = Color.Firebrick
            };
            lblIPs = new Label { Location = new Point(15, 42), Size = new Size(610, 40) };

            btnIniciar = new Button { Location = new Point(15, 88), Size = new Size(150, 32), Text = "Iniciar servidor" };
            btnCarpeta = new Button { Location = new Point(175, 88), Size = new Size(150, 32), Text = "Cambiar carpeta..." };
            btnEnviar = new Button { Location = new Point(335, 88), Size = new Size(140, 32), Text = "Enviar al celular...", Enabled = false };
            btnPrueba = new Button { Location = new Point(485, 88), Size = new Size(140, 32), Text = "Prueba local...", Enabled = false };

            lblCarpeta = new Label
            {
                Location = new Point(15, 128),
                Size = new Size(610, 20),
                AutoEllipsis = true,
                ForeColor = Color.RoyalBlue,
                Cursor = Cursors.Hand
            };
            lblArchivo = new Label { Location = new Point(15, 155), Size = new Size(610, 20), AutoEllipsis = true, Text = "Sin transferencias" };
            barra = new ProgressBar { Location = new Point(15, 178), Size = new Size(610, 22), Maximum = 1000 };
            lblVelocidad = new Label { Location = new Point(15, 205), Size = new Size(610, 20) };

            txtLog = new TextBox
            {
                Location = new Point(15, 232),
                Size = new Size(610, 275),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.White
            };

            Controls.AddRange(new Control[]
            {
                lblEstado, lblIPs, btnIniciar, btnCarpeta, btnEnviar, btnPrueba,
                lblCarpeta, lblArchivo, barra, lblVelocidad, txtLog
            });

            btnIniciar.Click += BtnIniciar_Click;
            btnCarpeta.Click += BtnCarpeta_Click;
            btnEnviar.Click += BtnEnviar_Click;
            btnPrueba.Click += BtnPrueba_Click;
            lblCarpeta.Click += (s, e) => Process.Start("explorer.exe", carpetaDestino);
        }

        private void ActualizarCarpeta()
        {
            lblCarpeta.Text = "Carpeta de recibidos (clic para abrir): " + carpetaDestino;
        }

        // Muestra la IP útil (Wi-Fi primero, sin adaptadores virtuales)
        private void MostrarIPs()
        {
            var wifi = new StringBuilder();
            var otras = new StringBuilder();

            foreach (RedLocal red in ObtenerRedesUtiles())
            {
                StringBuilder destino = red.EsWifi ? wifi : otras;
                if (destino.Length > 0) destino.Append("   |   ");
                destino.Append(red.Nombre + ": " + red.Ip);
            }

            if (wifi.Length > 0)
            {
                lblIPs.Text = "IP de esta PC  ->  " + wifi +
                              (otras.Length > 0 ? "\r\nOtras redes: " + otras : "");
                lblIPs.ForeColor = Color.ForestGreen;
            }
            else if (otras.Length > 0)
            {
                lblIPs.Text = "No hay Wi-Fi conectado. Otras redes: " + otras;
                lblIPs.ForeColor = Color.DarkOrange;
            }
            else
            {
                lblIPs.Text = "No hay red conectada. Conecta la laptop al hotspot del celular.";
                lblIPs.ForeColor = Color.Firebrick;
            }
        }

        private void Log(string mensaje)
        {
            if (IsDisposed) return;
            txtLog.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + mensaje + "\r\n");
        }

        private void MostrarProgreso(string accion, string nombre, long hecho, long total, Stopwatch reloj)
        {
            double segundos = Math.Max(reloj.Elapsed.TotalSeconds, 0.001);
            double mbPorSeg = hecho / 1048576.0 / segundos;

            barra.Value = total == 0 ? 1000 : (int)(hecho * 1000 / total);
            lblArchivo.Text = accion + ": " + nombre;
            lblVelocidad.Text = Tam(hecho) + " de " + Tam(total) + "   -   " + mbPorSeg.ToString("0.0") + " MB/s";
        }

        // ==================================================================
        //  BOTONES
        // ==================================================================
        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            if (escuchando) DetenerServidor();
            else IniciarServidor();
        }

        private void BtnCarpeta_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Elige dónde guardar los archivos recibidos";
                dlg.SelectedPath = carpetaDestino;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    carpetaDestino = dlg.SelectedPath;
                    ActualizarCarpeta();
                    Log("Carpeta cambiada a: " + carpetaDestino);
                }
            }
        }

        private async void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (flujoActual == null) return;
            string[] archivos = ElegirArchivos();
            if (archivos == null) return;

            btnEnviar.Enabled = false;
            try
            {
                foreach (string ruta in archivos)
                    await EnviarArchivoAsync(flujoActual, ruta, true);
            }
            catch (Exception ex)
            {
                Log("Error al enviar: " + ex.Message);
            }
            finally
            {
                btnEnviar.Enabled = flujoActual != null;
            }
        }

        private async void BtnPrueba_Click(object sender, EventArgs e)
        {
            string[] archivos = ElegirArchivos();
            if (archivos == null) return;

            btnPrueba.Enabled = false;
            try
            {
                using (var cliente = new TcpClient())
                {
                    await cliente.ConnectAsync(IPAddress.Loopback, PUERTO);
                    cliente.SendBufferSize = TAM_BLOQUE;
                    NetworkStream flujo = cliente.GetStream();

                    foreach (string ruta in archivos)
                        await EnviarArchivoAsync(flujo, ruta, false);
                }
            }
            catch (Exception ex)
            {
                Log("La prueba local falló: " + ex.Message);
                btnPrueba.Enabled = escuchando && clienteActual == null;
            }
        }

        private string[] ElegirArchivos()
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Elige uno o varios archivos";
                dlg.Multiselect = true;
                return dlg.ShowDialog() == DialogResult.OK ? dlg.FileNames : null;
            }
        }

        // ==================================================================
        //  FIREWALL
        //  Revisa si existe la regla que deja entrar conexiones al puerto 5000.
        //  Si no existe, ofrece crearla (Windows pide permiso de administrador).
        // ==================================================================
        private void RevisarFirewall()
        {
            if (ExisteReglaFirewall()) return;

            DialogResult r = MessageBox.Show(
                "Para que el celular pueda conectarse, el Firewall de Windows necesita " +
                "una regla que permita el puerto " + PUERTO + ".\r\n\r\n" +
                "¿Crearla ahora? Windows pedirá permiso de administrador una sola vez.",
                "Firewall", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r != DialogResult.Yes)
            {
                Log("Aviso: sin la regla del Firewall el celular no podrá conectarse.");
                return;
            }

            try
            {
                var psi = new ProcessStartInfo("netsh",
                    "advfirewall firewall add rule name=\"" + NOMBRE_REGLA_FIREWALL + "\" " +
                    "dir=in action=allow protocol=TCP localport=" + PUERTO + " profile=any")
                {
                    UseShellExecute = true,
                    Verb = "runas",                       // ejecutar como administrador
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                using (Process p = Process.Start(psi)) p.WaitForExit();

                Log(ExisteReglaFirewall()
                    ? "Regla del Firewall creada."
                    : "No se pudo confirmar la regla del Firewall.");
            }
            catch (Exception ex)
            {
                Log("No se creó la regla del Firewall: " + ex.Message);
            }
        }

        private static bool ExisteReglaFirewall()
        {
            try
            {
                var psi = new ProcessStartInfo("netsh",
                    "advfirewall firewall show rule name=\"" + NOMBRE_REGLA_FIREWALL + "\"")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                };
                using (Process p = Process.Start(psi))
                {
                    p.StandardOutput.ReadToEnd();
                    p.WaitForExit();
                    return p.ExitCode == 0;   // 0 = la regla existe
                }
            }
            catch
            {
                return true; // si no se puede comprobar, no molestar al usuario
            }
        }

        // ==================================================================
        //  DESCUBRIMIENTO AUTOMÁTICO (UDP broadcast)
        // ==================================================================
        private void Anunciar()
        {
            // Solo se anuncia si nadie está conectado
            if (!escuchando || clienteActual != null) return;

            byte[] mensaje = Encoding.UTF8.GetBytes(
                "TRANSFERWIFI|" + Environment.MachineName + "|" + PUERTO);

            // Se envía por cada red útil, a su dirección de "todos" (broadcast)
            foreach (RedLocal red in ObtenerRedesUtiles())
            {
                try
                {
                    using (var udp = new UdpClient(new IPEndPoint(red.Ip, 0)))
                    {
                        udp.EnableBroadcast = true;
                        IPAddress destino = DireccionBroadcast(red.Ip, red.Mascara);
                        udp.Send(mensaje, mensaje.Length, new IPEndPoint(destino, PUERTO_DESCUBRIMIENTO));
                    }
                }
                catch
                {
                    // Una red que falle no debe detener a las demás
                }
            }
        }

        // Ejemplo: IP 10.25.131.96 con máscara 255.255.255.0  ->  10.25.131.255
        private static IPAddress DireccionBroadcast(IPAddress ip, IPAddress mascara)
        {
            if (mascara == null || mascara.Equals(IPAddress.Any)) return IPAddress.Broadcast;

            byte[] b = ip.GetAddressBytes();
            byte[] m = mascara.GetAddressBytes();
            for (int i = 0; i < 4; i++)
                b[i] = (byte)(b[i] | ~m[i]);
            return new IPAddress(b);
        }

        // ==================================================================
        //  SERVIDOR
        // ==================================================================
        private void IniciarServidor()
        {
            RevisarFirewall();

            try
            {
                servidor = new TcpListener(IPAddress.Any, PUERTO);
                servidor.Start();
                escuchando = true;

                btnIniciar.Text = "Detener servidor";
                btnPrueba.Enabled = true;
                MostrarEsperando();
                MostrarIPs();
                timerAnuncio.Start();
                Log("Servidor iniciado. La PC se anuncia en la red para que el celular la encuentre solo.");

                _ = BucleAceptarAsync();
            }
            catch (SocketException ex)
            {
                Log("No se pudo iniciar el servidor: " + ex.Message);
            }
        }

        private void DetenerServidor()
        {
            escuchando = false;
            if (timerAnuncio != null) timerAnuncio.Stop();
            try { if (servidor != null) servidor.Stop(); } catch { }
            try { if (clienteActual != null) clienteActual.Close(); } catch { }

            if (IsDisposed) return;
            btnIniciar.Text = "Iniciar servidor";
            btnEnviar.Enabled = false;
            btnPrueba.Enabled = false;
            lblEstado.Text = "Servidor detenido";
            lblEstado.ForeColor = Color.Firebrick;
        }

        private void MostrarEsperando()
        {
            lblEstado.Text = "Esperando al celular (te encontrará solo)...";
            lblEstado.ForeColor = Color.DarkOrange;
        }

        private async Task BucleAceptarAsync()
        {
            while (escuchando)
            {
                TcpClient cliente;
                try
                {
                    cliente = await servidor.AcceptTcpClientAsync();
                }
                catch
                {
                    break;
                }

                await AtenderClienteAsync(cliente);
            }
        }

        private async Task AtenderClienteAsync(TcpClient cliente)
        {
            clienteActual = cliente;
            cliente.NoDelay = true;
            cliente.ReceiveBufferSize = TAM_BLOQUE;
            cliente.SendBufferSize = TAM_BLOQUE;
            flujoActual = cliente.GetStream();

            string quien = cliente.Client.RemoteEndPoint.ToString();
            lblEstado.Text = "Conectado con " + quien;
            lblEstado.ForeColor = Color.ForestGreen;
            btnEnviar.Enabled = true;
            btnPrueba.Enabled = false;
            Log("Dispositivo conectado: " + quien);

            try
            {
                while (await RecibirArchivoAsync(flujoActual)) { }
            }
            catch (Exception ex)
            {
                Log("Conexión interrumpida: " + ex.Message);
            }
            finally
            {
                try { cliente.Close(); } catch { }
                clienteActual = null;
                flujoActual = null;

                if (!IsDisposed)
                {
                    btnEnviar.Enabled = false;
                    btnPrueba.Enabled = escuchando;
                    if (escuchando) MostrarEsperando();
                    Log("Dispositivo desconectado.");
                }
            }
        }

        // ==================================================================
        //  RECIBIR UN ARCHIVO
        // ==================================================================
        private async Task<bool> RecibirArchivoAsync(NetworkStream flujo)
        {
            byte[] b4 = new byte[4];
            if (!await LeerExactoAsync(flujo, b4, 4, true)) return false;
            int largoNombre = LeerInt32(b4);
            if (largoNombre <= 0 || largoNombre > 4096)
                throw new InvalidDataException("Encabezado inválido (largo de nombre: " + largoNombre + ").");

            byte[] bNombre = new byte[largoNombre];
            await LeerExactoAsync(flujo, bNombre, largoNombre, false);
            string nombre = LimpiarNombre(Encoding.UTF8.GetString(bNombre));

            byte[] b8 = new byte[8];
            await LeerExactoAsync(flujo, b8, 8, false);
            long tamano = LeerInt64(b8);
            if (tamano < 0) throw new InvalidDataException("Tamaño de archivo inválido.");

            Directory.CreateDirectory(carpetaDestino);
            string rutaFinal = RutaSinRepetir(Path.Combine(carpetaDestino, nombre));
            string rutaTemp = rutaFinal + ".parcial";
            Log("Recibiendo: " + nombre + " (" + Tam(tamano) + ")");

            byte[] buffer = new byte[TAM_BLOQUE];
            long recibido = 0;
            long ultimaActualizacion = -1000;
            Stopwatch reloj = Stopwatch.StartNew();

            try
            {
                using (var archivo = new FileStream(rutaTemp, FileMode.Create, FileAccess.Write,
                                                    FileShare.None, TAM_BLOQUE, true))
                {
                    while (recibido < tamano)
                    {
                        int pedir = (int)Math.Min(buffer.Length, tamano - recibido);
                        int leidos = await flujo.ReadAsync(buffer, 0, pedir);
                        if (leidos == 0)
                            throw new IOException("El otro dispositivo se desconectó a mitad del archivo.");

                        await archivo.WriteAsync(buffer, 0, leidos);
                        recibido += leidos;

                        if (reloj.ElapsedMilliseconds - ultimaActualizacion > 150)
                        {
                            MostrarProgreso("Recibiendo", nombre, recibido, tamano, reloj);
                            ultimaActualizacion = reloj.ElapsedMilliseconds;
                        }
                    }
                }
            }
            catch
            {
                try { File.Delete(rutaTemp); } catch { }
                throw;
            }

            File.Move(rutaTemp, rutaFinal);
            reloj.Stop();
            MostrarProgreso("Recibido", nombre, recibido, tamano, reloj);
            Log("✓ Recibido: " + Path.GetFileName(rutaFinal) + " en " +
                reloj.Elapsed.TotalSeconds.ToString("0.0") + " s (" + Velocidad(tamano, reloj) + ")");
            return true;
        }

        // ==================================================================
        //  ENVIAR UN ARCHIVO
        // ==================================================================
        private async Task EnviarArchivoAsync(Stream flujo, string ruta, bool mostrarProgreso)
        {
            await candadoEnvio.WaitAsync();
            try
            {
                FileInfo info = new FileInfo(ruta);
                byte[] bNombre = Encoding.UTF8.GetBytes(info.Name);

                byte[] cabecera = new byte[4 + bNombre.Length + 8];
                EscribirInt32(cabecera, 0, bNombre.Length);
                Buffer.BlockCopy(bNombre, 0, cabecera, 4, bNombre.Length);
                EscribirInt64(cabecera, 4 + bNombre.Length, info.Length);
                await flujo.WriteAsync(cabecera, 0, cabecera.Length);

                if (mostrarProgreso) Log("Enviando: " + info.Name + " (" + Tam(info.Length) + ")");

                byte[] buffer = new byte[TAM_BLOQUE];
                long enviado = 0;
                long ultimaActualizacion = -1000;
                Stopwatch reloj = Stopwatch.StartNew();

                using (var archivo = new FileStream(ruta, FileMode.Open, FileAccess.Read,
                                                    FileShare.Read, TAM_BLOQUE, true))
                {
                    int leidos;
                    while ((leidos = await archivo.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await flujo.WriteAsync(buffer, 0, leidos);
                        enviado += leidos;

                        if (mostrarProgreso && reloj.ElapsedMilliseconds - ultimaActualizacion > 150)
                        {
                            MostrarProgreso("Enviando", info.Name, enviado, info.Length, reloj);
                            ultimaActualizacion = reloj.ElapsedMilliseconds;
                        }
                    }
                }

                await flujo.FlushAsync();
                reloj.Stop();

                if (mostrarProgreso)
                {
                    MostrarProgreso("Enviado", info.Name, enviado, info.Length, reloj);
                    Log("✓ Enviado: " + info.Name + " en " +
                        reloj.Elapsed.TotalSeconds.ToString("0.0") + " s (" + Velocidad(enviado, reloj) + ")");
                }
            }
            finally
            {
                candadoEnvio.Release();
            }
        }

        // ==================================================================
        //  AYUDANTES DE RED
        // ==================================================================

        // Redes reales de la PC (sin WSL, Hyper-V, VPN, etc.)
        private static List<RedLocal> ObtenerRedesUtiles()
        {
            var lista = new List<RedLocal>();

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus != OperationalStatus.Up) continue;
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;
                if (EsAdaptadorVirtual(ni)) continue;

                bool esWifi = ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211;

                foreach (UnicastIPAddressInformation ua in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ua.Address.AddressFamily != AddressFamily.InterNetwork) continue; // solo IPv4
                    if (ua.Address.ToString().StartsWith("169.254.")) continue;          // IP "sin red"

                    lista.Add(new RedLocal
                    {
                        Nombre = ni.Name,
                        EsWifi = esWifi,
                        Ip = ua.Address,
                        Mascara = ua.IPv4Mask
                    });
                }
            }
            return lista;
        }

        private static bool EsAdaptadorVirtual(NetworkInterface ni)
        {
            string texto = (ni.Name + " " + ni.Description).ToLowerInvariant();
            string[] virtuales =
            {
                "vethernet", "hyper-v", "wsl", "virtual", "vmware", "virtualbox",
                "vpn", "tap-", "tunnel", "bluetooth", "loopback", "docker"
            };
            foreach (string v in virtuales)
                if (texto.Contains(v)) return true;
            return false;
        }

        // Lee exactamente "cantidad" bytes (la red puede entregarlos en pedazos)
        private static async Task<bool> LeerExactoAsync(Stream flujo, byte[] buffer, int cantidad, bool permitirFinLimpio)
        {
            int leidos = 0;
            while (leidos < cantidad)
            {
                int n = await flujo.ReadAsync(buffer, leidos, cantidad - leidos);
                if (n == 0)
                {
                    if (leidos == 0 && permitirFinLimpio) return false;
                    throw new IOException("La conexión se cerró inesperadamente.");
                }
                leidos += n;
            }
            return true;
        }

        // ==================================================================
        //  AYUDANTES GENERALES
        // ==================================================================
        private static void EscribirInt32(byte[] b, int pos, int valor)
        {
            b[pos] = (byte)(valor >> 24);
            b[pos + 1] = (byte)(valor >> 16);
            b[pos + 2] = (byte)(valor >> 8);
            b[pos + 3] = (byte)valor;
        }

        private static void EscribirInt64(byte[] b, int pos, long valor)
        {
            for (int i = 7; i >= 0; i--)
            {
                b[pos + i] = (byte)valor;
                valor >>= 8;
            }
        }

        private static int LeerInt32(byte[] b)
        {
            return (b[0] << 24) | (b[1] << 16) | (b[2] << 8) | b[3];
        }

        private static long LeerInt64(byte[] b)
        {
            long valor = 0;
            for (int i = 0; i < 8; i++)
                valor = (valor << 8) | b[i];
            return valor;
        }

        private static string LimpiarNombre(string nombre)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                nombre = nombre.Replace(c, '_');
            nombre = nombre.Trim();
            if (nombre.Length == 0 || nombre == "." || nombre == "..") nombre = "archivo";
            return nombre;
        }

        private static string RutaSinRepetir(string ruta)
        {
            string carpeta = Path.GetDirectoryName(ruta);
            string baseNombre = Path.GetFileNameWithoutExtension(ruta);
            string extension = Path.GetExtension(ruta);

            string candidata = ruta;
            int i = 1;
            while (File.Exists(candidata) || File.Exists(candidata + ".parcial"))
            {
                candidata = Path.Combine(carpeta, baseNombre + " (" + i + ")" + extension);
                i++;
            }
            return candidata;
        }

        private static string Tam(long bytes)
        {
            if (bytes >= 1L << 30) return (bytes / 1073741824.0).ToString("0.00") + " GB";
            if (bytes >= 1L << 20) return (bytes / 1048576.0).ToString("0.0") + " MB";
            if (bytes >= 1024) return (bytes / 1024.0).ToString("0") + " KB";
            return bytes + " B";
        }

        private static string Velocidad(long bytes, Stopwatch reloj)
        {
            double segundos = Math.Max(reloj.Elapsed.TotalSeconds, 0.001);
            return (bytes / 1048576.0 / segundos).ToString("0.0") + " MB/s";
        }
    }
}