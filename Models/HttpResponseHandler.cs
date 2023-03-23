using System;

namespace OzMateApi.Models
{
	public class HttpResponseHandler
	{
		public static HttpResponse GetExceptionResponse(Exception ex)
		{
			HttpResponse resp = new HttpResponse();
			resp.Code = "500";
			resp.Message = ex.Message;
			return resp;
		}

		public static HttpResponse GetHttpResponse(string code, string message, object? contract)
		{
            HttpResponse resp = new HttpResponse();
            resp.Code = code;
            resp.Message = message;
			resp.ResponseData = contract;
            return resp;
        }
	}
}

