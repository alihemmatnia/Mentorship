using Mentorship.FileManager.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.FileManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly S3Client _s3Client;

        public FileController(S3Client s3Client)
        {
            _s3Client = s3Client;
        }

        [HttpGet]
        [Route("{path}")]
        public async Task<ActionResult> DownloadFile(string path, [FromQuery] bool inline = true)
        {
            if (string.IsNullOrWhiteSpace(path))
                return BadRequest("there is no file selected");

            var file = await _s3Client.DownloadFile(Configs.Configs.S3_FILEBUCKET, path);
            if (file.Item1 == null)
            {
                return NotFound();
            }
            Response.Headers.Append("Content-Disposition", new System.Net.Mime.ContentDisposition
            {
                FileName = path,
                Inline = inline
            }.ToString());

            return File(file.Item1, file.Item2, enableRangeProcessing: true);
        }

        [HttpPost]
        [Route("upload")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(79914561)]
        public async Task<ActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null)
                return BadRequest("there is no file in the request");

            try
            {
                var fileInfo = new FileInfo(file.Name);
                var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                await _s3Client.UploadFile(Configs.Configs.S3_FILEBUCKET, file.OpenReadStream(), fileName, MimeType.GetContentTypeFromExtension(fileInfo.Extension));

                return Ok(new
                {
                    Success = true,
                    Message = fileName
                });
            }
            catch 
            {
                return Ok(new
                {
                    Success = false,
                    Message = "fail to upload file",
                });
            }
        }

    }
}
