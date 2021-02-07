using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ZCheckIn_Backend
{
    public static class ProcessUploadedUser
    {
        [FunctionName("ProcessUploadedUser")]
        public static void Run([BlobTrigger("user/{uuid}", Connection = "Azurite_Storage")]Stream myBlob, string uuid, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{uuid} \n Size: {myBlob.Length} Bytes");
        }
    }
}
