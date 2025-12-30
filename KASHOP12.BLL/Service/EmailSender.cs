using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace KASHOP12.BLL.Service
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("sosoodeh07@gmail.com", "oyyl wsfl rwxk xpwo")
            };

            return client.SendMailAsync(
                new MailMessage(from: "sosoodeh07@gmail.com",
                                to: email,
                                subject,
                                htmlMessage)

                { IsBodyHtml = true });
                              
        }
    }
}
