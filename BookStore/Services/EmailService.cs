using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading.Tasks;
using BookStore.Services.Interfaces;

namespace BookStore.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService(){
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("tuongankk2003@gmail.com", ""),
                EnableSsl = true,
            };
        }
        public Task SendEmailAsync(string to, string subject, string body)
        {
            var mailService = new MailMessage{
                From = new MailAddress("tuongankk2003@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };
            mailService.To.Add(to);
            return _smtpClient.SendMailAsync(mailService);
        }
    }
}