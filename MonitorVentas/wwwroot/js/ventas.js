// =====================================================================
// Monitor — Caja (Nueva venta)
// Arma la lista de productos de la venta en el navegador y, al cobrar,
// la envía al servidor. El servidor vuelve a validar todo (precio, stock,
// cajero y método de pago), así que nada de esto se puede "saltar".
// =====================================================================
(function () {
    'use strict';

    const $ = (s) => document.querySelector(s);
    const moneda = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' });

    const catalogo = JSON.parse($('#pv-datos').textContent || '[]');
    const porId = new Map(catalogo.map((p) => [p.ProductoId, p]));

    const entradaProducto = $('#pv-producto');
    const entradaCantidad = $('#pv-cantidad');
    const info = $('#pv-info');
    const error = $('#pv-error');
    const cuerpo = $('#pv-lineas');
    const filaVacia = cuerpo.querySelector('.fila-vacia');

    // Renglones de la venta: [{ id, cantidad }]
    let lineas = [];

    // ---------- Lista de sugerencias (datalist) ----------
    function etiqueta(p) { return p.Nombre + ' · #' + p.ProductoId; }

    const lista = $('#pv-lista');
    catalogo.forEach((p) => {
        const op = document.createElement('option');
        op.value = etiqueta(p);
        op.label = moneda.format(p.PrecioFinal) + (p.Descuento ? ' (-' + p.Descuento + '%)' : '') +
            ' · stock ' + p.Stock + (p.Codigo ? ' · ' + p.Codigo : '');
        lista.appendChild(op);
    });

    if (catalogo.length === 0) {
        info.textContent = 'No hay productos registrados. Primero registre productos en el módulo Productos.';
    }

    // Encuentra el producto a partir de lo que se escribió:
    // "Nombre · #id" (de la lista), código de barras de 13 dígitos o el nombre exacto.
    function buscarProducto(texto) {
        const t = texto.trim();
        if (!t) return null;
        const m = t.match(/#(\d+)$/);
        if (m) return porId.get(Number(m[1])) || null;
        const porCodigo = catalogo.find((p) => p.Codigo && p.Codigo === t);
        if (porCodigo) return porCodigo;
        const normal = (s) => s.normalize('NFD').replace(/[̀-ͯ]/g, '').toLowerCase();
        const iguales = catalogo.filter((p) => normal(p.Nombre) === normal(t));
        return iguales.length === 1 ? iguales[0] : null;
    }

    function cantidadEnVenta(id) {
        const l = lineas.find((x) => x.id === id);
        return l ? l.cantidad : 0;
    }

    function mostrarError(msg) { error.textContent = msg || ''; }

    // ---------- Agregar ----------
    function agregar() {
        mostrarError('');
        const p = buscarProducto(entradaProducto.value);
        if (!p) {
            mostrarError('Seleccione un producto de la lista o escriba su código de barras.');
            entradaProducto.focus();
            return;
        }
        const cant = parseInt(entradaCantidad.value, 10);
        if (!Number.isInteger(cant) || cant < 1) {
            mostrarError('La cantidad debe ser un número entero mayor que cero.');
            entradaCantidad.focus();
            return;
        }
        const total = cantidadEnVenta(p.ProductoId) + cant;
        if (total > p.Stock) {
            mostrarError('Stock insuficiente de "' + p.Nombre + '": hay ' + p.Stock + ' y en la venta van ' + total + '.');
            return;
        }

        const existente = lineas.find((x) => x.id === p.ProductoId);
        if (existente) existente.cantidad = total;
        else lineas.push({ id: p.ProductoId, cantidad: cant });

        entradaProducto.value = '';
        entradaCantidad.value = 1;
        info.textContent = 'Agregado: ' + cant + ' x ' + p.Nombre;
        dibujar(p.ProductoId);
        entradaProducto.focus();
    }

    // ---------- Dibujar la tabla y el total ----------
    function dibujar(resaltar) {
        cuerpo.querySelectorAll('tr:not(.fila-vacia)').forEach((tr) => tr.remove());
        filaVacia.classList.toggle('d-none', lineas.length > 0);

        let total = 0, articulos = 0;
        lineas.forEach((l) => {
            const p = porId.get(l.id);
            if (!p) return;
            const sub = p.PrecioFinal * l.cantidad;   // precio con la promoción vigente
            total += sub;
            articulos += l.cantidad;

            const tr = document.createElement('tr');
            if (l.id === resaltar) tr.className = 'recien';
            tr.innerHTML =
                '<td><span class="nombre"></span><div class="form-text m-0"></div></td>' +
                '<td class="text-end"></td>' +
                '<td class="text-center"><input type="number" class="form-control form-control-sm text-center" min="1" step="1"></td>' +
                '<td class="text-end fw-bold"></td>' +
                '<td class="text-end"><button type="button" class="btn btn-sm btn-outline-danger" title="Quitar">Quitar</button></td>';
            tr.querySelector('.nombre').textContent = p.Nombre;
            tr.querySelector('.form-text').textContent = (p.Categoria || '') + ' · stock ' + p.Stock;
            if (p.Descuento > 0) {
                const antes = document.createElement('s');
                antes.className = 'precio-antes';
                antes.textContent = moneda.format(p.Precio);
                const etiqueta = document.createElement('span');
                etiqueta.className = 'descuento';
                etiqueta.textContent = '-' + p.Descuento + '%';
                tr.cells[1].append(antes, ' ', moneda.format(p.PrecioFinal), ' ', etiqueta);
            } else {
                tr.cells[1].textContent = moneda.format(p.PrecioFinal);
            }
            tr.cells[3].textContent = moneda.format(sub);

            const inp = tr.querySelector('input');
            inp.value = l.cantidad;
            inp.max = p.Stock;
            inp.setAttribute('aria-label', 'Cantidad de ' + p.Nombre);
            inp.addEventListener('change', () => {
                const n = parseInt(inp.value, 10);
                if (!Number.isInteger(n) || n < 1) {
                    mostrarError('La cantidad debe ser al menos 1. Use "Quitar" para eliminar el producto.');
                    inp.value = l.cantidad;
                    return;
                }
                if (n > p.Stock) {
                    mostrarError('Stock insuficiente de "' + p.Nombre + '": hay ' + p.Stock + '.');
                    inp.value = l.cantidad;
                    return;
                }
                mostrarError('');
                l.cantidad = n;
                dibujar();
            });
            tr.querySelector('button').addEventListener('click', () => {
                lineas = lineas.filter((x) => x.id !== l.id);
                info.textContent = 'Quitado: ' + p.Nombre;
                dibujar();
            });
            cuerpo.insertBefore(tr, filaVacia);
        });

        $('#pv-total').textContent = moneda.format(total);
        $('#pv-resumen').textContent = articulos + (articulos === 1 ? ' artículo' : ' artículos') +
            ' · ' + lineas.length + (lineas.length === 1 ? ' producto' : ' productos');
    }

    // ---------- Cobrar ----------
    $('#form-venta').addEventListener('submit', (e) => {
        mostrarError('');
        const problemas = [];
        if (lineas.length === 0) problemas.push('Agregue al menos un producto.');
        if (!(Number($('#EmpleadoId').value) > 0)) problemas.push('Seleccione el cajero.');
        if (!(Number($('#MetodoPagoId').value) > 0)) problemas.push('Seleccione el método de pago.');
        if (problemas.length) {
            e.preventDefault();
            mostrarError(problemas.join(' '));
            return;
        }

        // Se envían solo producto y cantidad; el precio lo pone el servidor.
        const ocultos = $('#pv-ocultos');
        ocultos.innerHTML = '';
        lineas.forEach((l, i) => {
            [['ProductoId', l.id], ['Cantidad', l.cantidad]].forEach(([campo, valor]) => {
                const h = document.createElement('input');
                h.type = 'hidden';
                h.name = 'Lineas[' + i + '].' + campo;
                h.value = valor;
                ocultos.appendChild(h);
            });
        });
        $('#pv-cobrar').disabled = true;            // evita cobrar dos veces
        $('#pv-cobrar').textContent = 'Procesando...';
    });

    // ---------- Teclado ----------
    $('#pv-agregar').addEventListener('click', agregar);
    [entradaProducto, entradaCantidad].forEach((el) => {
        el.addEventListener('keydown', (e) => {
            if (e.key === 'Enter') { e.preventDefault(); agregar(); }   // también sirve con lector de código de barras
        });
    });
    entradaProducto.addEventListener('input', () => {
        const p = buscarProducto(entradaProducto.value);
        info.textContent = p
            ? p.Nombre + ' · ' + moneda.format(p.PrecioFinal) + (p.Descuento ? ' (promoción -' + p.Descuento + '%)' : '') +
              ' · disponibles: ' + (p.Stock - cantidadEnVenta(p.ProductoId))
            : ' ';
    });
    document.addEventListener('keydown', (e) => {
        if (e.key === 'F2') { e.preventDefault(); entradaProducto.focus(); }
    });

    // Si el servidor devolvió la venta por algún error, se vuelven a cargar sus renglones
    try {
        const iniciales = JSON.parse($('#pv-iniciales').textContent || '[]');
        iniciales.forEach((l) => {
            const id = l.ProductoId ?? l.productoId;
            const cant = l.Cantidad ?? l.cantidad;
            if (porId.has(id) && cant > 0) lineas.push({ id, cantidad: cant });
        });
    } catch (err) { /* sin renglones iniciales */ }

    dibujar();
    entradaProducto.focus();
})();
