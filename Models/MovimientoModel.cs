namespace ControlInventario.Models
{
	public class MovimientoModel
	{
		public int Id_Movimiento { get; set; }
		public char Tipo_Movimiento { get; set; }
		public int Cantidad_Inicial { get; set; }
		public int Cantidad_Final { get; set; }
		public DateTime Fecha_Movimiento { get; set; }
		public int Id_Producto { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}
