// =====================================================================
// APAGAR MONITOR
// Inicio → Apagar... (o Ctrl+Q) muestra un diálogo clásico: Apagar / Reiniciar / Cancelar.
// Apagar: Moni se despide al centro, las ventanas se cierran una por una, la pantalla
// se "apaga" como un monitor de tubo (se aplasta a una línea y luego a un punto) y
// queda una pantalla negra con el botón Encender.
// Una página web no puede cerrar la pestaña por seguridad: eso lo hace el usuario.
// =====================================================================
(() => {
    'use strict';
    const E = window.Escritorio;
    if (!E) return;

    const $ = (sel, raiz) => (raiz || document).querySelector(sel);
    const esperar = (ms) => new Promise((listo) => setTimeout(listo, ms));
    let enCurso = false;

    // Al apagar se olvida la sesión: la próxima vez se ve la intro completa y Moni ofrece el tutorial
    function olvidarSesion() {
        ['mv-arranque-visto', 'mv-bienvenida', 'mv-moni-tutorial', 'mv-moni-oculto']
            .forEach((k) => { try { sessionStorage.removeItem(k); } catch (e) { } });
    }

    // ------------------------------------------------------------------
    // Diálogo "¿Qué desea hacer?"
    // ------------------------------------------------------------------
    function preguntar() {
        if (enCurso) return;
        E.cerrarMenus();
        const capa = $('#capa-dialogo');
        capa.innerHTML =
            '<section class="ventana activa dialogo dialogo-apagar" role="alertdialog" aria-modal="true" aria-labelledby="ap-t">' +
            '  <div class="ventana-titulo"><span class="t" id="ap-t">Apagar Monitor</span>' +
            '    <button type="button" class="cerrar" aria-label="Cancelar">&times;</button></div>' +
            '  <div class="dialogo-cuerpo">' + E.dibujo('apagar', 32) +
            '    <div><p>¿Qué desea hacer?</p>' +
            '      <label class="ap-opcion"><input type="radio" name="ap" value="apagar" checked> <span><b>Apagar Monitor</b><small>Cierra todo y termina la sesión.</small></span></label>' +
            '      <label class="ap-opcion"><input type="radio" name="ap" value="reiniciar"> <span><b>Reiniciar</b><small>Vuelve a empezar con la intro.</small></span></label>' +
            '    </div>' +
            '  </div>' +
            '  <div class="dialogo-botones">' +
            '    <button type="button" class="boton95" id="ap-ok">Aceptar</button>' +
            '    <button type="button" class="boton95" id="ap-no">Cancelar</button>' +
            '  </div>' +
            '</section>';
        capa.hidden = false;

        const cerrar = () => { capa.hidden = true; capa.innerHTML = ''; document.removeEventListener('keydown', teclas, true); };
        const aceptar = () => {
            const opcion = $('input[name=ap]:checked', capa).value;
            cerrar();
            if (opcion === 'reiniciar') reiniciar(); else apagar();
        };
        const teclas = (e) => {
            if (e.key === 'Escape') { e.preventDefault(); e.stopPropagation(); cerrar(); }
            if (e.key === 'Enter') { e.preventDefault(); e.stopPropagation(); aceptar(); }
        };
        $('#ap-ok', capa).addEventListener('click', aceptar);
        $('#ap-no', capa).addEventListener('click', cerrar);
        $('.cerrar', capa).addEventListener('click', cerrar);
        document.addEventListener('keydown', teclas, true);
        $('#ap-ok', capa).focus();
    }

    // ------------------------------------------------------------------
    // Cerrar las ventanas abiertas una por una
    // ------------------------------------------------------------------
    async function cerrarVentanas() {
        for (const v of E.ventanas().reverse()) {
            v.el.classList.add('cerrandose');
            await esperar(160);
            E.cerrar(v.id);
        }
    }

    // ------------------------------------------------------------------
    // Efecto de monitor de tubo apagándose
    // ------------------------------------------------------------------
    async function apagarPantalla() {
        document.documentElement.classList.add('apagando');
        await esperar(900);
    }

    async function apagar() {
        if (enCurso) return;
        enCurso = true;
        olvidarSesion();
        const despedida = window.Moni
            ? window.Moni.despedir('¡Gracias por usar Monitor! Nos vemos la próxima vez.', 3000)
            : esperar(800);
        await Promise.all([despedida, cerrarVentanas()]);
        await apagarPantalla();
        pantallaFinal();
    }

    async function reiniciar() {
        if (enCurso) return;
        enCurso = true;
        olvidarSesion();
        const despedida = window.Moni ? window.Moni.despedir('¡Vuelvo enseguida!', 1500) : esperar(400);
        await Promise.all([despedida, cerrarVentanas()]);
        await apagarPantalla();
        window.location.reload();
    }

    // ------------------------------------------------------------------
    // Pantalla negra final con el botón Encender
    // ------------------------------------------------------------------
    async function pantallaFinal() {
        document.documentElement.classList.remove('apagando');
        document.documentElement.classList.add('apagado');
        const p = document.createElement('div');
        p.id = 'pantalla-apagado';
        p.innerHTML =
            '<div class="pa-texto" aria-live="polite"></div>' +
            '<button type="button" class="pa-encender" hidden>' + E.dibujo('apagar', 16) + '<span>Encender</span></button>';
        document.body.appendChild(p);

        const salida = $('.pa-texto', p);
        const lineas = ['Monitor se apagó correctamente.', '', 'Ya puede cerrar esta ventana.'];
        for (const linea of lineas) {
            for (const letra of linea) { salida.textContent += letra; await esperar(28); }
            salida.textContent += '\n';
            await esperar(250);
        }
        const boton = $('.pa-encender', p);
        boton.hidden = false;
        boton.focus();
        boton.addEventListener('click', () => window.location.reload());

        // Si el navegador lo permite (pestaña abierta por otro programa), se cierra sola
        try { window.close(); } catch (e) { }
    }

    // ------------------------------------------------------------------
    // Accesos: menú Inicio (al final) y Ctrl+Q
    // ------------------------------------------------------------------
    E.separadorMenu();
    E.agregarAlMenu('Apagar...', 'apagar', preguntar);
    document.addEventListener('keydown', (e) => {
        if (e.ctrlKey && (e.key === 'q' || e.key === 'Q')) { e.preventDefault(); preguntar(); }
    });

    window.Apagado = { preguntar, apagar, reiniciar };
})();
