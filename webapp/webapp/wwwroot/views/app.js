document.addEventListener("DOMContentLoaded", () => {
    // Clic en una opción del menú → abre la ventana indicada en su data-form
    mnxAcademica.addEventListener("click", event => {
        event.preventDefault();
        const form = event.target.dataset.form;   // "alumno", "materia", "docente"...
        if (form) {                               // "Principal" no tiene data-form
            abrirVentana(form);
        }
    });

    // Clic en la X de cualquier ventana → la oculta.
    // Se registra UNA sola vez y sirve para todas las ventanas, incluso las que se carguen después.
    $(document).on("click", ".btn-close[data-form]", e => {
        $(`#${e.currentTarget.dataset.form}`).hide();
    });
});

function abrirVentana(form) {
    const $ventana = $(`#${form}`);               // #alumno, #materia, #docente, etc.

    if ($ventana.find("form").length > 0) {       // ya se había cargado antes → solo mostrarla
        $ventana.show();
        return;
    }

    fetch(`/views/${form}.html`)                  // descarga el html de la ventana
        .then(resp => {
            if (!resp.ok) throw new Error(`No existe la ventana "${form}" todavía`);
            return resp.text();
        })
        .then(vista => {
            $ventana.html(vista).show().draggable({ handle: ".card-header" });
        })
        .catch(error => alertify.error(error.message));
}
