using Amazon.S3;
using Amazon.S3.Model;

namespace Mentorship.FileManager.Utils
{
    public class S3Client
    {
        private static IAmazonS3 _s3Client;

        public S3Client()
        {
            _s3Client = new AmazonS3Client(
               Configs.Configs.S3_ACCESSKEY,
                Configs.Configs.S3_SECRETKEY,
                new AmazonS3Config
                {
                    ServiceURL = Configs.Configs.S3_ENDPOINT, 
                    ForcePathStyle = true, 
                });
        }

        public async Task<(Stream, string)> DownloadFile(
            string bucketName,
             string fileName)
        {
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = fileName,
            };
            using GetObjectResponse response = await _s3Client.GetObjectAsync(request);
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                return (null, null);

            string contentType = response.Headers["Content-Type"];
            using Stream responseStream = response.ResponseStream;

            MemoryStream memoryStream = new MemoryStream();
            await response.ResponseStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return (memoryStream, contentType);
        }


        public async Task Delete(string bucketName, string fileName)
        {
            DeleteObjectRequest request = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = fileName,
            };

            await _s3Client.DeleteObjectAsync(request);
        }

        public async Task<bool> UploadFile(
            string bucketName,
             Stream file,
             string fileName, string contentType)
        {

            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                InputStream = file,
                Key = fileName,
                ContentType = contentType
            };

            putRequest.Metadata.Add("x-amz-meta-title", fileName);

            PutObjectResponse response = await _s3Client.PutObjectAsync(putRequest);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
