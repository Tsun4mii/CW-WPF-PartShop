using CourseWork.Commands;
using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
                      Delivery delivery = new Delivery();
                      delivery.Name = Name;
                      delivery.Price = Convert.ToDouble(Price);
                      delivery.Description = Description;
                      App.db.Deliveries.Add(delivery);
                      App.db.SaveChanges();
                  }));
            }
        }
    }
}
