document.addEventListener("DOMContentLoaded", () => {
    mnxAcademica.addEventListener("click", event => {
        event.preventDefault();
        const form = event.target.dataset.form;
        if (form) {
            abrirVentana(form);
        }
    });

    $(document).on("click", ".btn-close[data-form]", e => {
        $(`#${e.currentTarget.dataset.form}`).hide();
    });
});

function abrirVentana(form) {
    const $ventana = $(`#${form}`);

    if ($ventana.find("form").length > 0) {
        $ventana.show();
        return Promise.resolve(true);
    }

    return fetch(`/views/${form}.html`)
        .then(resp => {
            if (!resp.ok) throw new Error(`No existe la ventana "${form}" todavía`);
            return resp.text();
        })
        .then(vista => {
            $ventana.html(vista).show().draggable({ handle: ".card-header" });
            return true;
        })
        .catch(error => {
            alertify.error(error.message);
            return false;
        });
}

async function obtenerDatos(buscar = '', form = 'Alumnos') {
    const respuesta = await fetch(`/api/${form}/buscar?buscar=${encodeURIComponent(buscar)}`),
        datos = await respuesta.json();
    return datos;
}

async function obtenerAlumnos(buscar = '') {
    return obtenerDatos(buscar, 'Alumnos');
}

function refrescarBusquedaAlumnos() {
    const txtBuscar = document.getElementById('txtBuscarAlumno');
    if (txtBuscar && typeof listarAlumnos === 'function') {
        listarAlumnos(txtBuscar.value);
    }
}

async function obtenerMaterias(buscar = '') {
    return obtenerDatos(buscar, 'Materias');
}

function refrescarBusquedaMaterias() {
    const txtBuscar = document.getElementById('txtBuscarMateria');
    if (txtBuscar && typeof listarMaterias === 'function') {
        listarMaterias(txtBuscar.value);
    }
}

async function obtenerDocentes(buscar = '') {
    return obtenerDatos(buscar, 'Docentes');
}

function refrescarBusquedaDocentes() {
    const txtBuscar = document.getElementById('txtBuscarDocente');
    if (txtBuscar && typeof listarDocentes === 'function') {
        listarDocentes(txtBuscar.value);
    }
}

function escaparHtml(texto) {
    return String(texto ?? '').replace(/[&<>"']/g, c => ({
        '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;'
    }[c]));
}
