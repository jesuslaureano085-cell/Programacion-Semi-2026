// =====================================================================
// Monitor — Nueva compra a proveedor
// Arma la lista de productos de la factura en el navegador.
// Solo se muestran los productos del proveedor elegido.
// El servidor vuelve a validar todo antes de guardar y subir el stock.
// =====================================================================
(function () {
    'use strict';

    const $ = (s) => document.querySelector(s);
    const moneda = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' });

    const catalogo = JSON.parse($('#pc-datos').textContent || '[]');
    const porId = new Map(catalogo.map((p) => [p.ProductoId, p]));

    const selProveedor = $('#ProveedorId');
    const entradaProducto = $('#pc-producto');
    const entradaCantidad = $('#pc-cantidad');
    const entradaCosto = $('#pc-costo');
    const lista = $('#pc-lista');
    const info = $('#pc-info');
    const error = $('#pc-error');
    const cuerpo = $('#pc-lineas');
    const filaVacia = cuerpo.querySelector('.fila-vacia');

    // Renglones: [{ id, cantidad, costo }]
    let lineas = [];

    function proveedorActual() { return Number(selProveedor.value) || 0; }
    function delProveedor() { return catalogo.filter((p) => p.ProveedorId === proveedorActual()); }
    function etiqueta(p) { return p.Nombre + ' · #' + p.ProductoId; }
    function mostrarError(msg) { error.textContent = msg || ''; }

    // ---------- Sugerencias según el proveedor ----------
    function llenarLista() {
        lista.innerHTML = '';
        const productos = delProveedor();
        productos.forEach((p) => {
            const op = document.createElement('option');
            op.value = etiqueta(p);
            op.label = 'stock ' + p.Stock + (p.UltimoCosto ? ' · último costo ' + moneda.format(p.UltimoCosto) : '');
            lista.appendChild(op);
        });
        entradaProducto.disabled = proveedorActual() === 0;
        entradaProducto.placeholder = proveedorActual() === 0
            ? 'Primero elija el proveedor'
            : (productos.length ? 'Escriba para buscar...' : 'Este proveedor no tiene productos registrados');
    }

    function buscarProducto(texto) {
        const t = texto.trim();
        if (!t) return null;
        const m = t.match(/#(\d+)$/);
        let p = null;
        if (m) p = porId.get(Number(m[1])) || null;
        if (!p) p = catalogo.find((x) => x.Codigo && x.Codigo === t) || null;
        if (!p) {
            const normal = (s) => s.normalize('NFD').replace(/[̀-ͯ]/g, '').toLowerCase();
            const iguales = delProveedor().filter((x) => normal(x.Nombre) === normal(t));
            p = iguales.length === 1 ? iguales[0] : null;
        }
        return p;
    }

    // Al cambiar de proveedor, la lista se vacía (una factura es de un solo proveedor)
    selProveedor.addEventListener('change', () => {
        if (lineas.length) {
            lineas = [];
            dibujar();
            info.textContent = 'Se vació la lista porque cambió el proveedor.';
        }
        llenarLista();
    });

    // Al elegir un producto se sugiere su último costo
    entradaProducto.addEventListener('input', () => {
        const p = buscarProducto(entradaProducto.value);
        if (p && p.ProveedorId === proveedorActual()) {
            if (p.UltimoCosto && !entradaCosto.value) entradaCosto.value = p.UltimoCosto.toFixed(2);
            info.textContent = p.Nombre + ' · stock actual ' + p.Stock + ' · precio de venta ' + moneda.format(p.Precio);
        } else {
            info.textContent = ' ';
        }
    });

    // ---------- Agregar ----------
    function agregar() {
        mostrarError('');
        if (proveedorActual() === 0) { mostrarError('Primero elija el proveedor.'); selProveedor.focus(); return; }

        const p = buscarProducto(entradaProducto.value);
        if (!p) { mostrarError('Seleccione un producto de la lista.'); entradaProducto.focus(); return; }
        if (p.ProveedorId !== proveedorActual()) { mostrarError('"' + p.Nombre + '" no es de este proveedor.'); return; }

        const cant = parseInt(entradaCantidad.value, 10);
        if (!Number.isInteger(cant) || cant < 1) { mostrarError('La cantidad debe ser un número entero mayor que cero.'); return; }

        const costo = Math.round(parseFloat(entradaCosto.value) * 100) / 100;
        if (!(costo > 0)) { mostrarError('Escriba el costo unitario (mayor que cero).'); entradaCosto.focus(); return; }

        const existente = lineas.find((l) => l.id === p.ProductoId);
        if (existente) { existente.cantidad += cant; existente.costo = costo; }
        else lineas.push({ id: p.ProductoId, cantidad: cant, costo: costo });

        entradaProducto.value = '';
        entradaCantidad.value = 1;
        entradaCosto.value = '';
        info.textContent = 'Agregado: ' + cant + ' x ' + p.Nombre;
        dibujar(p.ProductoId);
        entradaProducto.focus();
    }

    // ---------- Tabla y total ----------
    function dibujar(resaltar) {
        cuerpo.querySelectorAll('tr:not(.fila-vacia)').forEach((tr) => tr.remove());
        filaVacia.classList.toggle('d-none', lineas.length > 0);

        let total = 0, unidades = 0;
        lineas.forEach((l) => {
            const p = porId.get(l.id);
            if (!p) return;
            const sub = l.costo * l.cantidad;
            total += sub;
            unidades += l.cantidad;

            const tr = document.createElement('tr');
            if (l.id === resaltar) tr.className = 'recien';
            tr.innerHTML =
                '<td><span class="nombre"></span><div class="aviso-margen text-danger small"></div></td>' +
                '<td class="text-end"></td>' +
                '<td class="text-center"><input type="number" class="form-control form-control-sm text-center" min="1" step="1"></td>' +
                '<td class="text-center"><input type="number" class="form-control form-control-sm text-end" min="0.01" step="0.01"></td>' +
                '<td class="text-end fw-bold"></td>' +
                '<td class="text-end"><button type="button" class="btn btn-sm btn-outline-danger">Quitar</button></td>';
            tr.querySelector('.nombre').textContent = p.Nombre;
            tr.cells[1].textContent = p.Stock + ' → ' + (p.Stock + l.cantidad);
            tr.cells[4].textContent = moneda.format(sub);

            // Aviso de negocio: comprar más caro de lo que se vende = pérdida
            if (l.costo >= p.Precio) {
                tr.querySelector('.aviso-margen').textContent =
                    'Atención: el costo es mayor o igual al precio de venta (' + moneda.format(p.Precio) + ').';
            }

            const [inpCant, inpCosto] = tr.querySelectorAll('input');
            inpCant.value = l.cantidad;
            inpCosto.value = l.costo.toFixed(2);
            inpCant.setAttribute('aria-label', 'Cantidad de ' + p.Nombre);
            inpCosto.setAttribute('aria-label', 'Costo unitario de ' + p.Nombre);
            inpCant.addEventListener('change', () => {
                const n = parseInt(inpCant.value, 10);
                if (!Number.isInteger(n) || n < 1) { mostrarError('La cantidad debe ser al menos 1.'); inpCant.value = l.cantidad; return; }
                mostrarError(''); l.cantidad = n; dibujar();
            });
            inpCosto.addEventListener('change', () => {
                const c = Math.round(parseFloat(inpCosto.value) * 100) / 100;
                if (!(c > 0)) { mostrarError('El costo debe ser mayor que cero.'); inpCosto.value = l.costo.toFixed(2); return; }
                mostrarError(''); l.costo = c; dibujar();
            });
            tr.querySelector('button').addEventListener('click', () => {
                lineas = lineas.filter((x) => x.id !== l.id);
                dibujar();
            });
            cuerpo.insertBefore(tr, filaVacia);
        });

        $('#pc-total').textContent = moneda.format(total);
        $('#pc-resumen').textContent = unidades + (unidades === 1 ? ' unidad' : ' unidades') +
            ' · ' + lineas.length + (lineas.length === 1 ? ' producto' : ' productos');
    }

    // ---------- Guardar ----------
    $('#form-compra').addEventListener('submit', (e) => {
        mostrarError('');
        const problemas = [];
        if (proveedorActual() === 0) problemas.push('Seleccione el proveedor.');
        if (!$('#NumeroFactura').value.trim()) problemas.push('Escriba el número de factura.');
        if (!(Number($('#EmpleadoId').value) > 0)) problemas.push('Seleccione quién recibe.');
        if (lineas.length === 0) problemas.push('Agregue al menos un producto.');
        if (problemas.length) { e.preventDefault(); mostrarError(problemas.join(' ')); return; }

        const ocultos = $('#pc-ocultos');
        ocultos.innerHTML = '';
        lineas.forEach((l, i) => {
            [['ProductoId', l.id], ['Cantidad', l.cantidad], ['CostoUnitario', l.costo.toFixed(2)]].forEach(([campo, valor]) => {
                const h = document.createElement('input');
                h.type = 'hidden';
                h.name = 'Lineas[' + i + '].' + campo;
                h.value = valor;
                ocultos.appendChild(h);
            });
        });
        $('#pc-guardar').disabled = true;
        $('#pc-guardar').textContent = 'Guardando...';
    });

    // ---------- Teclado ----------
    $('#pc-agregar').addEventListener('click', agregar);
    [entradaProducto, entradaCantidad, entradaCosto].forEach((el) => {
        el.addEventListener('keydown', (e) => { if (e.key === 'Enter') { e.preventDefault(); agregar(); } });
    });

    // Si el servidor devolvió el formulario con errores, se recuperan los renglones
    try {
        JSON.parse($('#pc-iniciales').textContent || '[]').forEach((l) => {
            const id = l.ProductoId, cant = l.Cantidad, costo = Number(l.CostoUnitario);
            if (porId.has(id) && cant > 0 && costo > 0) lineas.push({ id, cantidad: cant, costo });
        });
    } catch (err) { /* sin renglones */ }

    llenarLista();
    dibujar();
})();
