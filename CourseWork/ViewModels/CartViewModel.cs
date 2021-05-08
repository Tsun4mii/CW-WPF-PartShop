using CourseWork.Commands;
using CourseWork.Database;
using CourseWork.Models;
using CourseWork.Properties;
using CourseWork.Services;
using CourseWork.SingletonView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.ViewModels
{
    public class CartViewModel : ViewModelBase
    {
        public static ObservableCollection<Part> Parts { get; set; }
        public CartViewModel()
        {
            Parts = ConnectionBetweenViews.Parts;
        }
        public Command buyCommand;
        public ICommand BuyCommand
        {
            get
            {
                return buyCommand ??
                  (buyCommand = new Command(async obj =>
                  {
                      await AddOrder(Parts);
                      Singleton.getInstance(null).MainViewModel.CurrentViewModel = new ConfirmOrderViewModel();
                  }));
            }
        }

        public async Task AddOrder(ObservableCollection<Part> parts)
        {
            using (PartShopDbContext db = new PartShopDbContext())
            {
                Order order = new Order();
                order.OrderDate = DateTime.Now;
                order.OrderState = Resources.waiting;
                List<Part> details = new List<Part>();
                foreach (Part i in Parts)                                      //--Огромный ебучий кастыль(но работает)
                {                                                              //--Решает баг с дублированием объектов в бд
                    details.Add(db.Parts.Where(x => x.PartId == i.PartId).FirstOrDefault());
                }                                                               
                order.Parts = details;
                order.UserId = Settings.Default.UserId;
                order.DeliveryId = 1;
                db.Orders.Add(order);
                await db.SaveChangesAsync();
            }
            
        }
    }
}
