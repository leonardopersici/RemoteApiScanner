// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Funzione per centrare il contenuto di un div tra header e footer
function setCenterDivHeight() {
    const centerDiv = document.querySelector('.container.d-flex');
    const headerHeight = document.querySelector('header').offsetHeight;
    const footerHeight = document.querySelector('footer').offsetHeight;
    const windowHeight = window.innerHeight - 32; // In toeria il -32 non dovrebbe servire ma è necessario altrimenti non funziona correttamente la centratura.
    const visibleHeight = windowHeight - headerHeight - footerHeight;
    centerDiv.style.height = visibleHeight + 'px';
}

window.addEventListener('DOMContentLoaded', setCenterDivHeight);
window.addEventListener('resize', setCenterDivHeight);