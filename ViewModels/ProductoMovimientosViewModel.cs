using ControlInventario.Models;

namespace ControlInventario.ViewModels
{
    public class ProductoMovimientosViewModel
    {
        public ProductoModel Producto { get; set; } = new ProductoModel();
        public List<MovimientoModel> MovimientoModels { get; set; } = new List<MovimientoModel>();
    }
}
