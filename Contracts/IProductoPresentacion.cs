using ControlInventario.Models;

namespace ControlInventario.Contracts
{
    public interface IProductoPresentacion
    {
        public Task<ResponseModel> AltaProductoPresentacion(PresentacionModel presentacion);
        public Task<List<PresentacionModel>> ConsultaProductosPresentacion();
    }
}
