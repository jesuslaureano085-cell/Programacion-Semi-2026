// =====================================================================
// MonitorVentas — Escritorio
// Íconos, ventanas (arrastrar, minimizar, maximizar, cerrar), barra de
// tareas, menú Inicio, menú de clic derecho, reloj, globos de aviso,
// cuadros de diálogo y el "Monitor de ventas" que se actualiza solo.
// =====================================================================
(function () {
    'use strict';

    // ------------------------------------------------------------------
    // Módulos del sistema. Para agregar uno nuevo basta con una línea:
    // url: null = todavía no existe (muestra "disponible pronto").
    // ------------------------------------------------------------------
    const MODULOS = [
        { id: 'clientes', nombre: 'Clientes', url: '/Clientes', icono: 'clientes' },
        { id: 'categorias', nombre: 'Categorías', url: '/Categorias', icono: 'categorias' },
        { id: 'proveedores', nombre: 'Proveedores', url: '/Proveedores', icono: 'proveedores' },
        { id: 'empleados', nombre: 'Empleados', url: '/Empleados', icono: 'empleados' },
        { id: 'metodos', nombre: 'Métodos de pago', url: '/MetodosPago', icono: 'metodos' },
        { id: 'productos', nombre: 'Productos', url: '/Productos', icono: 'productos' },
        { id: 'ajustes', nombre: 'Ajustes de inventario', url: '/AjustesInventario', icono: 'ajustes' },
        { id: 'promociones', nombre: 'Promociones', url: '/Promociones', icono: 'promociones' },
        { id: 'ventas', nombre: 'Nueva venta', url: '/Ventas/Create', icono: 'ventas' },
        { id: 'historial', nombre: 'Historial de ventas', url: '/Ventas', icono: 'historial' },
        { id: 'compras', nombre: 'Compras', url: '/Compras', icono: 'compras' },
        { id: 'devoluciones', nombre: 'Devoluciones', url: '/Devoluciones', icono: 'devoluciones' },
        { id: 'reportes', nombre: 'Reportes', url: '/Reportes', icono: 'reportes' },
        { id: 'asistente', nombre: 'Asistente IA', url: '/Asistente', icono: 'asistente' }
    ];

    // Fondos de escritorio (clic derecho > Cambiar fondo)
    const FONDOS = ['#2f6b5e', '#3b5b78', '#6a4e36', '#4b4a6e', '#5a5a52'];

    const INTERVALO_MONITOR = 30000; // ms

    // ------------------------------------------------------------------
    // Íconos pixelados propios (cuadrícula de 16x16): [x, y, ancho, alto, color]
    // ------------------------------------------------------------------
    const PIX = {
        clientes: [[9, 1, 4, 4, '#f0c8a0'], [9, 1, 4, 1, '#5a3a1a'], [8, 6, 6, 7, '#2f5f8a'], [8, 12, 6, 1, '#1f4060'],
                   [3, 4, 4, 4, '#f0c8a0'], [3, 4, 4, 1, '#3a2a1a'], [2, 9, 6, 6, '#3f8f5c'], [2, 14, 6, 1, '#245a39']],
        categorias: [[1, 3, 6, 2, '#d9a020'], [1, 5, 14, 9, '#f2c230'], [1, 5, 14, 1, '#fff3b0'], [1, 13, 14, 1, '#8a6a10'],
                     [4, 8, 8, 3, '#ffffff'], [5, 9, 6, 1, '#3f8f5c']],
        proveedores: [[1, 4, 9, 7, '#e07b28'], [1, 4, 9, 1, '#f0a050'], [10, 6, 4, 5, '#2f5f8a'], [11, 7, 2, 2, '#cfe9ff'],
                      [1, 11, 14, 1, '#404040'], [3, 12, 3, 3, '#222'], [10, 12, 3, 3, '#222'], [4, 13, 1, 1, '#bbb'], [11, 13, 1, 1, '#bbb']],
        empleados: [[6, 1, 4, 4, '#f0c8a0'], [6, 1, 4, 1, '#3a2a1a'], [4, 6, 8, 8, '#1f5c4a'], [7, 6, 2, 2, '#ffffff'],
                    [9, 9, 2, 3, '#f2c230'], [4, 14, 8, 1, '#123a2e']],
        metodos: [[1, 3, 12, 8, '#2f5f8a'], [1, 5, 12, 2, '#111'], [3, 8, 3, 2, '#f2c230'], [9, 8, 6, 6, '#f2c230'],
                  [10, 9, 4, 4, '#d9a020'], [11, 10, 2, 2, '#8a6a10']],
        productos: [[2, 4, 12, 10, '#c08a4a'], [2, 3, 12, 2, '#d9a868'], [7, 3, 2, 11, '#efe3c6'],
                    [2, 13, 12, 1, '#7a4f22'], [13, 4, 1, 10, '#8a5a2a'], [4, 9, 2, 1, '#7a4f22'], [10, 9, 2, 1, '#7a4f22']],
        ventas: [[0, 2, 3, 1, '#333'], [3, 2, 1, 2, '#333'], [3, 4, 11, 1, '#333'], [3, 4, 1, 6, '#333'], [13, 4, 1, 5, '#333'],
                 [4, 5, 9, 3, '#e07b28'], [6, 5, 1, 3, '#3f8f5c'], [9, 5, 2, 3, '#f2c230'], [4, 8, 10, 1, '#333'],
                 [4, 10, 10, 1, '#333'], [4, 12, 3, 3, '#333'], [11, 12, 3, 3, '#333'], [5, 13, 1, 1, '#bbb'], [12, 13, 1, 1, '#bbb']],
        ajustes: [[3, 2, 10, 13, '#c8a46a'], [5, 1, 6, 3, '#808080'], [4, 4, 8, 10, '#ffffff'],
                  [5, 6, 3, 1, '#3f8f5c'], [6, 5, 1, 3, '#3f8f5c'], [9, 6, 2, 1, '#555'],
                  [5, 10, 3, 1, '#c8402a'], [9, 10, 2, 1, '#555'], [5, 12, 6, 1, '#999']],
        promociones: [[2, 5, 10, 9, '#f2c230'], [12, 6, 2, 7, '#f2c230'], [14, 8, 1, 3, '#f2c230'], [3, 8, 2, 2, '#ffffff'],
                      [6, 7, 1, 1, '#c8402a'], [9, 11, 1, 1, '#c8402a'], [6, 11, 1, 1, '#c8402a'], [7, 10, 1, 1, '#c8402a'],
                      [8, 9, 1, 1, '#c8402a'], [9, 8, 1, 1, '#c8402a'], [2, 13, 10, 1, '#8a6a10']],
        compras: [[1, 6, 9, 8, '#c08a4a'], [1, 6, 9, 1, '#d9a868'], [5, 6, 1, 8, '#efe3c6'], [1, 13, 9, 1, '#7a4f22'],
                  [11, 1, 2, 7, '#3f8f5c'], [9, 3, 6, 2, '#3f8f5c'], [10, 2, 4, 1, '#3f8f5c'], [11, 4, 2, 1, '#3f8f5c']],
        devoluciones: [[5, 6, 10, 8, '#c08a4a'], [5, 6, 10, 1, '#d9a868'], [9, 6, 1, 8, '#efe3c6'], [5, 13, 10, 1, '#7a4f22'],
                       [1, 3, 6, 2, '#c8402a'], [1, 2, 2, 4, '#c8402a'], [0, 3, 1, 2, '#c8402a'], [6, 3, 1, 4, '#c8402a']],
        historial: [[2, 1, 12, 14, '#f4efe2'], [2, 1, 12, 2, '#1f5c4a'], [2, 14, 12, 1, '#8a8070'], [13, 1, 1, 14, '#8a8070'],
                    [4, 5, 8, 1, '#555'], [4, 7, 6, 1, '#555'], [4, 9, 8, 1, '#555'], [4, 11, 5, 1, '#555'], [10, 10, 3, 3, '#e07b28']],
        asistente: [[3, 3, 10, 10, '#2b2b2b'], [1, 4, 2, 1, '#9a9a9a'], [1, 7, 2, 1, '#9a9a9a'], [1, 10, 2, 1, '#9a9a9a'],
                    [13, 4, 2, 1, '#9a9a9a'], [13, 7, 2, 1, '#9a9a9a'], [13, 10, 2, 1, '#9a9a9a'], [4, 1, 1, 2, '#9a9a9a'],
                    [7, 1, 1, 2, '#9a9a9a'], [10, 1, 1, 2, '#9a9a9a'], [4, 13, 1, 2, '#9a9a9a'], [7, 13, 1, 2, '#9a9a9a'],
                    [10, 13, 1, 2, '#9a9a9a'], [5, 5, 6, 6, '#0b1a14'], [5, 9, 1, 1, '#39ff7a'], [6, 8, 1, 1, '#39ff7a'],
                    [7, 9, 1, 1, '#39ff7a'], [8, 7, 1, 1, '#39ff7a'], [9, 6, 1, 1, '#39ff7a'], [10, 5, 1, 1, '#f2c230']],
        reportes: [[3, 1, 10, 14, '#ffffff'], [3, 1, 10, 1, '#555'], [3, 14, 10, 1, '#555'], [3, 1, 1, 14, '#555'], [12, 1, 1, 14, '#555'],
                   [4, 9, 2, 4, '#3f8f5c'], [7, 6, 2, 7, '#e07b28'], [10, 4, 2, 9, '#2f5f8a'], [4, 13, 8, 1, '#777']],
        monitor: [[1, 1, 14, 11, '#c8c4b8'], [1, 1, 14, 1, '#fff'], [1, 1, 1, 11, '#fff'], [14, 1, 1, 11, '#404040'], [1, 11, 14, 1, '#404040'],
                  [3, 3, 10, 7, '#06120a'], [4, 7, 2, 1, '#39ff7a'], [6, 6, 2, 1, '#39ff7a'], [8, 7, 2, 1, '#39ff7a'], [10, 4, 2, 2, '#39ff7a'],
                  [6, 12, 4, 2, '#808080'], [4, 14, 8, 1, '#404040']],
        apagar: [[7, 1, 2, 7, '#c8402a'], [4, 3, 2, 2, '#c8402a'], [10, 3, 2, 2, '#c8402a'], [2, 5, 2, 2, '#c8402a'], [12, 5, 2, 2, '#c8402a'],
                 [1, 7, 2, 3, '#c8402a'], [13, 7, 2, 3, '#c8402a'], [2, 10, 2, 2, '#c8402a'], [12, 10, 2, 2, '#c8402a'],
                 [4, 12, 2, 2, '#c8402a'], [10, 12, 2, 2, '#c8402a'], [6, 13, 4, 2, '#c8402a']],
        info: [[5, 1, 6, 14, '#2f5f8a'], [3, 3, 10, 10, '#2f5f8a'], [4, 2, 8, 12, '#2f5f8a'], [2, 5, 12, 6, '#2f5f8a'],
               [7, 3, 2, 2, '#fff'], [7, 6, 2, 6, '#fff'], [6, 11, 4, 1, '#fff']],
        aviso: [[7, 1, 2, 2, '#f2c230'], [6, 3, 4, 2, '#f2c230'], [5, 5, 6, 2, '#f2c230'], [4, 7, 8, 2, '#f2c230'],
                [3, 9, 10, 2, '#f2c230'], [2, 11, 12, 2, '#f2c230'], [1, 13, 14, 2, '#f2c230'], [1, 14, 14, 1, '#8a6a10'],
                [7, 5, 2, 5, '#111'], [7, 11, 2, 2, '#111']],
        fondo: [[1, 2, 14, 11, '#404040'], [2, 3, 12, 9, '#7fb3d5'], [2, 8, 12, 4, '#3f8f5c'], [5, 6, 4, 3, '#3f8f5c'],
                [10, 4, 2, 2, '#f2c230'], [6, 13, 4, 1, '#808080'], [4, 14, 8, 1, '#404040']],
        campana: [[6, 1, 4, 1, '#8a6a10'], [5, 2, 6, 1, '#f2c230'], [4, 3, 8, 7, '#f2c230'], [3, 10, 10, 2, '#f2c230'],
                  [3, 12, 10, 1, '#8a6a10'], [7, 13, 2, 2, '#8a6a10'], [5, 4, 1, 4, '#fff3b0']]
    };

    function icono(nombre, tam) {
        const rects = PIX[nombre] || PIX.info;
        let s = '<svg viewBox="0 0 16 16" width="' + tam + '" height="' + tam + '" shape-rendering="crispEdges" aria-hidden="true">';
        for (const [x, y, w, h, c] of rects) {
            s += '<rect x="' + x + '" y="' + y + '" width="' + w + '" height="' + h + '" fill="' + c + '"/>';
        }
        return s + '</svg>';
    }

    // Utilidades
    const $ = (sel) => document.querySelector(sel);
    const almacen = {
        get(k, sesion) { try { return (sesion ? sessionStorage : localStorage).getItem(k); } catch (e) { return null; } },
        set(k, v, sesion) { try { (sesion ? sessionStorage : localStorage).setItem(k, v); } catch (e) { } }
    };

    const escritorio = $('#escritorio');
    const capaVentanas = $('#ventanas');
    const tareas = $('#tareas');
    const menuInicio = $('#menu-inicio');
    const btnInicio = $('#btn-inicio');
    const menuContexto = $('#menu-contexto');

    // ==================================================================
    // Ventanas
    // ==================================================================
    let zSuperior = 100;
    let contadorVentanas = 0;
    const abiertas = new Map(); // id del módulo -> ventana

    function abrirModulo(mod) {
        cerrarMenus();
        if (!mod.url) {
            dialogo(mod.nombre, 'El módulo "' + mod.nombre + '" estará disponible pronto.', 'info');
            return;
        }
        const existente = abiertas.get(mod.id);
        if (existente) { restaurar(existente); refrescarSiEsLista(existente); return; }
        crearVentana(mod);
    }

    function crearVentana(mod) {
        const el = document.createElement('section');
        el.className = 'ventana';
        el.setAttribute('role', 'dialog');
        el.setAttribute('aria-label', mod.nombre);
        el.innerHTML =
            '<div class="ventana-titulo">' +
            '  <span class="ti">' + icono(mod.icono, 16) + '</span>' +
            '  <span class="t"></span>' +
            '  <button type="button" data-acc="min" aria-label="Minimizar">_</button>' +
            '  <button type="button" data-acc="max" aria-label="Maximizar">&#9633;</button>' +
            '  <button type="button" data-acc="cerrar" class="cerrar" aria-label="Cerrar">&times;</button>' +
            '</div>' +
            '<div class="ventana-cuerpo"><iframe></iframe></div>' +
            '<div class="ventana-estado"><span class="estado-texto">Cargando...</span></div>';

        el.querySelector('.t').textContent = mod.nombre;
        const iframe = el.querySelector('iframe');
        iframe.title = mod.nombre;
        iframe.src = mod.url;

        // Tamaño y posición en cascada
        const area = escritorio.getBoundingClientRect();
        const w = Math.min(800, area.width - 20);
        const h = Math.min(540, area.height - 20);
        const desplazamiento = (contadorVentanas++ % 6) * 26;
        el.style.width = w + 'px';
        el.style.height = h + 'px';
        el.style.left = Math.max(0, Math.min(100 + desplazamiento, area.width - w)) + 'px';
        el.style.top = Math.max(0, Math.min(16 + desplazamiento, area.height - h)) + 'px';
        if (area.width < 700) el.classList.add('max');
        capaVentanas.appendChild(el);

        // Botón en la barra de tareas
        const tarea = document.createElement('button');
        tarea.type = 'button';
        tarea.className = 'tarea';
        tarea.innerHTML = icono(mod.icono, 16) + '<span></span>';
        tarea.querySelector('span').textContent = mod.nombre;
        tareas.appendChild(tarea);

        const v = { id: mod.id, modulo: mod, el, tarea, iframe };
        abiertas.set(mod.id, v);

        el.addEventListener('pointerdown', () => enfocar(v));
        el.querySelector('[data-acc="min"]').addEventListener('click', (e) => { e.stopPropagation(); minimizar(v); });
        el.querySelector('[data-acc="max"]').addEventListener('click', (e) => { e.stopPropagation(); alternarMaximizar(v); });
        el.querySelector('[data-acc="cerrar"]').addEventListener('click', (e) => { e.stopPropagation(); cerrarVentana(v); });

        const titulo = el.querySelector('.ventana-titulo');
        titulo.addEventListener('dblclick', (e) => { if (!e.target.closest('button')) alternarMaximizar(v); });
        hacerArrastrable(v, titulo);

        tarea.addEventListener('click', () => {
            if (el.classList.contains('min')) restaurar(v);
            else if (el.classList.contains('activa')) minimizar(v);
            else enfocar(v);
        });

        iframe.addEventListener('load', () => alCargarModulo(v));
        enfocar(v);
    }

    // Cuando la página del módulo termina de cargar dentro de la ventana
    function alCargarModulo(v) {
        const estado = v.el.querySelector('.estado-texto');
        try {
            const doc = v.iframe.contentDocument;
            const sub = (doc.title || '').split('·')[0].trim();
            const titulo = sub && sub !== v.modulo.nombre ? v.modulo.nombre + ' - ' + sub : v.modulo.nombre;
            v.el.querySelector('.t').textContent = titulo;
            v.tarea.querySelector('span').textContent = titulo;

            // Un clic dentro del contenido también enfoca la ventana
            doc.addEventListener('pointerdown', () => { enfocar(v); cerrarMenus(); });
            doc.addEventListener('keydown', (e) => { if (e.key === 'Escape') cerrarMenus(); });
            v.iframe.contentWindow.addEventListener('beforeunload', () => { estado.textContent = 'Cargando...'; });
        } catch (e) { /* contenido de otra procedencia: se ignora */ }
        estado.textContent = 'Listo';
        actualizarMonitor(false); // los datos pudieron cambiar (se guardó algo)
    }

    // Recarga una ventana si está mostrando una lista o un ticket.
    // Si tiene un formulario abierto (nuevo, editar, eliminar, anular, caja) no se toca,
    // para no borrar lo que el usuario está escribiendo.
    function refrescarSiEsLista(v) {
        try {
            const ruta = v.iframe.contentWindow.location.pathname.toLowerCase();
            if (/\/(create|edit|delete|anular)(\/|$)/.test(ruta)) return;
            v.el.querySelector('.estado-texto').textContent = 'Actualizando...';
            v.iframe.contentWindow.location.reload();
        } catch (e) { /* la ventana todavía está cargando */ }
    }

    // Otra ventana guardó algo: se actualizan las demás y el monitor
    window.addEventListener('message', (e) => {
        if (e.origin !== window.location.origin || !e.data || e.data.tipo !== 'mv:cambio') return;
        for (const v of abiertas.values()) {
            if (v.iframe.contentWindow !== e.source) refrescarSiEsLista(v);
        }
        actualizarMonitor(false);
    });

    function enfocar(v) {
        for (const o of abiertas.values()) {
            o.el.classList.remove('activa');
            o.tarea.classList.remove('activa');
        }
        v.el.classList.remove('min');
        v.el.classList.add('activa');
        v.tarea.classList.add('activa');
        v.el.style.zIndex = ++zSuperior;
    }

    function minimizar(v) {
        v.el.classList.add('min');
        v.el.classList.remove('activa');
        v.tarea.classList.remove('activa');
        // Enfoca la siguiente ventana visible que esté más arriba
        let siguiente = null;
        for (const o of abiertas.values()) {
            if (!o.el.classList.contains('min') && (!siguiente || +o.el.style.zIndex > +siguiente.el.style.zIndex)) siguiente = o;
        }
        if (siguiente) enfocar(siguiente);
    }

    function restaurar(v) { v.el.classList.remove('min'); enfocar(v); }

    function alternarMaximizar(v) {
        const max = v.el.classList.toggle('max');
        const b = v.el.querySelector('[data-acc="max"]');
        b.innerHTML = max ? '&#10064;' : '&#9633;';
        b.setAttribute('aria-label', max ? 'Restaurar' : 'Maximizar');
    }

    function cerrarVentana(v) {
        v.el.remove();
        v.tarea.remove();
        abiertas.delete(v.id);
    }

    function hacerArrastrable(v, asa) {
        asa.addEventListener('pointerdown', (e) => {
            if (e.button !== 0 || e.target.closest('button') || v.el.classList.contains('max')) return;
            const area = escritorio.getBoundingClientRect();
            const r = v.el.getBoundingClientRect();
            const dx = e.clientX - r.left;
            const dy = e.clientY - r.top;
            asa.setPointerCapture(e.pointerId);
            document.body.classList.add('arrastrando');

            const mover = (ev) => {
                let x = ev.clientX - area.left - dx;
                let y = ev.clientY - area.top - dy;
                x = Math.min(Math.max(x, 80 - r.width), area.width - 80);
                y = Math.min(Math.max(y, 0), area.height - 24);
                v.el.style.left = x + 'px';
                v.el.style.top = y + 'px';
            };
            const soltar = () => {
                asa.removeEventListener('pointermove', mover);
                asa.removeEventListener('pointerup', soltar);
                asa.removeEventListener('pointercancel', soltar);
                document.body.classList.remove('arrastrando');
            };
            asa.addEventListener('pointermove', mover);
            asa.addEventListener('pointerup', soltar);
            asa.addEventListener('pointercancel', soltar);
        });
    }

    // El "Monitor de ventas" también se puede traer al frente
    $('#monitor').addEventListener('pointerdown', () => { $('#monitor').style.zIndex = ++zSuperior; });

    // ==================================================================
    // Íconos del escritorio
    // ==================================================================
    const contIconos = $('#iconos');
    let tipoPuntero = 'mouse';

    function seleccionarIcono(b) {
        contIconos.querySelectorAll('.icono').forEach((i) => i.classList.toggle('sel', i === b));
    }

    MODULOS.forEach((mod, indice) => {
        const b = document.createElement('button');
        b.type = 'button';
        b.className = 'icono';
        b.style.setProperty('--i', indice);   // para que aparezcan uno por uno
        b.setAttribute('role', 'listitem');
        b.innerHTML = icono(mod.icono, 32) + '<span></span>';
        b.querySelector('span').textContent = mod.nombre;
        b.title = mod.url ? 'Doble clic para abrir ' + mod.nombre : mod.nombre + ' (disponible pronto)';
        b.addEventListener('pointerdown', (e) => { tipoPuntero = e.pointerType; });
        b.addEventListener('click', (e) => {
            seleccionarIcono(b);
            // Táctil: un toque abre. Teclado (Enter/Espacio): también abre.
            if (tipoPuntero === 'touch' || e.detail === 0) abrirModulo(mod);
        });
        b.addEventListener('dblclick', () => abrirModulo(mod));
        contIconos.appendChild(b);
    });

    escritorio.addEventListener('pointerdown', (e) => {
        if (e.target === escritorio || e.target === contIconos || e.target === capaVentanas) seleccionarIcono(null);
    });

    // ==================================================================
    // Menú Inicio
    // ==================================================================
    const itemsInicio = $('#menu-items');

    function itemMenu(contenedor, texto, nombreIcono, accion, nota) {
        const b = document.createElement('button');
        b.type = 'button';
        b.className = 'menu-item';
        b.setAttribute('role', 'menuitem');
        b.innerHTML = (nombreIcono ? icono(nombreIcono, 24) : '') + '<span></span>' + (nota ? '<small></small>' : '');
        b.querySelector('span').textContent = texto;
        if (nota) b.querySelector('small').textContent = nota;
        b.addEventListener('click', () => { cerrarMenus(); accion(); });
        contenedor.appendChild(b);
        return b;
    }

    function separador(contenedor) {
        const d = document.createElement('div');
        d.className = 'menu-sep';
        d.setAttribute('role', 'separator');
        contenedor.appendChild(d);
    }

    MODULOS.forEach((mod) => itemMenu(itemsInicio, mod.nombre, mod.icono, () => abrirModulo(mod), mod.url ? '' : 'pronto'));
    separador(itemsInicio);
    itemMenu(itemsInicio, 'Fondo de escritorio', 'fondo', () => abrirPropiedadesFondo());
    itemMenu(itemsInicio, 'Acerca de Monitor', 'info', acercaDe);
    itemMenu(itemsInicio, 'Restablecer sistema...', 'info', () =>
        abrirModulo({ id: 'restablecer', nombre: 'Restablecer sistema', url: '/Sistema/Restablecer', icono: 'info' }));

    function abrirMenuInicio() {
        cerrarMenus();
        menuInicio.hidden = false;
        btnInicio.setAttribute('aria-expanded', 'true');
        const primero = menuInicio.querySelector('.menu-item');
        if (primero) primero.focus();
    }

    btnInicio.addEventListener('click', (e) => {
        e.stopPropagation();
        if (menuInicio.hidden) abrirMenuInicio(); else cerrarMenus();
    });

    // ==================================================================
    // Menú de clic derecho del escritorio
    // ==================================================================
    itemMenu(menuContexto, 'Actualizar monitor', null, () => actualizarMonitor(true));
    itemMenu(menuContexto, 'Fondo de escritorio...', null, () => abrirPropiedadesFondo());
    separador(menuContexto);
    itemMenu(menuContexto, 'Acerca de Monitor', null, acercaDe);

    escritorio.addEventListener('contextmenu', (e) => {
        if (e.target.closest('.ventana')) return;
        e.preventDefault();
        cerrarMenus();
        menuContexto.hidden = false;
        const r = menuContexto.getBoundingClientRect();
        menuContexto.style.left = Math.min(e.clientX, window.innerWidth - r.width - 4) + 'px';
        menuContexto.style.top = Math.min(e.clientY, window.innerHeight - r.height - 34) + 'px';
    });

    function cerrarMenus() {
        menuInicio.hidden = true;
        menuContexto.hidden = true;
        btnInicio.setAttribute('aria-expanded', 'false');
    }

    document.addEventListener('pointerdown', (e) => {
        if (!e.target.closest('#menu-inicio, #btn-inicio, #menu-contexto')) cerrarMenus();
    });
    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape') cerrarMenus();
    });

    // ==================================================================
    // Fondo de escritorio: color o imagen propia (se recuerda en este navegador)
    // ==================================================================
    const CLAVE_COLOR = 'mv-fondo';
    const CLAVE_IMAGEN = 'mv-fondo-img';
    const CLAVE_MODO = 'mv-fondo-modo';
    const MODOS = { ajustar: 'Ajustar a la pantalla', centrar: 'Centrada', mosaico: 'Mosaico' };

    function fondoGuardado() {
        return {
            color: parseInt(almacen.get(CLAVE_COLOR), 10) || 0,
            imagen: almacen.get(CLAVE_IMAGEN) || null,
            modo: MODOS[almacen.get(CLAVE_MODO)] ? almacen.get(CLAVE_MODO) : 'ajustar'
        };
    }

    // Aplica un fondo a un elemento (el escritorio o la vista previa)
    function pintarFondo(el, f) {
        el.style.backgroundColor = FONDOS[f.color % FONDOS.length];
        if (f.imagen) {
            el.style.backgroundImage = 'url("' + f.imagen + '")';
            el.style.backgroundRepeat = f.modo === 'mosaico' ? 'repeat' : 'no-repeat';
            el.style.backgroundPosition = f.modo === 'mosaico' ? 'left top' : 'center';
            el.style.backgroundSize = f.modo === 'ajustar' ? 'cover' : 'auto';
        } else {
            el.style.backgroundImage = 'none';
        }
    }

    function aplicarFondo(f) {
        document.documentElement.style.setProperty('--fondo', FONDOS[f.color % FONDOS.length]);
        pintarFondo(escritorio, f);
    }

    // Guarda el fondo. Devuelve false si la imagen no cupo en el almacenamiento del navegador.
    function guardarFondo(f) {
        try {
            localStorage.setItem(CLAVE_COLOR, String(f.color));
            localStorage.setItem(CLAVE_MODO, f.modo);
            if (f.imagen) localStorage.setItem(CLAVE_IMAGEN, f.imagen);
            else localStorage.removeItem(CLAVE_IMAGEN);
            return true;
        } catch (e) {
            return false;
        }
    }

    // Reduce la imagen (máx. 1920x1080) y la convierte a JPEG para que ocupe poco
    function prepararImagen(archivo) {
        return new Promise((resolver, rechazar) => {
            if (!archivo.type || !archivo.type.startsWith('image/')) { rechazar(new Error('El archivo no es una imagen.')); return; }
            if (archivo.size > 20 * 1024 * 1024) { rechazar(new Error('La imagen pesa más de 20 MB.')); return; }
            const url = URL.createObjectURL(archivo);
            const img = new Image();
            img.onload = () => {
                const escala = Math.min(1, 1920 / img.width, 1080 / img.height);
                const w = Math.max(1, Math.round(img.width * escala));
                const h = Math.max(1, Math.round(img.height * escala));
                const lienzo = document.createElement('canvas');
                lienzo.width = w;
                lienzo.height = h;
                lienzo.getContext('2d').drawImage(img, 0, 0, w, h);
                URL.revokeObjectURL(url);
                resolver(lienzo.toDataURL('image/jpeg', 0.85));
            };
            img.onerror = () => { URL.revokeObjectURL(url); rechazar(new Error('No se pudo leer la imagen.')); };
            img.src = url;
        });
    }

    // Ventana "Propiedades de fondo" (vista previa en un monitor pequeño)
    function abrirPropiedadesFondo() {
        cerrarMenus();
        const original = fondoGuardado();
        const temp = Object.assign({}, original);
        const capa = $('#capa-dialogo');

        capa.innerHTML =
            '<section class="ventana activa dialogo dialogo-fondo" role="dialog" aria-modal="true" aria-labelledby="pf-t">' +
            '  <div class="ventana-titulo"><span class="t" id="pf-t">Fondo de escritorio</span>' +
            '    <button type="button" class="cerrar" aria-label="Cerrar">&times;</button></div>' +
            '  <div class="pf-cuerpo">' +
            '    <div class="pf-monitor"><div class="pf-pantalla" id="pf-pantalla"></div><div class="pf-base"></div></div>' +
            '    <fieldset class="pf-grupo"><legend>Color</legend><div class="pf-colores" id="pf-colores"></div></fieldset>' +
            '    <fieldset class="pf-grupo"><legend>Imagen</legend>' +
            '      <div class="pf-fila"><span id="pf-nombre" class="pf-nombre">(ninguna)</span></div>' +
            '      <div class="pf-fila">' +
            '        <button type="button" class="boton95" id="pf-examinar">Examinar...</button>' +
            '        <button type="button" class="boton95" id="pf-quitar">Quitar</button>' +
            '        <select id="pf-modo" class="pf-select" aria-label="Cómo mostrar la imagen"></select>' +
            '      </div>' +
            '      <input type="file" id="pf-archivo" accept="image/*" hidden>' +
            '      <div class="pf-error" id="pf-error" role="alert"></div>' +
            '    </fieldset>' +
            '  </div>' +
            '  <div class="dialogo-botones">' +
            '    <button type="button" class="boton95" id="pf-aceptar">Aceptar</button>' +
            '    <button type="button" class="boton95" id="pf-cancelar">Cancelar</button>' +
            '    <button type="button" class="boton95" id="pf-aplicar">Aplicar</button>' +
            '  </div>' +
            '</section>';
        capa.hidden = false;

        const pantalla = capa.querySelector('#pf-pantalla');
        const nombre = capa.querySelector('#pf-nombre');
        const errorEl = capa.querySelector('#pf-error');
        const selModo = capa.querySelector('#pf-modo');
        const archivo = capa.querySelector('#pf-archivo');

        Object.keys(MODOS).forEach((k) => {
            const op = document.createElement('option');
            op.value = k;
            op.textContent = MODOS[k];
            selModo.appendChild(op);
        });

        const contColores = capa.querySelector('#pf-colores');
        FONDOS.forEach((c, i) => {
            const b = document.createElement('button');
            b.type = 'button';
            b.className = 'pf-color';
            b.style.background = c;
            b.setAttribute('aria-label', 'Color ' + (i + 1));
            b.addEventListener('click', () => { temp.color = i; refrescar(); });
            contColores.appendChild(b);
        });

        function refrescar() {
            pintarFondo(pantalla, temp);
            contColores.querySelectorAll('.pf-color').forEach((b, i) => b.classList.toggle('sel', i === temp.color));
            nombre.textContent = temp.imagen ? (temp.nombreArchivo || 'Imagen guardada') : '(ninguna)';
            selModo.value = temp.modo;
            selModo.disabled = !temp.imagen;
            capa.querySelector('#pf-quitar').disabled = !temp.imagen;
        }

        capa.querySelector('#pf-examinar').addEventListener('click', () => archivo.click());
        archivo.addEventListener('change', async () => {
            errorEl.textContent = '';
            const f = archivo.files && archivo.files[0];
            if (!f) return;
            nombre.textContent = 'Cargando...';
            try {
                temp.imagen = await prepararImagen(f);
                temp.nombreArchivo = f.name;
            } catch (err) {
                errorEl.textContent = err.message;
            }
            archivo.value = '';
            refrescar();
        });
        capa.querySelector('#pf-quitar').addEventListener('click', () => { temp.imagen = null; temp.nombreArchivo = null; refrescar(); });
        selModo.addEventListener('change', () => { temp.modo = selModo.value; refrescar(); });

        function aplicar() {
            errorEl.textContent = '';
            if (!guardarFondo(temp)) {
                errorEl.textContent = 'La imagen es demasiado grande para guardarse en este navegador. Pruebe con una más liviana.';
                return false;
            }
            aplicarFondo(temp);
            return true;
        }
        function cerrarVentana() {
            capa.hidden = true;
            capa.innerHTML = '';
            document.removeEventListener('keydown', teclas, true);
        }
        const teclas = (e) => {
            if (e.key === 'Escape') { e.preventDefault(); e.stopPropagation(); aplicarFondo(fondoGuardado()); cerrarVentana(); }
        };
        document.addEventListener('keydown', teclas, true);

        capa.querySelector('#pf-aplicar').addEventListener('click', aplicar);
        capa.querySelector('#pf-aceptar').addEventListener('click', () => { if (aplicar()) cerrarVentana(); });
        const cancelar = () => { aplicarFondo(fondoGuardado()); cerrarVentana(); };
        capa.querySelector('#pf-cancelar').addEventListener('click', cancelar);
        capa.querySelector('.cerrar').addEventListener('click', cancelar);

        refrescar();
        capa.querySelector('#pf-examinar').focus();
    }

    aplicarFondo(fondoGuardado());

    // ==================================================================
    // Cuadros de diálogo propios
    // ==================================================================
    function dialogo(titulo, mensaje, tipo) {
        const capa = $('#capa-dialogo');
        capa.innerHTML =
            '<section class="ventana activa dialogo" role="alertdialog" aria-modal="true" aria-labelledby="dlg-t" aria-describedby="dlg-m">' +
            '  <div class="ventana-titulo"><span class="t" id="dlg-t"></span>' +
            '    <button type="button" class="cerrar" aria-label="Cerrar">&times;</button></div>' +
            '  <div class="dialogo-cuerpo">' + icono(tipo || 'info', 32) + '<p id="dlg-m"></p></div>' +
            '  <div class="dialogo-botones"><button type="button" class="boton95" id="dlg-ok">Aceptar</button></div>' +
            '</section>';
        capa.querySelector('#dlg-t').textContent = titulo;
        capa.querySelector('#dlg-m').textContent = mensaje;
        capa.hidden = false;

        const cerrarDialogo = () => {
            capa.hidden = true;
            capa.innerHTML = '';
            document.removeEventListener('keydown', teclas, true);
        };
        const teclas = (e) => {
            if (e.key === 'Escape' || e.key === 'Enter') { e.preventDefault(); e.stopPropagation(); cerrarDialogo(); }
        };
        capa.querySelector('#dlg-ok').addEventListener('click', cerrarDialogo);
        capa.querySelector('.cerrar').addEventListener('click', cerrarDialogo);
        document.addEventListener('keydown', teclas, true);
        capa.querySelector('#dlg-ok').focus();
    }

    function acercaDe() {
        dialogo('Acerca de Monitor',
            'Monitor 1.0. Sistema de monitoreo de ventas para supermercado. Proyecto final integrador, 2026.',
            'monitor');
    }

    // ==================================================================
    // Reloj
    // ==================================================================
    function reloj() {
        const ahora = new Date();
        const r = $('#reloj');
        r.textContent = ahora.toLocaleTimeString('es-SV', { hour: '2-digit', minute: '2-digit' });
        r.title = ahora.toLocaleDateString('es-SV', { weekday: 'long', day: 'numeric', month: 'long', year: 'numeric' });
    }
    reloj();
    setInterval(reloj, 10000);

    // ==================================================================
    // Globos de aviso junto al reloj
    // ==================================================================
    let ultimoAviso = null;
    let temporizadorGlobo = null;
    $('#btn-avisos').innerHTML = icono('campana', 16);

    function globo(titulo, texto) {
        ultimoAviso = { titulo, texto };
        $('#globo-titulo').textContent = titulo;
        $('#globo-texto').textContent = texto;
        $('#globo').hidden = false;
        clearTimeout(temporizadorGlobo);
        temporizadorGlobo = setTimeout(() => { $('#globo').hidden = true; }, 8000);
    }
    $('#globo .x').addEventListener('click', () => { $('#globo').hidden = true; });
    $('#btn-avisos').addEventListener('click', () => {
        if (ultimoAviso) globo(ultimoAviso.titulo, ultimoAviso.texto);
        else globo('Sin avisos', 'No hay avisos nuevos.');
    });

    // ==================================================================
    // Monitor de ventas: se actualiza solo y avisa de cambios
    // ==================================================================
    const moneda = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' });
    let anterior = null;

    async function actualizarMonitor(manual) {
        try {
            const resp = await fetch('/Home/Estado', { cache: 'no-store', headers: { 'Accept': 'application/json' } });
            if (!resp.ok) throw new Error('HTTP ' + resp.status);
            const e = await resp.json();

            $('#m-ventas').textContent = e.ventasHoy;
            $('#m-monto').textContent = moneda.format(e.montoHoy);
            $('#m-clientes').textContent = e.totalClientes;
            $('#m-stock').textContent = e.productosStockBajo;
            $('#m-hora').textContent = 'Actualizado: ' +
                new Date().toLocaleTimeString('es-SV', { hour: '2-digit', minute: '2-digit', second: '2-digit' });

            if (anterior && e.ventasHoy > anterior.ventasHoy) {
                globo('Nueva venta', 'Se registraron ' + (e.ventasHoy - anterior.ventasHoy) +
                    ' venta(s). Total de hoy: ' + moneda.format(e.montoHoy) + '.');
            } else if (anterior && e.productosStockBajo > anterior.productosStockBajo) {
                globo('Stock bajo', e.productosStockBajo + ' producto(s) con ' + e.umbralStockBajo + ' unidades o menos.');
            } else if (manual) {
                globo('Monitor actualizado', 'Los datos del monitor de ventas están al día.');
            }
            anterior = e;
        } catch (err) {
            $('#m-hora').textContent = 'Sin conexión con el servidor';
            if (manual) dialogo('Monitor de ventas', 'No se pudo conectar con el servidor. Verifique que la aplicación esté en ejecución.', 'aviso');
        }
    }

    $('#monitor-actualizar').addEventListener('click', () => actualizarMonitor(true));
    actualizarMonitor(false);
    setInterval(() => actualizarMonitor(false), INTERVALO_MONITOR);

    // ==================================================================
    // Entrada del escritorio al terminar el arranque: la barra sube, los
    // íconos aparecen uno por uno y el monitor se enciende contando desde 0
    // ==================================================================
    function contar(el, final, formato) {
        const duracion = 900;
        const inicio = performance.now();
        const paso = (t) => {
            const p = Math.min(1, (t - inicio) / duracion);
            el.textContent = formato(final * (1 - Math.pow(1 - p, 3)));
            if (p < 1) requestAnimationFrame(paso);
        };
        el.textContent = formato(0);
        requestAnimationFrame(paso);
    }

    function entradaEscritorio() {
        const html = document.documentElement;
        html.classList.remove('arrancando');
        html.classList.add('entrando');

        const valor = (id, campo) => anterior ? Number(anterior[campo]) : parseFloat($(id).dataset.valor || '0');
        const entero = (v) => String(Math.round(v));
        $('#m-ventas').textContent = '0';
        $('#m-clientes').textContent = '0';
        $('#m-stock').textContent = '0';
        $('#m-monto').textContent = moneda.format(0);

        setTimeout(() => {
            contar($('#m-ventas'), valor('#m-ventas', 'ventasHoy'), entero);
            contar($('#m-monto'), valor('#m-monto', 'montoHoy'), (v) => moneda.format(v));
            contar($('#m-clientes'), valor('#m-clientes', 'totalClientes'), entero);
            contar($('#m-stock'), valor('#m-stock', 'productosStockBajo'), entero);
        }, 1350);
        setTimeout(() => html.classList.remove('entrando'), 2800);
    }

    if (document.documentElement.classList.contains('arrancando')) {
        document.addEventListener('mv:arranque-revelar', entradaEscritorio, { once: true });
    }

    // ==================================================================
    // Funciones que el escritorio comparte con Moni (wwwroot/js/moni.js)
    // ==================================================================
    window.Escritorio = {
        modulos: MODULOS,
        abrir(id) { const m = MODULOS.find((x) => x.id === id); if (m) abrirModulo(m); },
        icono(id) { return contIconos.children[MODULOS.findIndex((x) => x.id === id)] || null; },
        ventana(id) { return abiertas.get(id) || null; },      // { el, iframe, modulo } o null
        alFrente(id) { const v = abiertas.get(id); if (v) restaurar(v); },
        agregarAlMenu(texto, nombreIcono, accion) { return itemMenu(itemsInicio, texto, nombreIcono, accion); },
        separadorMenu() { separador(itemsInicio); },
        dibujo: (nombre, tam) => icono(nombre, tam),       // ícono en pixel art como texto SVG
        ventanas() { return [...abiertas.values()]; },
        cerrar(id) { const v = abiertas.get(id); if (v) cerrarVentana(v); },
        globo,
        cerrarMenus
    };

    // ==================================================================
    // Bienvenida (una vez por sesión, al terminar la animación)
    // ==================================================================
    function bienvenida() {
        if (window.Moni) return; // Moni da la bienvenida
        if (almacen.get('mv-bienvenida', true)) return;
        almacen.set('mv-bienvenida', '1', true);
        globo('Bienvenido a Monitor',
            'Haga doble clic en un ícono para abrir un módulo, o use el botón Inicio. Clic derecho en el escritorio para más opciones.');
    }
    document.addEventListener('mv:arranque-fin', () => setTimeout(bienvenida, 500));
    if (document.documentElement.classList.contains('sin-arranque')) setTimeout(bienvenida, 800);
    // ==================================================================
    // Asistente IA: avisa si hay productos por agotarse (al entrar y cada 10 min)
    // ==================================================================
    async function avisoAsistente() {
        if (window.Moni && window.Moni.ocupado()) { setTimeout(avisoAsistente, 20000); return; } // no interrumpir a Moni
        try {
            const resp = await fetch('/Asistente/Alertas', { cache: 'no-store', headers: { 'Accept': 'application/json' } });
            if (!resp.ok) return;
            const a = await resp.json();
            if (!a.total) return;
            const p = a.productos[0];
            const cuando = p.stock === 0 ? 'ya se agotó' : p.dias === 0 ? 'se agota hoy' : 'se agota en ' + p.dias + ' día(s)';
            globo('Asistente IA', p.nombre + ' ' + cuando + '. ' +
                (a.total > 1 ? 'Hay ' + a.total + ' productos críticos. ' : '') +
                'Abra "Asistente IA" para ver la compra sugerida.');
        } catch (err) { /* sin aviso */ }
    }
    document.addEventListener('mv:arranque-fin', () => setTimeout(avisoAsistente, 9000));
    if (document.documentElement.classList.contains('sin-arranque')) setTimeout(avisoAsistente, 9000);
    setInterval(avisoAsistente, 10 * 60 * 1000);
})();
