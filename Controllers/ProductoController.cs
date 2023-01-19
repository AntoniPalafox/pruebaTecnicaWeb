using ControlInventario.Contracts;
using ControlInventario.Models;
using ControlInventario.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ControlInventario.Controllers
{
	public class ProductoController : Controller
	{
		private readonly IProducto _producto;
		private readonly IProductoTipo _productoTipo;
		private readonly IProductoPresentacion _productoPresentacion;
        private readonly IMovimiento _movimiento;

		public ProductoController(IProducto producto, IProductoTipo productoTipo, IProductoPresentacion productoPresentacion, IMovimiento movimiento)
        {
            _producto = producto;
            _productoTipo = productoTipo;
            _productoPresentacion = productoPresentacion;
            _movimiento = movimiento;
        }

        public async Task<ActionResult> Index()
		{
			ProductoViewModel productoViewModel = new();
            try 
			{
                productoViewModel.ProductoModels = await _producto.ConsultaProductos();
            }
            catch (Exception ex)
			{
                productoViewModel.ErrorViewModel.Error = ex.Message;
				Console.WriteLine(ex.Message);
			}

			return View(productoViewModel);
		}

        public async Task<ActionResult> FormProducto( [FromQuery] int Id_Producto = 0)
        {
            ProductoModel producto = new();

            if(Id_Producto != 0)
            {
                producto = await _producto.ConsultaProducto(Id_Producto);
            }
            SelectList selectListTipos = new( await _productoTipo.ConsultaProductosTipos(), "Id_Producto_Tipo", "Producto_Tipo");
            SelectList selectListPresentaciones = new( await _productoPresentacion.ConsultaProductosPresentacion(), "Id_Producto_Presentacion", "Producto_Presentacion");
            
            ViewData["listTipos"] = selectListTipos;
            ViewData["listPresentaciones"] = selectListPresentaciones;
            return View(producto);
        }

        public ActionResult FormTipo()
        {
            TipoModel tipo = new();
            return View(tipo);
        }

        public ActionResult FormPresentacion()
        {
            PresentacionModel presentacion = new();
            return View(presentacion);
        }

        [HttpPost]
		public async Task<ActionResult> AltaProducto([Bind] ProductoModel request)
		{
            ProductoViewModel productoViewModel = new();
            ResponseModel response = new();
			try
			{
				response = await _producto.AltaProducto(request);
			}
			catch (Exception ex)
			{
                productoViewModel.ErrorViewModel.Error = ex.Message;
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine(response.Mensaje);
            Console.WriteLine(response.Exito);

            if (response.Exito != 0 && response.Mensaje == null)
                return RedirectToAction("Index", new {notificacion = 1});
            else
                return RedirectToAction("FormProducto", new {mensaje = response.Mensaje});
		}

        [HttpPost]
        public async Task<ActionResult> AltaProductoTipo([Bind] TipoModel request)
        {
            ProductoViewModel productoViewModel = new();
            ResponseModel response = new();
            try
            {
                response = await _productoTipo.AltaProductoTipo(request);
            }
            catch (Exception ex)
            {
                productoViewModel.ErrorViewModel.Error = ex.Message;
                Console.WriteLine(ex.Message);
            }

            if (response.Exito != 0 && response.Mensaje == null)
                return RedirectToAction("Catalogos", new { notificacion = 1 });
            else
                return View();
        }

        [HttpPost]
        public async Task<ActionResult> AltaProductoPresentacion([Bind] PresentacionModel request)
        {
            ProductoViewModel productoViewModel = new();
            ResponseModel response = new();
            try
            {
                response = await _productoPresentacion.AltaProductoPresentacion(request);
            }
            catch (Exception ex)
            {
                productoViewModel.ErrorViewModel.Error = ex.Message;
                Console.WriteLine(ex.Message);
            }

            if (response.Exito != 0 && response.Mensaje == null)
                return RedirectToAction("Catalogos", new { notificacion = 1 });
            else
                return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditarProducto([Bind] ProductoModel request)
        {
            ProductoViewModel productoViewModel = new();
            ResponseModel response = new();
            try
            {
                response = await _producto.EditarProducto(request);
            }
            catch (Exception ex)
            {
                productoViewModel.ErrorViewModel.Error = ex.Message;
                Console.WriteLine(ex.Message);
            }

            if (response.Exito != 0 && response.Mensaje == null)
                return RedirectToAction("Index", new { notificacion = 1 });
            else
                return RedirectToAction("FormProducto", new { Id_Producto = request.Id_Producto, mensaje = response.Mensaje });
        }

        [HttpGet]
        public async Task<ActionResult> VerMovimientos(int id_producto)
        {
            ProductoMovimientosViewModel vm = new()
            {
                MovimientoModels = await _movimiento.ConsultaMovimientos(id_producto),
                Producto = await _producto.ConsultaProducto(id_producto)
            };

            return PartialView("VerMovimientos", vm);
        }

        [HttpGet]
		public async Task<JsonResult> ConsultaProductosTipos()
		{
			return new JsonResult( await _productoTipo.ConsultaProductosTipos());
		}

		[HttpGet]
		public async Task<JsonResult> ConsultaProductosPresentacion()
		{
			return new JsonResult( await _productoPresentacion.ConsultaProductosPresentacion());
		}

		[HttpGet]
		public async Task<JsonResult> ConsultaProducto([Bind] int id_producto )
		{
			return new JsonResult(await _producto.ConsultaProducto(id_producto));
		}

        [HttpGet]
        public async Task<JsonResult> ConsultaMovimientos(int id_producto)
        {
            return new JsonResult(await _movimiento.ConsultaMovimientos(id_producto));
        }

        [HttpPost]
        public async Task<ActionResult> AlterarProducto([Bind] ProductoModel request)
        {
            ProductoViewModel productoViewModel = new();
            ResponseModel response = new();
            try
            {
                response = await _producto.AlterarProducto(request);
            }
            catch (Exception ex)
            {
                productoViewModel.ErrorViewModel.Error = ex.Message;
                Console.WriteLine(ex.Message);
            }


            if (response.Exito != 0 && response.Mensaje == null)
                return RedirectToAction("Index", new { notificacion = 1 });
            else
                return RedirectToAction("Index", new { mensaje = "Ingresa una cantidad valida" });
        }

        [HttpGet]
        public ActionResult ConfirmacionModal(int id_producto, string opcion)
        {
            ProductoModel producto = new();
            producto.Id_Producto = id_producto;
            producto.Opcion = opcion;

            return PartialView("ConfirmacionModal", producto);
        }

        public async Task<ActionResult> Catalogos()
        {
            ProductoViewModel productoViewModel = new();
            try
            {
                productoViewModel.TipoModels = await _productoTipo.ConsultaProductosTipos();
                productoViewModel.PresentacionModels = await _productoPresentacion.ConsultaProductosPresentacion();
            }
            catch (Exception ex)
            {
                productoViewModel.ErrorViewModel.Error = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return View(productoViewModel);
        }


    }
}
