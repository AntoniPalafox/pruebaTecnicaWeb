using ControlInventario.Context;
using ControlInventario.Contracts;
using ControlInventario.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ControlInventario.Repository
{
	public class ProductoRepository : IProducto
	{
		private readonly DapperContext _context;
		public ProductoRepository(DapperContext context) { _context = context; }

		public async Task<ResponseModel> AltaProducto(ProductoModel producto)
		{
			ResponseModel response = new();

			using (IDbConnection connection = _context.CreateConnection())
			{
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}

				DynamicParameters dynamicParameters = new();
				dynamicParameters.Add("@producto", producto.Producto, DbType.String);
				dynamicParameters.Add("@descripcion", producto.Descripcion, DbType.String);
				dynamicParameters.Add("@cantidad", producto.Cantidad, DbType.Int32);
				dynamicParameters.Add("@id_producto_tipo", producto.Id_Producto_Tipo, DbType.Int32);
				dynamicParameters.Add("@id_producto_presentacion", producto.Id_Producto_Presentacion, DbType.Int32);
                dynamicParameters.Add("@mensaje", producto.Mensaje, DbType.String, ParameterDirection.Output);

                response.Exito = await connection.ExecuteAsync("SP_C_PRODUCTOS", dynamicParameters, commandType: CommandType.StoredProcedure);
				response.Mensaje = dynamicParameters.Get<string>("@mensaje");

                connection.Close();
            }
			return response;
		}

		public async Task<List<ProductoModel>> ConsultaProductos()
		{
			List<ProductoModel> productos= new();

			using (IDbConnection connection = _context.CreateConnection())
			{
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}

                productos.AddRange(await connection.QueryAsync<ProductoModel>("SP_R_PRODUCTOS", null, commandType: CommandType.StoredProcedure));

                connection.Close();
            }
			return productos;
		}

        public async Task<ProductoModel> ConsultaProducto( int id_producto)
        {
			ProductoModel producto = new();

            using (IDbConnection connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
				DynamicParameters dynamicParameters = new();
				dynamicParameters.Add("@id_producto", id_producto, DbType.Int32);

                producto = await connection.QueryFirstAsync<ProductoModel>("SP_R_PRODUCTOS", dynamicParameters, commandType: CommandType.StoredProcedure);

				connection.Close();
            }
            return producto;
        }

        public async Task<ResponseModel> EditarProducto(ProductoModel producto)
        {
			ResponseModel response = new();

            using (IDbConnection connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@opcion", "modificar", DbType.String);
                dynamicParameters.Add("@id_producto", producto.Id_Producto, DbType.Int32);
                dynamicParameters.Add("@producto", producto.Producto, DbType.String);
                dynamicParameters.Add("@descripcion", producto.Descripcion, DbType.String);
                dynamicParameters.Add("@id_producto_tipo", producto.Id_Producto_Tipo, DbType.Int32);
                dynamicParameters.Add("@id_producto_presentacion", producto.Id_Producto_Presentacion, DbType.Int32);
                dynamicParameters.Add("@mensaje", producto.Mensaje, DbType.String, ParameterDirection.Output);

                response.Exito = await connection.ExecuteAsync("SP_U_PRODUCTOS", dynamicParameters, commandType: CommandType.StoredProcedure);
                response.Mensaje = dynamicParameters.Get<string>("@mensaje");

                connection.Close();
            }
            return response;
        }

        public async Task<ResponseModel> AlterarProducto(ProductoModel producto)
        {
            ResponseModel response = new();

            using (IDbConnection connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@opcion", producto.Opcion, DbType.String);
                dynamicParameters.Add("@id_producto", producto.Id_Producto, DbType.Int32);
                dynamicParameters.Add("@cantidad", producto.Cantidad, DbType.Int32);
                dynamicParameters.Add("@mensaje", producto.Mensaje, DbType.String, ParameterDirection.Output);

                response.Exito = await connection.ExecuteAsync("SP_U_PRODUCTOS", dynamicParameters, commandType: CommandType.StoredProcedure);
                response.Mensaje = dynamicParameters.Get<string>("@mensaje");

                connection.Close();
            }
            return response;
        }

       

    }
}
