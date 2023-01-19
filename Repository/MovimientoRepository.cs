using ControlInventario.Context;
using ControlInventario.Contracts;
using ControlInventario.Models;
using Dapper;
using System.Data;

namespace ControlInventario.Repository
{
    public class MovimientoRepository: IMovimiento
    {
        private readonly DapperContext _context;
        public MovimientoRepository(DapperContext context) { _context = context; }

        public async Task<List<MovimientoModel>> ConsultaMovimientos(int id_producto)
        {
            List<MovimientoModel> movimientos = new();

            using (IDbConnection connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id_producto", id_producto, DbType.Int64);

                movimientos.AddRange(await connection.QueryAsync<MovimientoModel>("SP_R_MOVIMIENTOS", dynamicParameters, commandType: CommandType.StoredProcedure));

                connection.Close();
            }
            return movimientos;
        }
    }
}
