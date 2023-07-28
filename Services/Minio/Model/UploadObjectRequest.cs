using System;
namespace OzMateApi.Services.Minio.Model
{
	public class UploadObjectRequest
	{
        public required string Bucket { get; set; }
		public required byte[] Data { get; set; }
		public required string ObjectName { get; set; }
	}
}

