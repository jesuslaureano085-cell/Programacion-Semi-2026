// =====================================================================
// Monitor — Pantalla de arranque (dura entre 12 y 14 segundos)
// Fase 1: texto tipo BIOS escribiéndose (con datos reales) + ENTER simulado
// Fase 2: el logotipo MONITOR se traza solo, la O-pantalla late y un
//         destello de luz cruza el logo
// Fase 3: la pantalla se deshace en una ola de caracteres de código que
//         revela el escritorio, mientras el logo vuela al botón Inicio
// Solo se muestra al abrir el sitio (una vez por sesión del navegador).
// Enter (o cualquier tecla / clic) salta directo a la ola de código.
// =====================================================================
(function () {
    'use strict';

    const capa = document.getElementById('arranque');
    if (!capa) return;

    const raiz = document.documentElement;
    const salida = document.getElementById('arranque-salida');
    const tecla = document.getElementById('arranque-tecla');
    const marca = document.getElementById('arranque-marca');
    const logotipo = marca.querySelector('.logotipo');
    const lema = document.getElementById('arranque-lema');
    const lienzo = document.getElementById('arranque-codigo');
    const CLAVE = 'mv-arranque-visto';

    let corriendo = false;
    let saltando = false;     // se presionó una tecla: terminar fases 1 y 2 al instante
    let enTransicion = false; // ya empezó la ola de código (no se puede saltar más)
    let revelado = false;

    function esperar(ms) {
        return new Promise((r) => setTimeout(r, saltando ? 0 : ms));
    }

    // Escribe texto letra por letra en el elemento indicado
    async function escribir(destino, texto, min, max) {
        const letras = Array.from(texto);
        for (let i = 0; i < letras.length; i++) {
            if (saltando) { destino.textContent += letras.slice(i).join(''); return; }
            destino.textContent += letras[i];
            if (max > 0) await esperar(min + Math.random() * (max - min));
        }
    }

    function puntos(etiqueta) { return '.'.repeat(Math.max(3, 30 - etiqueta.length)); }

    // "Etiqueta .......... VALOR", como en una BIOS
    async function lineaVerificacion(etiqueta, valor) {
        await escribir(salida, etiqueta + ' ', 9, 18);
        await escribir(salida, puntos(etiqueta), 1, 3);
        await esperar(200 + Math.random() * 180);
        await escribir(salida, ' ' + valor + '\n', 14, 26);
    }

    // La memoria "cuenta" hasta 640K, como en las computadoras viejas
    async function contarMemoria() {
        const etiqueta = 'Memoria del sistema';
        await escribir(salida, etiqueta + ' ', 9, 18);
        await escribir(salida, puntos(etiqueta) + ' ', 1, 3);
        const base = salida.textContent;
        for (let k = 64; k <= 640; k += 64) {
            if (saltando) break;
            salida.textContent = base + k + 'K';
            await esperar(85);
        }
        salida.textContent = base + '640K OK\n';
    }

    // ------------------------------------------------------------------
    // Fase 1: arranque tipo BIOS
    // ------------------------------------------------------------------
    async function fase1() {
        const d = capa.dataset;
        const fecha = new Date().toLocaleDateString('es-SV', { day: '2-digit', month: '2-digit', year: 'numeric' });

        await esperar(500);
        await escribir(salida, 'MONITOR BIOS v1.0                (C) 2026 Monitor\n', 6, 12);
        await escribir(salida, 'Fecha del sistema: ' + fecha + '\n\n', 6, 12);
        await esperar(400);

        await contarMemoria();
        await lineaVerificacion('Detectando teclado', 'OK');
        await lineaVerificacion('Conectando a base de datos', 'OK');
        await lineaVerificacion('Clientes registrados', d.clientes || '0');
        await lineaVerificacion('Productos en inventario', d.productos || '0');
        await lineaVerificacion('Ventas de hoy', d.ventas || '0');
        await lineaVerificacion('Productos con stock bajo', d.stock || '0');

        await esperar(450);
        await escribir(salida, '\nC:\\> ', 0, 0);
        await esperar(600);
        await escribir(salida, 'MONITOR.EXE', 80, 150);   // velocidad "humana"
        await esperar(350);

        // ENTER simulado
        tecla.classList.add('visible');
        await esperar(250);
        tecla.classList.add('pulsada');
        await esperar(220);
        tecla.classList.remove('pulsada');
        await esperar(200);
        tecla.classList.remove('visible');
    }

    // ------------------------------------------------------------------
    // Fase 2: el logotipo se traza y brilla
    // ------------------------------------------------------------------
    async function fase2() {
        salida.textContent = '';
        capa.classList.add('sin-cursor');
        await esperar(250);
        marca.classList.add('visible', 'marca-dibujando');   // CSS dibuja cada letra
        await esperar(1900);
        await escribir(lema, 'SISTEMA DE VENTAS', 40, 70);
        await esperar(300);
        marca.classList.add('marca-brillo');                 // latido de luz
        const destello = document.getElementById('brillo-anim');
        if (destello && destello.beginElement && !saltando) destello.beginElement();   // franja de brillo
        await esperar(2200);
    }

    // ------------------------------------------------------------------
    // Fase 3: ola de código que revela el escritorio
    // ------------------------------------------------------------------
    function revelar() {
        if (revelado) return;
        revelado = true;
        document.dispatchEvent(new CustomEvent('mv:arranque-revelar'));
    }

    // El logo se encoge y "aterriza" en el botón Inicio
    function volarAlInicio() {
        const destino = document.querySelector('#btn-inicio .logo-mini');
        const barra = document.getElementById('barra');
        if (!destino || !barra || !marca.classList.contains('visible')) return;
        const r = logotipo.getBoundingClientRect();
        const t = destino.getBoundingClientRect();
        // La barra todavía está escondida abajo; se calcula dónde quedará
        const ajuste = barra.getBoundingClientRect().top - (window.innerHeight - barra.offsetHeight);
        const escala = Math.max(t.width / r.width, 0.02);
        const dx = (t.left + t.width / 2) - (r.left + r.width / 2);
        const dy = (t.top - ajuste + t.height / 2) - (r.top + r.height / 2);
        logotipo.style.transition = 'transform .8s cubic-bezier(.6,0,.3,1), opacity .8s ease-in';
        logotipo.style.transform = 'translate(' + dx + 'px, ' + dy + 'px) scale(' + escala + ')';
        logotipo.style.opacity = '0.15';
        lema.style.opacity = '0';
    }

    function olaDeCodigo(duracion) {
        return new Promise((resolver) => {
            const dpr = Math.min(window.devicePixelRatio || 1, 2);
            const W = window.innerWidth;
            const H = window.innerHeight;
            lienzo.width = Math.round(W * dpr);
            lienzo.height = Math.round(H * dpr);
            lienzo.style.width = W + 'px';
            lienzo.style.height = H + 'px';
            lienzo.hidden = false;

            const ctx = lienzo.getContext('2d');
            ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
            const celda = 18;
            const cols = Math.ceil(W / celda);
            const filas = Math.ceil(H / celda);
            ctx.font = celda + 'px VT323, Consolas, monospace';
            ctx.textBaseline = 'top';

            // Cada celda se "apaga" según su distancia al centro (+ un poco de azar)
            const cx = W / 2, cy = H / 2;
            const maxD = Math.hypot(cx, cy);
            const tiempos = new Float32Array(cols * filas);
            for (let f = 0; f < filas; f++) {
                for (let c = 0; c < cols; c++) {
                    const d = Math.hypot(c * celda + celda / 2 - cx, f * celda + celda / 2 - cy);
                    tiempos[f * cols + c] = (d / maxD) * duracion * 0.8 + Math.random() * duracion * 0.2;
                }
            }

            const CARACTERES = '01ABCDEF{}[]<>/\\#$%&*=+;:';
            const inicio = performance.now();

            function cuadro(ahora) {
                const t = ahora - inicio;
                ctx.clearRect(0, 0, W, H);
                let quedan = false;
                for (let f = 0; f < filas; f++) {
                    for (let c = 0; c < cols; c++) {
                        const resta = tiempos[f * cols + c] - t;
                        if (resta <= 0) continue;            // celda ya revelada
                        quedan = true;
                        const x = c * celda, y = f * celda;
                        ctx.fillStyle = '#040805';
                        ctx.fillRect(x, y, celda, celda);
                        if (resta < 260) {                   // frente de la ola: caracteres
                            ctx.fillStyle = resta < 90 ? '#d8ffe5' : '#39ff7a';
                            ctx.fillText(CARACTERES[(Math.random() * CARACTERES.length) | 0], x + 3, y);
                        }
                    }
                }
                if (quedan) requestAnimationFrame(cuadro);
                else { ctx.clearRect(0, 0, W, H); resolver(); }
            }
            cuadro(inicio); // primer cuadro inmediato: pantalla negra completa, sin parpadeo
        });
    }

    async function fase3(rapida) {
        enTransicion = true;
        saltando = false;                       // a partir de aquí las esperas vuelven a ser reales
        capa.classList.add('revelando');        // el fondo negro pasa al lienzo
        const ola = olaDeCodigo(rapida ? 900 : 1200);
        revelar();                              // el escritorio empieza a entrar
        await esperar(120);
        volarAlInicio();
        await ola;
        await esperar(rapida ? 150 : 350);
    }

    // ------------------------------------------------------------------
    // Control general
    // ------------------------------------------------------------------
    function terminar() {
        try { sessionStorage.setItem(CLAVE, '1'); } catch (e) { }
        capa.hidden = true;
        capa.className = '';
        raiz.classList.add('sin-arranque');
        corriendo = false;
        document.dispatchEvent(new CustomEvent('mv:arranque-fin'));
    }

    // Enter, cualquier tecla o un clic: se salta directo a la ola de código
    function omitir(e) {
        if (!corriendo || saltando || enTransicion) return;
        e.preventDefault();
        e.stopPropagation();
        saltando = true;
    }

    async function iniciar() {
        corriendo = true;
        capa.hidden = false;
        capa.classList.add('encendiendo');
        await fase1();
        if (!saltando) await fase2();
        await fase3(saltando);
        terminar();
    }

    document.addEventListener('keydown', omitir, true);
    capa.addEventListener('pointerdown', omitir);

    if (raiz.classList.contains('sin-arranque')) {
        capa.hidden = true;
    } else {
        iniciar();
    }
})();
