using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
// using System.Net.Mail;
using System.Net;
using Microsoft.VisualBasic;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace FreeMoney.Functions
{
    public static class RegisterUserRecord
    {
        [FunctionName("RegisterUserRecord")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            string email = req.Query["email"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    var messageBody = $"<h1>{name} just registered</h1><br /><p>Email is: {email}</p>";
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Jonathan Gzz", "jonathankyurem@hotmail.com"));
                    message.To.Add(new MailboxAddress("Jonathan Gzz", "jonathankyurem@hotmail.com"));
                    message.Subject = "New registration";

                    message.Body = new TextPart("html")
                    {
                        Text = messageBody
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.ethereal.email", 587, false);

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate("arthur72@ethereal.email", "9g5UDmme6WMYHCGKx1");

                        client.Send(message);
                        client.Disconnect(true);
                    }
                }
                catch (Exception e)
                {
                    responseMessage = e.Message;
                }
            }

            return new OkObjectResult(responseMessage);
        }
    }
}
