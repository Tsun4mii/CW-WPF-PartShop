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

namespace CourseWork.ViewModels
{
    public class CancelOrderViewModel : ViewModelBase 
    {
        public int code;
        public static int orderId;
        public double Sum = 0;
        public Order orderForCancelation;
        public Card userCard;
        public string codeFromView { get; set; }

        public CancelOrderViewModel()
        {
            orderForCancelation = App.db.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();
            userCard = App.db.Cards.Where(x => x.UserId == Settings.Default.UserId).FirstOrDefault();

        }
        private Command cancelOrder;
        public ICommand CancelOrder
        {
            get
            {
                return cancelOrder ??
                  (cancelOrder = new Command(obj =>
                  {
                      try
                      {
                          if (Convert.ToInt32(codeFromView) == code)
                          {
                              //App.db.Orders.Where(x => x.OrderId == orderId).FirstOrDefault().OrderState = Resources.canceled;
                              //App.db.SaveChanges();
                              List<OrderedParts> prts = new List<OrderedParts>(App.db.OrderedParts.Where(x => x.OrderId == orderId));
                              foreach (var p in prts)
                              {
                                  App.db.Parts.Where(x => x.PartId == p.PartId & p.OrderId == orderId).FirstOrDefault().Quantity += p.Amount;
                                  Sum += App.db.Parts.Where(x => x.PartId == p.PartId).FirstOrDefault().Price * p.Amount;
                              }
                              Sum += orderForCancelation.Delivery.Price;
                              orderForCancelation.OrderState = Resources.canceled;
                              userCard.Balance += Sum;
                              App.db.SaveChangesAsync();
                          }
                          else
                          {
                              throw new Exception("Ошибка кода");
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
