using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ZCheckIn.Backend.DTOs;

namespace ZCheckIn_Backend
{
    public static class ProcessUploadedUser
    {
        [FunctionName("ProcessUploadedUser")]
        public static void Run([BlobTrigger("users/user/{uuid}", Connection = "Azurite_Storage")] Stream myBlob, string uuid, ILogger log)
        {
            log.LogInformation(string.Format("User {0} added to storage. Processing...", uuid));

            string jsonContent = new StreamReader(myBlob).ReadToEnd();
            UserDTO user = JsonConvert.DeserializeObject<UserDTO>(jsonContent);

            string content = "";

            log.LogInformation("Sending email notification.");

            //SendEmail(log, content);

            log.LogInformation("Email sent.");

        }

        private static void SendEmail(ILogger log, string emailContent)
        {
            try
            {

                MailMessage mailMsg = new MailMessage();
                mailMsg.To.Add(new MailAddress("", "Admin@ZCheckIn"));
                mailMsg.From = new MailAddress("", "ZCheckIn App");

                mailMsg.Subject = "New User/Employee";
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(emailContent, null, MediaTypeNames.Text.Html));


                SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));

                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("<SendGrid-username>", "<SendGrid-password>");
                smtpClient.Credentials = credentials;
                smtpClient.Send(mailMsg);
            }
            catch (Exception ex)
            {
                log.LogError(string.Format("Error sending email. Error: {0}", ex.Message));
            }


        }
    }


}
