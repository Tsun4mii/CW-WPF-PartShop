using CourseWork.Commands;
using CourseWork.Models;
using CourseWork.Properties;
using CourseWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace CourseWork.ViewModels
{
    public class ConfirmOrderViewModel : ViewModelBase
    {
        public int code;
        public string codeFromBox { get; set; }
        public Command submitCode;
        public static int orderId;
        public Order order = App.db.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();
        public ConfirmOrderViewModel()
        {
            Random rand = new Random();
            code = rand.Next(99999);
            EmailSenderService.SendCodeRefactor(Settings.Default.UserMail, code, "Код подтверждения", "Никому не сообщайте данный код! \nКод подтверждения: ").GetAwaiter();
        }
        public ICommand SubmitCode
        {
            get
            {
                return submitCode ??
                  (submitCode = new Command(obj =>
                  {
                      try
                      {
                          if (code == Convert.ToInt32(codeFromBox))
                          {
                              App.db.Orders.Where(x => x.OrderId == orderId).FirstOrDefault().OrderState = Resources.acepted;
                              App.db.SaveChanges();
                              App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Ваш заказ был подтвержден");
                              EmailSenderService.SendTicket(Settings.Default.UserMail, "Чек заказа", EmailSenderService.GenerateTicket(order)).GetAwaiter();
                          }
                          else
                          {
                              App.NotifyWindow(Application.Current.Windows[0]).ShowError("Введен неверный код");
                          }
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
    }
}
