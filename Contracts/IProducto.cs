using ControlInventario.Models;

namespace ControlInventario.Contracts
{
	public interface IProducto
	{
		public Task<ResponseModel> AltaProducto(ProductoModel producto);
		public Task<List<ProductoModel>> ConsultaProductos();
		public Task<ProductoModel> ConsultaProducto(int id_producto);
		public Task<ResponseModel> EditarProducto(ProductoModel producto);
		public Task<ResponseModel> AlterarProducto(ProductoModel producto);

    }
}
