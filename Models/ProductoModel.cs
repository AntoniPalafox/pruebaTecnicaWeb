namespace ControlInventario.Models
{
	public class ProductoModel
	{
		public int Id_Producto { get; set; }
		public string Producto { get; set; } = string.Empty;
		public string Descripcion { get; set; } = string.Empty;
		public int Cantidad { get; set; }
		public bool Activo { get; set; }
		public bool Borrado { get; set; }
		public int Id_Producto_Tipo { get; set; }
		public int Id_Producto_Presentacion { get; set; }
		public string Mensaje { get; set; } = string.Empty;
		public string Opcion { get; set; } = string.Empty;
	}
}
