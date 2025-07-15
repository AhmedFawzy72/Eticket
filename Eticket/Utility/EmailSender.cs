using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Eticket.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("mahmouedahmoued236@gmail.com", "qxru fiqt mapa rxsz\r\n")
            };

            return client.SendMailAsync(
                new MailMessage(from: "mahmouedahmoued236@gmail.com",
                                to: email,
                                subject,
                                message


                                )
                {
                    IsBodyHtml = true


                });
            
        }
    }
}
