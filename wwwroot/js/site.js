// =====================================================================
// Monitor — utilidades comunes de las páginas de cada módulo
// =====================================================================
(function () {
    'use strict';

    // Quita acentos y pasa a minúsculas para comparar ("jose" encuentra "José")
    function normalizar(texto) {
        return (texto || '').normalize('NFD').replace(/[̀-ͯ]/g, '').toLowerCase();
    }

    // ------------------------------------------------------------------
    // Búsqueda interactiva para cualquier lista.
    // Uso en la vista:
    //   <input data-filtrar="#tabla" data-contador="#contador" data-entidad="clientes">
    //   <tr class="fila-vacia"> ... </tr>   (fila que se muestra si no hay resultados)
    // ------------------------------------------------------------------
    document.querySelectorAll('input[data-filtrar]').forEach(function (input) {
        const tabla = document.querySelector(input.dataset.filtrar);
        if (!tabla) return;

        const filas = Array.from(tabla.querySelectorAll('tbody tr:not(.fila-vacia)'));
        const vacia = tabla.querySelector('tbody tr.fila-vacia');
        const contador = input.dataset.contador ? document.querySelector(input.dataset.contador) : null;
        const entidad = input.dataset.entidad || 'registros';

        function filtrar() {
            const texto = normalizar(input.value.trim());
            let visibles = 0;
            filas.forEach(function (fila) {
                const coincide = normalizar(fila.textContent).includes(texto);
                fila.classList.toggle('d-none', !coincide);
                if (coincide) visibles++;
            });

            if (vacia && filas.length > 0) {
                vacia.classList.toggle('d-none', visibles > 0);
                if (visibles === 0) {
                    vacia.cells[0].textContent = 'No hay ' + entidad + ' que coincidan con la búsqueda.';
                }
            }
            if (contador) {
                contador.textContent = filas.length ? 'Mostrando ' + visibles + ' de ' + filas.length + ' ' + entidad : '';
            }
        }

        input.addEventListener('input', filtrar);
        filtrar();
    });

    // ------------------------------------------------------------------
    // DUI: solo números y el guion se pone solo (00000000-0)
    // ------------------------------------------------------------------
    document.querySelectorAll('input[data-formato="dui"]').forEach(function (input) {
        input.addEventListener('input', function () {
            const digitos = input.value.replace(/\D/g, '').slice(0, 9);
            input.value = digitos.length > 8 ? digitos.slice(0, 8) + '-' + digitos.slice(8) : digitos;
        });
    });

    // Campos que solo aceptan dígitos (código de barras)
    document.querySelectorAll('input[data-formato="digitos"]').forEach(function (input) {
        input.addEventListener('input', function () {
            const limpio = input.value.replace(/\D/g, '');
            if (limpio !== input.value) input.value = limpio;
        });
    });

    // ------------------------------------------------------------------
    // Si esta página guardó, editó, eliminó o anuló algo, avisa al
    // escritorio para que actualice las demás ventanas abiertas.
    // ------------------------------------------------------------------
    if (document.body.dataset.cambio === '1' && window.parent !== window) {
        try { window.parent.postMessage({ tipo: 'mv:cambio' }, window.location.origin); } catch (e) { }
    }

    // ------------------------------------------------------------------
    // Íconos de la barra de herramientas: cada botón de arriba (y los de
    // guardar/buscar) recibe un pequeño ícono en pixel art según su texto.
    // ------------------------------------------------------------------
    const px = function (x, y, w, h, c) { return '<rect x="' + x + '" y="' + y + '" width="' + w + '" height="' + h + '" fill="' + c + '"/>'; };
    const svg = function (contenido) { return '<svg viewBox="0 0 16 16" width="16" height="16" shape-rendering="crispEdges" aria-hidden="true">' + contenido + '</svg>'; };
    const ICONOS = {
        nuevo: svg(px(3, 1, 9, 14, '#fff') + px(3, 1, 9, 1, '#555') + px(3, 14, 9, 1, '#555') + px(3, 1, 1, 14, '#555') + px(11, 1, 1, 14, '#555') +
                   px(9, 7, 6, 2, '#2e8b57') + px(11, 5, 2, 6, '#2e8b57')),
        imprimir: svg(px(4, 1, 8, 5, '#fff') + px(4, 1, 8, 1, '#555') + px(1, 6, 14, 6, '#8a8070') + px(1, 6, 14, 1, '#c8c4b8') +
                      px(12, 8, 2, 1, '#39ff7a') + px(4, 10, 8, 5, '#fff') + px(5, 12, 6, 1, '#888')),
        excel: svg(px(2, 1, 12, 14, '#1f6f43') + px(4, 3, 8, 10, '#fff') + px(4, 6, 8, 1, '#1f6f43') + px(4, 9, 8, 1, '#1f6f43') + px(7, 3, 1, 10, '#1f6f43')),
        lista: svg(px(2, 1, 12, 14, '#f4efe2') + px(2, 1, 12, 2, '#1f5c4a') + px(4, 5, 8, 1, '#555') + px(4, 8, 8, 1, '#555') + px(4, 11, 6, 1, '#555')),
        anular: svg(px(4, 1, 8, 2, '#b22') + px(2, 3, 2, 2, '#b22') + px(12, 3, 2, 2, '#b22') + px(1, 5, 2, 6, '#b22') + px(13, 5, 2, 6, '#b22') +
                    px(2, 11, 2, 2, '#b22') + px(12, 11, 2, 2, '#b22') + px(4, 13, 8, 2, '#b22') + px(5, 4, 2, 2, '#b22') + px(7, 6, 2, 2, '#b22') + px(9, 8, 2, 2, '#b22') + px(11, 10, 1, 2, '#b22')),
        devolver: svg(px(6, 3, 7, 2, '#c8402a') + px(11, 5, 2, 5, '#c8402a') + px(4, 9, 9, 2, '#c8402a') + px(3, 7, 2, 6, '#c8402a') + px(1, 9, 2, 2, '#c8402a')),
        ticket: svg(px(3, 1, 10, 13, '#fff') + px(3, 1, 10, 1, '#555') + px(3, 1, 1, 13, '#555') + px(12, 1, 1, 13, '#555') +
                    px(5, 4, 6, 1, '#555') + px(5, 6, 6, 1, '#555') + px(5, 9, 4, 1, '#555') + px(3, 14, 2, 1, '#555') + px(7, 14, 2, 1, '#555') + px(11, 14, 2, 1, '#555')),
        datos: svg(px(3, 2, 10, 3, '#2f5f8a') + px(3, 6, 10, 3, '#2f5f8a') + px(3, 10, 10, 3, '#2f5f8a') + px(4, 3, 8, 1, '#6d95bd') + px(4, 7, 8, 1, '#6d95bd') + px(4, 11, 8, 1, '#6d95bd')),
        guardar: svg(px(2, 2, 12, 12, '#2f5f8a') + px(4, 2, 8, 5, '#fff') + px(9, 3, 2, 3, '#2f5f8a') + px(4, 9, 8, 5, '#c8c4b8')),
        buscar: svg(px(3, 2, 6, 1, '#333') + px(2, 3, 1, 6, '#333') + px(9, 3, 1, 6, '#333') + px(3, 9, 6, 1, '#333') + px(3, 3, 6, 6, '#bfe4ff') +
                    px(9, 9, 2, 2, '#333') + px(11, 11, 2, 2, '#333') + px(13, 13, 2, 2, '#333')),
        generar: svg(px(2, 9, 3, 5, '#3f8f5c') + px(6, 5, 3, 9, '#e07b28') + px(10, 2, 3, 12, '#2f5f8a') + px(1, 14, 14, 1, '#555')),
        escritorio: svg(px(1, 2, 14, 10, '#c8c4b8') + px(2, 3, 12, 8, '#0b1a14') + px(4, 7, 2, 1, '#39ff7a') + px(6, 6, 2, 1, '#39ff7a') + px(8, 7, 2, 1, '#39ff7a') +
                        px(10, 5, 2, 1, '#39ff7a') + px(6, 12, 4, 2, '#808080') + px(4, 14, 8, 1, '#555'))
    };
    const REGLAS = [
        [/^\+\s*nuev/i, 'nuevo'], [/^imprimir/i, 'imprimir'], [/excel/i, 'excel'], [/^anular/i, 'anular'],
        [/devoluci/i, 'devolver'], [/^ver venta/i, 'ticket'], [/demostraci/i, 'datos'], [/^generar/i, 'generar'],
        [/^buscar/i, 'buscar'], [/^s[ií], anular/i, 'anular'], [/^(guardar|registrar|cobrar)/i, 'guardar'],
        [/^(historial|todos los reportes|volver|cancelar)/i, 'lista'], [/escritorio/i, 'escritorio']
    ];
    document.querySelectorAll('.page-header .btn, .barra-sola .btn, form .btn').forEach(function (b) {
        if (b.querySelector('svg') || b.classList.contains('btn-sm')) return;   // ya tiene ícono, o es un botón chico de tabla
        const texto = b.textContent.trim();
        const regla = REGLAS.find(function (r) { return r[0].test(texto); });
        if (!regla) return;
        b.textContent = texto.replace(/^\+\s*/, '');                           // el ícono reemplaza al "+"
        b.insertAdjacentHTML('afterbegin', '<span class="btn-ico">' + ICONOS[regla[1]] + '</span>');
    });

    // ------------------------------------------------------------------
    // Atajos de teclado
    //   F3      -> ir al cuadro de búsqueda
    //   Alt + N -> nuevo registro (si la página tiene botón "Nuevo")
    // ------------------------------------------------------------------
    document.addEventListener('keydown', function (e) {
        if (e.key === 'F3') {
            const buscar = document.querySelector('input[data-filtrar]');
            if (buscar) { e.preventDefault(); buscar.focus(); buscar.select(); }
        }
        if (e.altKey && (e.key === 'n' || e.key === 'N')) {
            const nuevo = document.querySelector('.page-header a.btn-primary');
            if (nuevo) { e.preventDefault(); nuevo.click(); }
        }
    });
})();
