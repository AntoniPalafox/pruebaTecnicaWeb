using Microsoft.Data.SqlClient;
using System.Data;

namespace ControlInventario.Context
{
	public class DapperContext
	{
		private readonly IConfiguration _configuration;
		private readonly string _localConnection;

		public DapperContext(IConfiguration configuration)
		{
			_configuration = configuration;
			_localConnection = _configuration.GetConnectionString("LocalConnection");
		}

		public IDbConnection CreateConnection() => new SqlConnection(_localConnection);
	}
}
