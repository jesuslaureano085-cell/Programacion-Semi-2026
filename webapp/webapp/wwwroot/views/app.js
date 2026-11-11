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
        return;
    }

    fetch(`/views/${form}.html`)
        .then(resp => {
            if (!resp.ok) throw new Error(`No existe la ventana "${form}" todavía`);
            return resp.text();
        })
        .then(vista => {
            $ventana.html(vista).show().draggable({ handle: ".card-header" });
        })
        .catch(error => alertify.error(error.message));
}
