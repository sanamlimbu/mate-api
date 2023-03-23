using System;

namespace OzMateApi.Models
{
	public class HttpResponse
	{
		public string Code { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;
		public object? ResponseData { get; set; }
	}
}

