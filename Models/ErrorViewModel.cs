namespace ControlInventario.Models
{
	public class ErrorViewModel
	{
		public string? RequestId { get; set; }
		public string Error { get; set; } = string.Empty;

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}