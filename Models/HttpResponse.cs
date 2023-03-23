﻿using System;
namespace OzMateApi.Models
{
	public class HTTPResponse
	{
		public string Code { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;
		public object? ResponseDate { get; set; }
	}

	public enum ResponseType
	{
		Success,
		NotFound,
		Failure,
	}
}

