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

async function obtenerAlumnos(buscar = '') {
    const respuesta = await fetch(`/api/Alumnos/buscar?buscar=${encodeURIComponent(buscar)}`),
        alumnos = await respuesta.json();
    return alumnos;
}

function refrescarBusquedaAlumnos() {
    const txtBuscar = document.getElementById('txtBuscarAlumno');
    if (txtBuscar && typeof listarAlumnos === 'function') {
        listarAlumnos(txtBuscar.value);
    }
}

async function obtenerMaterias(buscar = '') {
    const respuesta = await fetch(`/api/Materias/buscar?buscar=${encodeURIComponent(buscar)}`),
        materias = await respuesta.json();
    return materias;
}

function refrescarBusquedaMaterias() {
    const txtBuscar = document.getElementById('txtBuscarMateria');
    if (txtBuscar && typeof listarMaterias === 'function') {
        listarMaterias(txtBuscar.value);
    }
}

function escaparHtml(texto) {
    return String(texto ?? '').replace(/[&<>"']/g, c => ({
        '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;'
    }[c]));
}
