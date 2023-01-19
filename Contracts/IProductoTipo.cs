using ControlInventario.Models;

namespace ControlInventario.Contracts
{
    public interface IProductoTipo
    {
        public Task<ResponseModel> AltaProductoTipo(TipoModel tipo);
        public Task<List<TipoModel>> ConsultaProductosTipos();
    }
}
