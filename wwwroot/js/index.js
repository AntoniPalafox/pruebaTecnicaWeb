import { limpiar, mostrarAlerta } from "./utils.js"

(function () {
	console.log('conectado al index');

	var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
	var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
		return new bootstrap.Tooltip(tooltipTriggerEl)
	})

	escucharModal();

	function escucharModal() {
		let iconosModal = document.querySelectorAll('.iconos-modal');

		iconosModal.forEach(icono => {
			icono.addEventListener("click", e => {
				e.preventDefault();

				console.log(e.target.id);
				let id = e.target.id;

				if (id != "") {
					let particionado = id.split('-');
					let accion = particionado[0];
					let identificador = particionado[1];

                    mostrarModal(identificador, accion);
				}
			})
		})
	}

	async function mostrarModal(identificador, accion) {

		let endpoint = 'ConfirmacionModal';
		if (accion == 'movimientos') {
			endpoint = 'VerMovimientos';
		}

        try {

            document.getElementById("modal-contenido").innerHTML = '';

            await fetch(`/Producto/${endpoint}/?id_producto=${parseInt(identificador)}&opcion=${accion}`)
                .then(async function (response) {
                    return await response.text();
                })
                .then(function (html) {
                    //genero un documento dom
                    var parser = new DOMParser();
                    var doc = parser.parseFromString(html, 'text/html');
                    document.getElementById("modal-contenido").innerHTML = doc.body.innerHTML;
                })
                .catch(function (err) {
                    console.warn('Error:', err);
                });

            await $('#confirmacionModal').modal('toggle');
        } catch (error) {
            console.log(error);
        }
	}
})();
