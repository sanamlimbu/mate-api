using Minio;
using Minio.Exceptions;
using OzMateApi.Services.Minio.Model;

namespace OzMateApi.Services.Minio
{
	public class MinioObject
	{
        private readonly IConfiguration _configuration;
        private readonly MinioClient _minio;

        public MinioObject(IConfiguration configuration)
        {
            _configuration = configuration;

            string endpoint = _configuration["Minio:Endpoint"];
            string accessKey = _configuration["Minio:AccessKey"];
            string secretKey = _configuration["Minio:SecretKey"];

            _minio = new MinioClient()
                            .WithEndpoint(endpoint)
                            .WithCredentials("mma","")
                            .WithSSL(false)
                            .Build();
        }

        
        public async Task<string> UploadObject(UploadObjectRequest request)
        {
            var bucketName = request.Bucket;
            var objectName = request.ObjectName;
            try
            {
                // Check if bucket present
                var beArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);
                bool found = await _minio.BucketExistsAsync(beArgs).ConfigureAwait(false);
                if (!found)
                {
                    return await Task.FromResult<string>("");
                }

                MemoryStream filestream = new MemoryStream(request.Data);

                // Upload a file to bucket.
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithStreamData(filestream)
                    .WithObjectSize(filestream.Length);

                await _minio.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
                return await Task.FromResult<string>(objectName);
            }
            catch (MinioException e)
            {
                Console.WriteLine("MinIO exceptiom: {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                throw;
            }
        }

        public async Task<GetObjectReply> GetObject(string bucketName, string objectName)
        {
            try
            {
                var args = new GetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName);

                // Confirm object exists before attempting to get.
                StatObjectArgs statObjectArgs = new StatObjectArgs()
                    .WithBucket(bucketName)
                    .WithBucket(objectName);

                var objectStat = await _minio.StatObjectAsync(statObjectArgs);

                MemoryStream destination = new MemoryStream();

                // Get input stream.
                GetObjectArgs getObjectArgs = new GetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithCallbackStream((stream) =>
                    {
                        stream.CopyTo(destination);
                    });
                await _minio.GetObjectAsync(getObjectArgs);

                return await Task.FromResult<GetObjectReply>(new GetObjectReply()
                {
                    Data = destination.ToArray(),
                    ObjectStat = objectStat

                });
            }
            catch (MinioException e)
            {
                Console.WriteLine($"MinIO Exception: {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                throw;
            }
        }
    } 
}

