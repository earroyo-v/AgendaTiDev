// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//btn-danger
window.addEventListener('DOMContentLoaded', function () {

    const deleteButton = document.querySelector('.btnDeleteUser');
    const modalElement = document.getElementById('errorModal');

    if (!deleteButton || !modalElement) {
        return;
    }

    const errorModal = new bootstrap.Modal(modalElement);
    const footer = modalElement.querySelector('.modal-footer');

    // Crear botón Confirmar solamente una vez
    let confirmButton = footer.querySelector('.confDeleteUser');

    if (!confirmButton) {
        confirmButton = document.createElement('button');

        confirmButton.type = 'button';
        confirmButton.className = 'btn btn-primary confDeleteUser';
        confirmButton.textContent = 'Confirmar';

        footer.insertBefore(confirmButton, footer.lastElementChild);
    }

    deleteButton.addEventListener('click', function (event) {
        event.preventDefault();

        // Cambiar título
        modalElement.querySelector('.modal-title').textContent =
            'Eliminar usuario';

        // Cambiar mensaje
        document.getElementById('modalMessageE').textContent =
            '¿Seguro que quieres eliminar tu cuenta?';

        // Cambiar icono
        modalElement.querySelector('.modal-body i').className =
            'bi bi-exclamation-triangle-fill text-warning';

        // Mostrar modal
        errorModal.show();
    });

    // Confirmar eliminación
    confirmButton.addEventListener('click', function () {
        window.location.href = deleteButton.href;
    });
});