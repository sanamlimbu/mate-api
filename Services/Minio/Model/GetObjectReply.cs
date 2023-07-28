using Minio.DataModel;

namespace OzMateApi.Services.Minio.Model
{
	public class GetObjectReply
	{
        public required ObjectStat ObjectStat { get; set; }
        public required byte[] Data { get; set; }
    }
}

