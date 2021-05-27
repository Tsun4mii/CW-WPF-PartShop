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
        { }
        public static Window wndw = Application.Current.Windows[0];
        public static Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: wndw,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
        public ICommand SubmitCode
        {
            get
            {
                return submitCode ??
                  (submitCode = new Command(obj =>
                  {
                      if (code == Convert.ToInt32(codeFromBox)) //--ToDo: Переделать. Передавать определенный заказ и его потом изменять.
                      {
                          App.db.Orders.Where(x => x.OrderId == orderId).FirstOrDefault().OrderState = Resources.acepted;
                          App.db.SaveChanges();
                          notifier.ShowSuccess("Ваш заказ был подтвержден");
                          EmailSenderService.SendTicket(Settings.Default.UserMail, "Чек заказа", EmailSenderService.GenerateTicket(order)).GetAwaiter();
                      }
                      else
                      {
                          notifier.ShowError("Введенный вами код ошибочный");
                      }
                  }));
            }
        }
    }
}
