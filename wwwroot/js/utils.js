//Borrar un elemento insertado con scripting JS
export function limpiar(elemento) {
    while (elemento.firstChild) {
        elemento.removeChild(elemento.firstChild);
    }
}

//Validar que un objeto este lleno
export function validar(objeto) {
    return !Object.values(objeto).every(input => input !== '')
}

//Mostrar una alerta y eliminarla después de 3 segundos
export function mostrarAlerta(mensaje, campo) {
    console.log(mensaje);

    campo.innerHTML = `<p class="text-center text-uppercase bg-danger text-white py-3">${mensaje}</p>`;
    campo.classList.add('d-fixed', 'px-auto', 'my-3');

    setTimeout(() => {
        limpiar(campo)
    }, 3000);
}
