using ControlInventario.Models;

namespace ControlInventario.Contracts
{
    public interface IMovimiento
    {
        public Task<List<MovimientoModel>> ConsultaMovimientos(int id_producto);
    }
}
