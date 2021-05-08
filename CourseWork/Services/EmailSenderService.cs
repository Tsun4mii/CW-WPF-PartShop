using CourseWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Services
{
    public static class EmailSenderService
    {
        public static async Task SendEmail(string UserMail)
        {
            MailAddress from = new MailAddress("cwpartshopmail@gmail.com", "PartShop");
            MailAddress to = new MailAddress(UserMail);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Test";
            message.Body = "Письмо-тест 2 работы smtp-клиента";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("cwpartshopmail@gmail.com", "93700739YoG");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(message);
        }

        public static async Task SendCode(string UserMail, int code)
        {
            MailAddress from = new MailAddress("cwpartshopmail@gmail.com", "PartShop");
            MailAddress to = new MailAddress(UserMail);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Test";
            message.Body = code.ToString();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("cwpartshopmail@gmail.com", "93700739YoG");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(message);
        }
    }
}
