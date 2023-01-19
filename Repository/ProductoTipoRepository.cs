using ControlInventario.Context;
using ControlInventario.Contracts;
using ControlInventario.Models;
using Dapper;
using System.Data;

namespace ControlInventario.Repository
{
    public class ProductoTipoRepository : IProductoTipo
    {
        private readonly DapperContext _context;
        public ProductoTipoRepository(DapperContext context) { _context = context; }

        public async Task<ResponseModel> AltaProductoTipo(TipoModel tipo)
        {
            ResponseModel response = new();

            using (IDbConnection connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@producto_tipo", tipo.Producto_Tipo, DbType.String);
                dynamicParameters.Add("@mensaje", tipo.Mensaje, DbType.String, ParameterDirection.Output);

                response.Exito = await connection.ExecuteAsync("SP_C_PRODUCTOS_TIPOS", dynamicParameters, commandType: CommandType.StoredProcedure);
                response.Mensaje = dynamicParameters.Get<string>("@mensaje");

                connection.Close();
            }
            return response;
        }


        public async Task<List<TipoModel>> ConsultaProductosTipos()
        {
            List<TipoModel> tipos = new();

            using (IDbConnection connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                tipos.AddRange(await connection.QueryAsync<TipoModel>("SP_R_PRODUCTOS_TIPOS", null, commandType: CommandType.StoredProcedure));

            }
            return tipos;
        }
    }
}
