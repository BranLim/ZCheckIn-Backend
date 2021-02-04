using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace check_in_user_registration
{
    public static class RegisterUser
    {
        [FunctionName("RegisterUser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Registering User.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrEmpty(requestBody))
            {
                return new BadRequestObjectResult("Invalid request body");
            }

            RegisterUserRequest data = JsonConvert.DeserializeObject<RegisterUserRequest>(requestBody);

            string uuid = data?.UUID;

            if (string.IsNullOrEmpty(uuid))
            {
                return new BadRequestObjectResult("missing UUID");
            }

            string faceImage = data?.FaceImageAsBase64;




            return new OkObjectResult(string.Format("Your ID is {0}", uuid));
        }
    }
}
