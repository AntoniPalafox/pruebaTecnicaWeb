using ControlInventario.Models;

namespace ControlInventario.ViewModels
{
    public class ProductoViewModel
    {
        public ErrorViewModel ErrorViewModel { get; set; } = new ErrorViewModel();
        public List<ProductoModel> ProductoModels { get; set; } = new List<ProductoModel>();
        public List<MovimientoModel> MovimientoModels { get; set; } = new List<MovimientoModel>();
        public List<TipoModel> TipoModels { get; set; } = new List<TipoModel>();
        public List<PresentacionModel> PresentacionModels { get; set; } = new List<PresentacionModel>();
        public ProductoModel Producto { get; set; } = new ProductoModel();
        public MovimientoModel Movimiento { get; set; } = new MovimientoModel();
    }
}
