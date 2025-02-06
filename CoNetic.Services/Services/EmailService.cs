using CoNetic.Core.ServicesInterfaces;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CoNetic.Core.Models;
using CoNetic.Services.Helper;
using CoNetic.Services.Build;
using Microsoft.AspNetCore.Hosting;

namespace CoNetic.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly SMTP smtp;
        public EmailService(UserManager<User> _userManager, IOptions<SMTP> smtp, IWebHostEnvironment webHostEnvironment)
        {
            this._userManager = _userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.smtp = smtp.Value;

        }
        public async Task SendEmailAsync(string email, string subject, string verificationCode)
        {
      

            try
            {

                string imageUrl = $"https://cdn.dribbble.com/users/3366206/screenshots/10583824/media/a242c31359ec9f92fc0c9a344a4b355e.jpg";
                var user = await _userManager.FindByEmailAsync(email);
                string emailBody = BuildBody.GenerateEmailBody("EmailTemplate",
                   new Dictionary<string, string>{ { "{{UserName}}", user.FirstName },
                       { "{{VerificationCode}}", verificationCode} ,
                       {"{{ImageUrl}}", imageUrl },
                       {"{{Year}}", DateTime.Now.Year.ToString() }
                   }, webHostEnvironment);




             



                using (var smtpClient = new SmtpClient(smtp.SmtpServer, smtp.SmtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(smtp.SenderEmail, smtp.SenderPassword);
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtp.SenderEmail),
                        Subject = subject,
                        Body = emailBody,
                        IsBodyHtml = true // Set to true if sending HTML email
                    };

                    mailMessage.To.Add(email);

                    await smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine("Email sent successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
