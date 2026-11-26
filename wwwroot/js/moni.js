// =====================================================================
// MONI — el ayudante del escritorio de Monitor
// - Tutorial: al entrar por primera vez recorre el escritorio ícono por ícono (se puede saltar).
// - Preguntas: responde primero de forma sencilla; con «Más información» explica a fondo,
//   y con «Guíeme paso a paso» acompaña al usuario DENTRO de la ventana del módulo,
//   señalando cada botón y campo (guías en moni-guias.js). Responde /Moni/Preguntar.
// Personaje original: un pequeño monitor CRT con la línea de pulso del logo como boca.
// =====================================================================
(() => {
    'use strict';
    const E = window.Escritorio;
    if (!E) return;
    const GUIAS = window.MoniGuias || {};

    const $ = (sel, raiz) => (raiz || document).querySelector(sel);
    // Se guarda por SESIÓN: cada vez que se abre Monitor en un navegador nuevo, Moni vuelve a
    // aparecer y a ofrecer el tutorial (ideal para que cualquiera que lo abra lo vea desde el inicio).
    const guardar = {
        get(k) { try { return sessionStorage.getItem(k); } catch (e) { return null; } },
        set(k, v) { try { sessionStorage.setItem(k, v); } catch (e) { } },
        quitar(k) { try { sessionStorage.removeItem(k); } catch (e) { } }
    };
    try { localStorage.removeItem('mv-moni-tutorial'); localStorage.removeItem('mv-moni-oculto'); } catch (e) { } // limpia valores de versiones anteriores
    const CLAVE_TUTORIAL = 'mv-moni-tutorial';
    const CLAVE_OCULTO = 'mv-moni-oculto';
    const nombreDe = (id) => (E.modulos.find((m) => m.id === id) || {}).nombre || id;

    // ------------------------------------------------------------------
    // Dibujo del personaje (pixel art 16 x 18, cada píxel = 4 px)
    // ------------------------------------------------------------------
    const px = (x, y, w, h, c) => `<rect x="${x}" y="${y}" width="${w}" height="${h}" fill="${c}"/>`;
    const CARA = '#d9d2bd', LUZ = '#f6f1e3', SOMBRA = '#8a8070', OSCURO = '#4a443a', PANTALLA = '#0b1a14', FOSFORO = '#39ff7a';
    const svgMoni =
        '<svg viewBox="0 0 16 18" width="64" height="72" shape-rendering="crispEdges" aria-hidden="true">' +
        px(1, 1, 14, 12, CARA) + px(1, 1, 14, 1, LUZ) + px(1, 1, 1, 12, LUZ) +
        px(14, 2, 1, 11, SOMBRA) + px(2, 12, 13, 1, SOMBRA) + px(0, 1, 1, 12, OSCURO) + px(15, 1, 1, 12, OSCURO) +
        px(1, 0, 14, 1, OSCURO) + px(1, 13, 14, 1, OSCURO) +
        px(3, 3, 10, 8, PANTALLA) + px(3, 3, 10, 1, '#16302a') +
        '<g class="moni-mirada"><g class="moni-ojos">' + px(5, 5, 2, 2, FOSFORO) + px(9, 5, 2, 2, FOSFORO) + '</g></g>' +
        '<g class="moni-boca boca-a">' + px(5, 8, 1, 1, FOSFORO) + px(6, 9, 4, 1, FOSFORO) + px(10, 8, 1, 1, FOSFORO) + '</g>' +
        '<g class="moni-boca boca-b">' + px(4, 9, 2, 1, FOSFORO) + px(6, 8, 1, 1, FOSFORO) + px(7, 7, 1, 1, FOSFORO) +
        px(8, 9, 1, 1, FOSFORO) + px(9, 8, 1, 1, FOSFORO) + px(10, 9, 2, 1, FOSFORO) + '</g>' +
        px(12, 11, 1, 1, '#f2c230') + px(6, 14, 4, 1, SOMBRA) + px(4, 15, 8, 2, CARA) + px(4, 15, 8, 1, LUZ) +
        px(3, 15, 1, 2, OSCURO) + px(12, 15, 1, 2, OSCURO) + px(4, 17, 8, 1, OSCURO) +
        '</svg>';

    // ------------------------------------------------------------------
    // Elementos en pantalla
    // ------------------------------------------------------------------
    const moni = document.createElement('div');
    moni.id = 'moni';
    moni.innerHTML =
        '<div class="moni-globo" role="dialog" aria-label="Moni, el ayudante" hidden>' +
        '  <button type="button" class="moni-x" aria-label="Cerrar">&times;</button>' +
        '  <strong class="moni-titulo"></strong>' +
        '  <div class="moni-progreso" hidden><span></span></div>' +
        '  <div class="moni-texto" aria-live="polite"></div>' +
        '  <div class="moni-chips"></div>' +
        '  <form class="moni-pregunta" autocomplete="off">' +
        '    <input type="text" maxlength="200" placeholder="Escriba su pregunta..." aria-label="Pregunta para Moni" />' +
        '    <button type="submit" class="boton95">Preguntar</button>' +
        '  </form>' +
        '  <div class="moni-acciones"></div>' +
        '</div>' +
        '<button type="button" class="moni-cuerpo" title="Moni — haga clic para preguntarme algo" aria-label="Moni, el ayudante. Haga clic para preguntar.">' +
        svgMoni + '</button>';
    document.body.appendChild(moni);

    const foco = document.createElement('div');
    foco.id = 'moni-foco';
    foco.hidden = true;
    document.body.appendChild(foco);

    const globo = $('.moni-globo', moni);
    const titulo = $('.moni-titulo', moni);
    const progreso = $('.moni-progreso', moni);
    const texto = $('.moni-texto', moni);
    const chips = $('.moni-chips', moni);
    const acciones = $('.moni-acciones', moni);
    const formulario = $('.moni-pregunta', moni);
    const entrada = $('input', formulario);
    const cuerpo = $('.moni-cuerpo', moni);

    // ------------------------------------------------------------------
    // Posición
    // ------------------------------------------------------------------
    const ANCHO = 64, ALTO = 72, ANCHO_GLOBO = 330;
    function irA(x, y, ladoGlobo) {
        const maxX = window.innerWidth - ANCHO - 6, maxY = window.innerHeight - ALTO - 36;
        const yFinal = Math.max(6, Math.min(maxY, y));
        moni.style.left = Math.max(6, Math.min(maxX, x)) + 'px';
        moni.style.top = yFinal + 'px';
        moni.classList.toggle('globo-derecha', ladoGlobo === 'derecha');
        moni.classList.toggle('globo-abajo', yFinal < window.innerHeight * 0.45); // arriba en pantalla: el globo crece hacia abajo
    }
    let casa = null;          // posición elegida por el usuario al arrastrar a Moni (null = esquina)
    let manualEnGuia = false; // durante una guía, si el usuario lo movió, Moni se queda donde lo dejó
    const ladoSegun = (x) => (x + ANCHO / 2 < window.innerWidth / 2 ? 'derecha' : 'izquierda');
    function irACasa() {
        if (casa) irA(casa.x, casa.y, ladoSegun(casa.x));
        else irA(window.innerWidth - ANCHO - 14, window.innerHeight - ALTO - 40, 'izquierda');
        foco.hidden = true;
    }
    // Marco amarillo alrededor de lo que se explica
    function marcar(r) {
        foco.style.left = (r.left - 4) + 'px';
        foco.style.top = (r.top - 4) + 'px';
        foco.style.width = (r.width + 8) + 'px';
        foco.style.height = (r.height + 8) + 'px';
        foco.hidden = false;
    }
    function irJunto(el) {
        const r = el.getBoundingClientRect();
        const enDerecha = r.left > window.innerWidth / 2;
        // Si es un ícono, Moni se para a la derecha de TODAS las columnas de íconos para no taparlas
        const derechaIconos = el.classList.contains('icono')
            ? Math.max(...[...el.parentElement.children].map((i) => i.getBoundingClientRect().right))
            : r.right;
        if (enDerecha) irA(r.left - ANCHO - 18, r.bottom - ALTO / 2, 'izquierda');
        else irA(derechaIconos + 18, r.top + r.height / 2 - ALTO / 2, 'derecha');
        marcar(r);
    }
    window.addEventListener('resize', () => { if (!enTutorial && !guia) irACasa(); });

    // ------------------------------------------------------------------
    // Hablar: el texto aparece letra por letra y la boca se mueve
    // ------------------------------------------------------------------
    let temporizador = null, textoCompleto = '';
    function decir(t, nombre) {
        titulo.textContent = nombre || 'Moni';
        textoCompleto = t;
        texto.textContent = '';
        globo.hidden = false;
        moni.classList.add('hablando');
        clearInterval(temporizador);
        let i = 0;
        temporizador = setInterval(() => {
            i += 3;
            texto.textContent = textoCompleto.slice(0, i);
            if (i >= textoCompleto.length) terminarDeHablar();
        }, 16);
    }
    function terminarDeHablar() {
        clearInterval(temporizador);
        texto.textContent = textoCompleto;
        moni.classList.remove('hablando');
    }
    function botones(lista) {
        acciones.innerHTML = '';
        lista.forEach(([etiqueta, accion, principal]) => {
            const b = document.createElement('button');
            b.type = 'button';
            b.className = 'boton95' + (principal ? ' principal' : '');
            b.textContent = etiqueta;
            b.addEventListener('click', accion);
            acciones.appendChild(b);
        });
    }
    function sugerencias(lista) {
        chips.innerHTML = '';
        (lista || []).forEach((s) => {
            const b = document.createElement('button');
            b.type = 'button';
            b.className = 'moni-chip';
            b.textContent = s;
            b.addEventListener('click', () => preguntar(s));
            chips.appendChild(b);
        });
    }
    function barraProgreso(actual, total) {
        progreso.hidden = !total;
        if (total) progreso.firstChild.style.width = Math.round(actual / total * 100) + '%';
    }
    function cerrarGlobo() {
        terminarDeHablar();
        globo.hidden = true;
        if (enTutorial) terminarTutorial(false);
        if (guia) salirGuia();
    }
    $('.moni-x', moni).addEventListener('click', cerrarGlobo);
    document.addEventListener('keydown', (e) => { if (e.key === 'Escape' && !globo.hidden) cerrarGlobo(); });

    // ------------------------------------------------------------------
    // Tutorial del escritorio
    // ------------------------------------------------------------------
    const PASOS = [
        { texto: '¡Hola! Soy Moni, el ayudante de Monitor. En un minuto le muestro para qué sirve cada parte del escritorio. Puede saltar el recorrido cuando quiera.' },
        { icono: 'clientes', texto: 'Aquí se registran los clientes del supermercado: nombre, teléfono y dirección. Tiene búsqueda instantánea y no deja repetir teléfonos.' },
        { icono: 'categorias', texto: 'Las categorías agrupan los productos (Abarrotes, Bebidas, Lácteos...). Sirven para filtrar, para reportes y para promociones.' },
        { icono: 'proveedores', texto: 'Los proveedores son las empresas que surten al supermercado. Cada producto pertenece a uno.' },
        { icono: 'empleados', texto: 'Empleados: cajeros, bodegueros y demás personal, con su DUI. Quien ya tiene ventas no se borra: se marca como inactivo.' },
        { icono: 'metodos', texto: 'Métodos de pago: efectivo, tarjeta, transferencia... Estos cinco primeros son los formularios básicos.' },
        { icono: 'productos', texto: 'El catálogo: cada producto con su categoría, proveedor, precio, stock y código de barras. Los que tienen 5 unidades o menos salen en rojo.' },
        { icono: 'ajustes', texto: 'Ajustes de inventario: entradas o salidas que no son ventas, como productos vencidos, dañados o un conteo físico.' },
        { icono: 'promociones', texto: 'Promociones: descuentos por producto o por categoría con fecha de inicio y fin. La caja aplica sola el mejor descuento.' },
        { icono: 'ventas', texto: '¡La caja! Aquí se hace una venta con varios productos. El stock baja solo, se aplican promociones y sale el ticket.' },
        { icono: 'historial', texto: 'El historial de ventas: buscar tickets, reimprimirlos, anular una venta o registrar una devolución.' },
        { icono: 'compras', texto: 'Compras: la factura del proveedor. Al registrarla, el stock sube. También se puede anular.' },
        { icono: 'devoluciones', texto: 'Devoluciones: el cliente regresa productos y se le devuelve lo que pagó. La mercadería vuelve al inventario o se descarta.' },
        { icono: 'reportes', texto: '10 reportes con gráficos, filtros de fecha, impresión y exportación a Excel. ¡El kardex es mi favorito!' },
        { icono: 'asistente', texto: 'El Asistente IA aprende del historial de ventas con ML.NET y predice cuándo se agota cada producto y cuánto comprar.' },
        { elemento: '#monitor', texto: 'Este es el Monitor de ventas, el dashboard: ventas y monto de hoy, clientes y stock bajo. Se actualiza solo cada 30 segundos.' },
        { elemento: '#btn-inicio', texto: 'En el menú Inicio están todos los módulos, el fondo de escritorio y yo. Con clic derecho en el escritorio hay más opciones.' },
        { final: true, texto: '¡Eso es todo! Cuando tenga una duda, haga clic en mí. Le respondo, le explico más si quiere, y hasta lo acompaño paso a paso dentro de cada módulo.' }
    ];
    let enTutorial = false, paso = 0;

    function mostrarPaso() {
        const p = PASOS[paso];
        const objetivo = p.icono ? E.icono(p.icono) : p.elemento ? $(p.elemento) : null;
        if (objetivo) irJunto(objetivo); else irACasa();
        formulario.hidden = true;
        sugerencias([]);
        barraProgreso(paso, PASOS.length - 1);
        decir(p.texto, (p.icono ? nombreDe(p.icono) : 'Moni') + (paso > 0 && !p.final ? ` · ${paso}/${PASOS.length - 2}` : ''));

        const lista = [];
        if (paso > 0 && !p.final) lista.push(['< Anterior', () => { paso--; mostrarPaso(); }]);
        if (p.final) lista.push(['Preguntarme algo', () => { terminarTutorial(true); abrirChat(); }, true], ['Cerrar', () => terminarTutorial(true)]);
        else lista.push([paso === 0 ? 'Empezar' : 'Siguiente >', siguiente, true], ['Saltar tutorial', () => terminarTutorial(true)]);
        botones(lista);
        $('.principal', acciones)?.focus();
    }
    function siguiente() {
        if (moni.classList.contains('hablando')) { terminarDeHablar(); return; } // primer clic: termina de escribir
        paso = Math.min(paso + 1, PASOS.length - 1);
        mostrarPaso();
    }
    function iniciarTutorial() {
        E.cerrarMenus();
        if (guia) terminarGuia();
        mostrar();
        enTutorial = true;
        paso = 0;
        mostrarPaso();
    }
    function terminarTutorial(marcarVisto) {
        enTutorial = false;
        barraProgreso(0, 0);
        if (marcarVisto) guardar.set(CLAVE_TUTORIAL, '1');
        globo.hidden = true;
        irACasa();
    }

    // ------------------------------------------------------------------
    // Guías paso a paso DENTRO de las ventanas
    // ------------------------------------------------------------------
    let guia = null, pasoGuia = 0, vigilante = null, desplazado = -1, recargado = -1;

    function iniciarGuia(clave) {
        const g = GUIAS[clave];
        if (!g) return;
        if (enTutorial) terminarTutorial(true);
        E.cerrarMenus();
        mostrar();
        guia = g;
        pasoGuia = 0;
        formulario.hidden = true;
        sugerencias([]);
        mostrarPasoGuia();
    }
    function terminarGuia() {
        guia = null;
        manualEnGuia = false;
        clearInterval(vigilante);
        barraProgreso(0, 0);
        foco.hidden = true;
    }
    function salirGuia() {
        terminarGuia();
        globo.hidden = true;
        irACasa();
    }
    function siguienteGuia() {
        if (!guia) return;
        if (pasoGuia < guia.pasos.length - 1) { pasoGuia++; mostrarPasoGuia(); }
    }

    const urlDe = (id) => (E.modulos.find((m) => m.id === id) || {}).url || '';
    const rutaDe = (v) => { try { return v.iframe.contentWindow.location.pathname; } catch (e) { return ''; } };
    const docDe = (v) => { try { const d = v.iframe.contentDocument; return d && d.readyState === 'complete' ? d : null; } catch (e) { return null; } };

    function mostrarPasoGuia() {
        const p = guia.pasos[pasoGuia];
        const total = guia.pasos.length;
        clearInterval(vigilante);
        desplazado = -1;
        barraProgreso(pasoGuia + 1, total);
        decir(p.texto, `${guia.titulo} · paso ${pasoGuia + 1} de ${total}`);

        const lista = [];
        if (pasoGuia > 0) lista.push(['<', () => { pasoGuia--; mostrarPasoGuia(); }]);
        if (p.abrir) lista.push(['Ábralo por mí', () => abrirDesdeCero(p.abrir), true]);
        else if (p.final) lista.push(['Terminar', terminarConExito, true]);
        else lista.push([p.esperar ? 'Ya lo hice >' : 'Siguiente >', siguienteGuia, !p.esperar]);
        lista.push(['Salir de la guía', salirGuia]);
        botones(lista);

        // La ventana del paso debe estar abierta y al frente
        if (p.en) { if (E.ventana(p.en)) E.alFrente(p.en); else abrirDesdeCero(p.en); }
        ubicarGuia();
        vigilante = setInterval(vigilar, 400);
    }

    // Abre el módulo en su pantalla principal (si ya estaba abierto en otra página, lo regresa)
    function abrirDesdeCero(id) {
        const v = E.ventana(id);
        if (!v) { E.abrir(id); return; }
        E.alFrente(id);
        if (rutaDe(v).toLowerCase() !== urlDe(id).toLowerCase()) v.iframe.src = urlDe(id);
    }

    function vigilar() {
        if (!guia) return;
        const p = guia.pasos[pasoGuia];
        if (p.abrir) {
            const v = E.ventana(p.abrir);
            if (v && docDe(v)) {
                // Abierta, pero en otra página (por ejemplo un ticket viejo): la regresamos al inicio del módulo
                if (rutaDe(v).toLowerCase() !== urlDe(p.abrir).toLowerCase()) {
                    if (recargado !== pasoGuia) { recargado = pasoGuia; v.iframe.src = urlDe(p.abrir); }
                    return;
                }
                recargado = -1;
                siguienteGuia();
                return;
            }
        } else if (p.esperar) {
            const v = E.ventana(p.en);
            if (v && docDe(v) && new RegExp(p.esperar, 'i').test(rutaDe(v))) { siguienteGuia(); return; }
        }
        ubicarGuia(); // sigue a la ventana si el usuario la mueve o se desplaza
    }

    function ubicarGuia() {
        const p = guia.pasos[pasoGuia];
        if (p.abrir) {
            const ic = E.icono(p.abrir);
            if (ic) { if (manualEnGuia) marcar(ic.getBoundingClientRect()); else irJunto(ic); }
            return;
        }

        const v = E.ventana(p.en);
        if (!v || v.el.classList.contains('min')) { irACasa(); return; }
        const vr = v.el.getBoundingClientRect();
        let r = vr;
        const doc = docDe(v);
        const objetivo = doc && p.sel ? doc.querySelector(p.sel) : null;
        if (objetivo && objetivo.getClientRects().length) {
            if (desplazado !== pasoGuia) { desplazado = pasoGuia; objetivo.scrollIntoView({ block: 'center', behavior: 'smooth' }); }
            const ir = v.iframe.getBoundingClientRect(), er = objetivo.getBoundingClientRect();
            const top = Math.max(ir.top, ir.top + er.top), bottom = Math.min(ir.bottom, ir.top + er.bottom);
            r = { left: ir.left + er.left, top, width: er.width, height: Math.max(8, bottom - top) };
        }
        marcar(r);
        if (manualEnGuia) return; // el usuario lo puso en otro lugar: solo se mueve el marco amarillo

        // Moni se coloca al lado de la ventana, a la altura de lo que señala
        const y = r.top + r.height / 2 - ALTO / 2;
        const libreDer = window.innerWidth - vr.right, libreIzq = vr.left;
        if (libreDer >= ANCHO + 20) irA(vr.right + 10, y, libreDer >= ANCHO + ANCHO_GLOBO + 20 ? 'derecha' : 'izquierda');
        else if (libreIzq >= ANCHO + 20) irA(vr.left - ANCHO - 10, y, libreIzq >= ANCHO + ANCHO_GLOBO + 20 ? 'izquierda' : 'derecha');
        else irA(window.innerWidth - ANCHO - 14, window.innerHeight - ALTO - 40, 'izquierda');
    }

    function terminarConExito() {
        const nombreGuia = guia.titulo;
        terminarGuia();
        irACasa();
        formulario.hidden = false;
        decir(`¡Muy bien! Terminamos «${nombreGuia}». ¿Le explico algo más?`);
        sugerencias(['¿Cómo hago una venta?', '¿Qué es el kardex?', '¿Cómo funciona la IA?']);
        botones([['Repetir tutorial', iniciarTutorial], ['Ocultar a Moni', ocultar]]);
    }

    // ------------------------------------------------------------------
    // Preguntas: respuesta sencilla → «Más información» → «Guíeme paso a paso»
    // ------------------------------------------------------------------
    function abrirChat(saludo) {
        if (enTutorial) terminarTutorial(true);
        if (guia) terminarGuia();
        mostrar();
        irACasa();
        formulario.hidden = false;
        barraProgreso(0, 0);
        decir(saludo || '¿En qué le ayudo? Pregúnteme cómo funciona cualquier parte de Monitor, o pídame un dato.');
        sugerencias(['¿Cómo funciona Clientes?', '¿Cómo hago una venta?', '¿Cuánto se vendió hoy?', '¿Qué se está agotando?']);
        botones([['Repetir tutorial', iniciarTutorial], ['Ocultar a Moni', ocultar]]);
        setTimeout(() => entrada.focus(), 50);
    }

    // Botones según lo que traiga la respuesta
    function accionesRespuesta(r, yaDetalle) {
        const lista = [];
        if (r.guia && GUIAS[r.guia]) lista.push(['Guíeme paso a paso', () => iniciarGuia(r.guia), r.pideGuia || yaDetalle]);
        if (r.detalle && !yaDetalle) lista.push(['Más información', () => {
            decir(r.detalle);
            accionesRespuesta(r, true);
        }, !r.pideGuia]);
        if (r.modulo && E.modulos.some((m) => m.id === r.modulo))
            lista.push(['Abrir ' + nombreDe(r.modulo), () => { globo.hidden = true; E.abrir(r.modulo); }]);
        botones(lista);
    }

    let consultando = false;
    async function preguntar(t) {
        t = (t || '').trim();
        if (!t || consultando) return;
        consultando = true;
        entrada.value = '';
        sugerencias([]);
        botones([]);
        decir('Déjeme ver...');
        try {
            const resp = await fetch('/Moni/Preguntar?texto=' + encodeURIComponent(t), { cache: 'no-store', headers: { 'Accept': 'application/json' } });
            if (!resp.ok) throw new Error(resp.status);
            const r = await resp.json();
            decir(r.texto);
            sugerencias(r.sugerencias);
            accionesRespuesta(r, false);
        } catch (err) {
            decir('Uy, no pude consultar eso ahora. Revise que el sistema esté funcionando e intente de nuevo.');
        } finally {
            consultando = false;
            entrada.focus();
        }
    }
    formulario.addEventListener('submit', (e) => { e.preventDefault(); preguntar(entrada.value); });

    let arrastre = null, recienArrastrado = false;
    cuerpo.addEventListener('pointerdown', (e) => {
        if (e.button !== 0) return;
        const r = moni.getBoundingClientRect();
        arrastre = { x0: e.clientX, y0: e.clientY, dx: e.clientX - r.left, dy: e.clientY - r.top, movido: false };
        cuerpo.setPointerCapture(e.pointerId);
    });
    cuerpo.addEventListener('pointermove', (e) => {
        if (!arrastre) return;
        if (!arrastre.movido && Math.hypot(e.clientX - arrastre.x0, e.clientY - arrastre.y0) < 5) return;
        arrastre.movido = true;
        moni.classList.add('arrastrando');
        const x = e.clientX - arrastre.dx, y = e.clientY - arrastre.dy;
        irA(x, y, ladoSegun(x));
    });
    cuerpo.addEventListener('pointerup', () => {
        if (!arrastre) return;
        if (arrastre.movido) {
            recienArrastrado = true;               // evita que el "clic" del final abra el globo
            moni.classList.remove('arrastrando');
            const r = moni.getBoundingClientRect();
            if (guia || enTutorial) manualEnGuia = true;
            else casa = { x: r.left, y: r.top };
        }
        arrastre = null;
    });
    window.addEventListener('resize', () => { if (casa) casa = null; });

    cuerpo.addEventListener('click', () => {
        if (recienArrastrado) { recienArrastrado = false; return; }
        moni.classList.remove('saltando'); void moni.offsetWidth; moni.classList.add('saltando');
        if (enTutorial) { siguiente(); return; }
        if (guia) { globo.hidden = !globo.hidden; return; } // durante una guía, el clic muestra/oculta el globo
        if (!globo.hidden && !formulario.hidden) { cerrarGlobo(); return; }
        abrirChat();
    });

    // ------------------------------------------------------------------
    // Mostrar / ocultar
    // ------------------------------------------------------------------
    function mostrar() {
        guardar.quitar(CLAVE_OCULTO);
        moni.hidden = false;
        document.body.classList.add('con-moni');
    }
    function ocultar() {
        if (guia) terminarGuia();
        terminarDeHablar();
        globo.hidden = true;
        moni.hidden = true;
        foco.hidden = true;
        document.body.classList.remove('con-moni');
        guardar.set(CLAVE_OCULTO, '1');
        E.globo('Moni se fue a descansar', 'Puede llamarlo de nuevo desde Inicio → Ayudante Moni.');
    }

    // Opciones en el menú Inicio
    E.agregarAlMenu('Ayudante Moni', 'monitor', () => abrirChat());
    E.agregarAlMenu('Tutorial del escritorio', 'info', iniciarTutorial);

    // ------------------------------------------------------------------
    // Vida propia: parpadea y mira a los lados
    // ------------------------------------------------------------------
    const ojos = $('.moni-ojos', moni);
    setInterval(() => {
        if (moni.hidden) return;
        ojos.classList.add('parpadeo');
        setTimeout(() => ojos.classList.remove('parpadeo'), 140);
    }, 3800);
    const mirada = $('.moni-mirada', moni);
    let ultimoMovimiento = 0, pendiente = null;
    function mirarA(x, y) {
        const r = cuerpo.getBoundingClientRect();
        const cx = r.left + r.width / 2, cy = r.top + r.height * 0.33; // centro de la pantallita
        const dx = x - cx, dy = y - cy;
        const dist = Math.hypot(dx, dy);
        // Cerca de Moni mira al frente; lejos, mueve los ojos 1 píxel hacia el puntero
        const mx = dist < 24 ? 0 : Math.round(dx / dist), my = dist < 24 ? 0 : Math.round(dy / dist);
        mirada.style.transform = `translate(${mx}px, ${my}px)`;
    }
    document.addEventListener('pointermove', (e) => {
        ultimoMovimiento = Date.now();
        if (moni.hidden || pendiente) return;
        pendiente = requestAnimationFrame(() => { pendiente = null; mirarA(e.clientX, e.clientY); });
    });
    setInterval(() => {
        if (moni.hidden || Date.now() - ultimoMovimiento < 4000) return; // si el mouse está quieto, mira a los lados
        const d = [-1, 0, 1][Math.floor(Math.random() * 3)];
        mirada.style.transform = `translate(${d}px, 0)`;
    }, 2600);

    // ------------------------------------------------------------------
    // Inicio: aparece al terminar la intro; la primera vez ofrece el tutorial
    // ------------------------------------------------------------------
    moni.hidden = true;
    function aparecer() {
        if (guardar.get(CLAVE_OCULTO)) return;
        mostrar();
        irACasa();
        moni.classList.add('entrando');
        setTimeout(() => moni.classList.remove('entrando'), 900);
        if (!guardar.get(CLAVE_TUTORIAL)) {
            setTimeout(() => {
                formulario.hidden = true;
                decir('¡Hola! Soy Moni, su ayudante. ¿Le muestro rápidamente para qué sirve cada parte del escritorio?');
                botones([
                    ['Sí, muéstreme', iniciarTutorial, true],
                    ['Ahora no', () => { guardar.set(CLAVE_TUTORIAL, '1'); decir('¡Está bien! Si me necesita, haga clic en mí.'); botones([]); setTimeout(() => { globo.hidden = true; }, 3500); }]
                ]);
            }, 700);
        }
    }
    if (document.documentElement.classList.contains('arrancando'))
        document.addEventListener('mv:arranque-fin', () => setTimeout(aparecer, 400), { once: true });
    else setTimeout(aparecer, 700);

    // ------------------------------------------------------------------
    // Despedida al apagar Monitor: Moni va al centro, más grande, y se despide
    // ------------------------------------------------------------------
    function despedir(mensaje, duracion) {
        if (guia) terminarGuia();
        if (enTutorial) { enTutorial = false; barraProgreso(0, 0); }
        mostrar();
        foco.hidden = true;
        formulario.hidden = true;
        sugerencias([]);
        botones([]);
        moni.classList.remove('globo-derecha', 'globo-abajo');
        moni.classList.add('centro');
        moni.style.left = (window.innerWidth / 2 - ANCHO / 2) + 'px';
        moni.style.top = (window.innerHeight / 2 - ALTO / 2 + 40) + 'px';
        decir(mensaje, 'Moni');
        return new Promise((listo) => setTimeout(listo, duracion || 2800));
    }

    // Para que el escritorio sepa si Moni está ocupado (y no tape sus avisos)
    window.Moni = { ocupado: () => enTutorial || !!guia || !globo.hidden, abrir: abrirChat, tutorial: iniciarTutorial, guia: iniciarGuia, despedir };
})();
