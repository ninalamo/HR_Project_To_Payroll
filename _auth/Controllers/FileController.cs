using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using auth.api.Models.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace auth.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        static CloudBlobClient _blobClient;
        const string _blobContainerName = "erp";
        static CloudBlobContainer _blobContainer;
        private readonly IConfiguration _configuration;

        public FileController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile()
        {
            var storageConnectionString = _configuration.GetValue<string>("StorageConnectionString");

            var blobUrl = _configuration.GetValue<string>("BlobUrl");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            _blobClient = storageAccount.CreateCloudBlobClient();
            _blobContainer = _blobClient.GetContainerReference(_blobContainerName);
            await _blobContainer.CreateIfNotExistsAsync();
            await _blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            try
             {
                var file = Request.Form.Files[0];

                if(file == null)
                {
                    return BadRequest("Could not upload files.");
                }
                else
                {
                    var fileName = GetRandomBlobName(file.FileName);

                    CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(fileName);

                    var stream = file.OpenReadStream();

                   await blob.UploadFromStreamAsync(stream);

                    return Ok(new
                    {
                        success = true,
                        message = "File uploaded successfully.",
                        filePath = $"{blobUrl}/{_blobContainerName}/{fileName}"
                    });
                }
                
            }
            catch(Exception e)
            {
                return BadRequest("Error encountered.");
            }
        }

        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }

    }
}