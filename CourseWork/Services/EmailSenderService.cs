using CourseWork.Database;
using CourseWork.Models;
using CourseWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            try
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
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static async Task SendCodeRefactor(string UserMail, int code, string subject, string body)
        {
            try
            {
                MailAddress from = new MailAddress("cwpartshopmail@gmail.com", "PartShop");
                MailAddress to = new MailAddress(UserMail);
                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body + code.ToString();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("cwpartshopmail@gmail.com", "93700739YoG");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static async Task SendTicket(string UserMail, string subject, string body)
        {
            try
            {
                MailAddress from = new MailAddress("cwpartshopmail@gmail.com", "PartShop");
                MailAddress to = new MailAddress(UserMail);
                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("cwpartshopmail@gmail.com", "93700739YoG");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static string GenerateTicket(Order order)
        {
            using (PartShopDbContext db = new PartShopDbContext()) 
            {
                double sum = 0;
                var orderParts = from o in db.Orders
                                 join op in db.OrderedParts
                                 on o.OrderId equals op.OrderId
                                 join p in db.Parts
                                 on op.PartId equals p.PartId
                                 where o.OrderId == order.OrderId
                                 select new
                                 {
                                     Name = p.Name,
                                     Amount = op.Amount,
                                     Price = p.Price,
                                     Mark = p.Mark.MarkName
                                 };
                StringBuilder body = new StringBuilder();
                body.Append($"Чек заказа {order.OrderId} из магазина автозапастей AutoLight");
                body.Append("\n" + order.OrderDate);
                body.Append("\n-----------------------------------");
                foreach(var p in orderParts)
                {
                    body.Append($"\nЗапчасть: {p.Name}, Количество: {p.Amount}, Цена: {p.Price}, Для авто: {p.Mark}");
                    sum += p.Price * p.Amount;
                }
                body.Append("\n-----------------------------------");
                body.Append($"\nДоставка: {order.Delivery.Name}");
                body.Append($"\nИтоговая сумма: {sum + order.Delivery.Price}");
                body.Append("\n-----------------------------------");
                body.Append("\nСпасибо за заказ в нашем магазине!");
                return body.ToString();
            }
        }
    }
}
