using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Blob;
using ZCheckIn.Backend.DTOs;

namespace ZCheckIn.Backend
{
    public static class UploadUser
    {
        [FunctionName("UploadUser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
             [Blob("users", FileAccess.Write, Connection = "Azurite_Storage")] CloudBlobContainer outputContainer,
            ILogger log)
        {
            log.LogInformation("Uploading user profile to Blob Storage");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            UserDTO data = JsonConvert.DeserializeObject<UserDTO>(requestBody);

            string uuid = data?.UUID;

            if (string.IsNullOrEmpty(uuid))
            {
                return new BadRequestObjectResult("missing UUID");
            }
            if (string.IsNullOrEmpty(data.FaceImageAsBase64))
            {
                return new BadRequestObjectResult("missing user image");
            }

            try
            {
                log.LogInformation("Creating storage container if not exists.");
                await outputContainer.CreateIfNotExistsAsync();
            }
            catch (Exception e)
            {
                log.LogError(string.Format("Fail to create container. Error: {0}", e.Message));
                return new BadRequestObjectResult("Failed to create storage container.");
            }


            CloudBlockBlob blob = outputContainer.GetBlockBlobReference("user/" + uuid);

            await blob.UploadTextAsync(JsonConvert.SerializeObject(data));
            log.LogInformation("User registered");

            return new OkObjectResult("User uploaded");
        }
    }
}
