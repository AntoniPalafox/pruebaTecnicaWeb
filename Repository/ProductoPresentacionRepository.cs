using ControlInventario.Context;
using ControlInventario.Contracts;
using ControlInventario.Models;
using Dapper;
using System.Data;

namespace ControlInventario.Repository
{
    public class ProductoPresentacionRepository : IProductoPresentacion
    {
        private readonly DapperContext _context;
        public ProductoPresentacionRepository(DapperContext context) { _context = context; }

        public async Task<ResponseModel> AltaProductoPresentacion(PresentacionModel presentacion)
        {
            ResponseModel response = new();

            using (IDbConnection connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@producto_presentacion", presentacion.Producto_Presentacion, DbType.String);
                dynamicParameters.Add("@mensaje", presentacion.Mensaje, DbType.String, ParameterDirection.Output);

                response.Exito = await connection.ExecuteAsync("SP_C_PRODUCTOS_PRESENTACION", dynamicParameters, commandType: CommandType.StoredProcedure);
                response.Mensaje = dynamicParameters.Get<string>("@mensaje");

                connection.Close();
            }
            return response;
        }

        public async Task<List<PresentacionModel>> ConsultaProductosPresentacion()
        {
            List<PresentacionModel> presentaciones = new List<PresentacionModel>();

            using (IDbConnection connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                presentaciones.AddRange(await connection.QueryAsync<PresentacionModel>("SP_R_PRODUCTOS_PRESENTACION", null, commandType: CommandType.StoredProcedure));

            }
            return presentaciones;
        }
    }
}
