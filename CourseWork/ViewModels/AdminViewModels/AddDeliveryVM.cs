using CourseWork.Commands;
using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace CourseWork.ViewModels.AdminViewModels
{
    public class AddDeliveryVM : ViewModelBase
    {
        public string Description { get; set; }
        public string Price { get; set; }
        public string Name { get; set; }

        private Command addDelivery;
        public ICommand AddDelivery
        {
            get
            {
                return addDelivery ??
                  (addDelivery = new Command(obj =>
                  {
                      try
                      {
                          if(Name == null | Price == null | Description == null)
                          {
                              throw new Exception("Для добавления должны быть введены все параметры");
                          }
                          if(Convert.ToDouble(Price) < 0)
                          {
                              throw new Exception("Цена доставки не может быть меньше 0");
                          }
                          Delivery delivery = new Delivery();
                          delivery.Name = Name;
                          delivery.Price = Convert.ToDouble(Price);
                          delivery.Description = Description;
                          App.db.Deliveries.Add(delivery);
                          App.db.SaveChanges();
                          this.Close();
                          App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Служба доставки была добавлена");
                      }
                      catch(Exception e)
                      {
                          App.NotifyWindow(Application.Current.Windows[0]).ShowError(e.Message);
                      }
                  }));
            }
        }
        public void Close()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
    }
}
