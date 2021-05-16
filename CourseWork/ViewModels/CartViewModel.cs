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
using ToastNotifications.Messages;

namespace CourseWork.ViewModels
{
    public class CartViewModel : ViewModelBase
    {
        public static ObservableCollection<Part> Parts { get; set; }
        public ObservableCollection<Delivery> Deliveries { get; set; }
        public Delivery tmpDelivary = new Delivery { Price = 0 };
        public CartViewModel()
        {
            Parts = ConnectionBetweenViews.Parts;
            Deliveries = new ObservableCollection<Delivery>(App.db.Deliveries);
            foreach(Part i in Parts)
            {
                Summary += i.Price * i.Amount;
            }
        }
        private Delivery selectedDelivery;
        public Delivery SelectedDelivery
        {
            get { return selectedDelivery; }
            set
            {
                selectedDelivery = value;
                OnPropertyChanged("SelectedDelivery");
            }
        }
        private Part selectedPart;
        public Part SelectedPart
        {
            get { return selectedPart; }
            set
            {
                selectedPart = value;
                OnPropertyChanged("SelectedPart");
            }
        }
        private double summary;
        public double Summary
        {
            get { return summary; }
            set
            {
                summary = value;
                OnPropertyChanged("Summary");
            }
        }
        private Command deleteItem;
        public ICommand DeleteItem
        {
            get
            {
                return deleteItem ??
                  (deleteItem = new Command(obj =>
                  {
                      if(selectedPart.Amount > 1)
                      {
                          App.notifier.ShowSuccess($"Товар {selectedPart.Name} был удален из корзины");
                          selectedPart.Amount--;
                          Summary -= selectedPart.Price;
                      }
                      else
                      {
                          App.notifier.ShowSuccess($"Товар {selectedPart.Name} был удален из корзины");
                          Summary -= selectedPart.Price;
                          Parts.Remove(SelectedPart);
                      }                   
                  }));
            }
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
                  }));
            }
        }
        public Command deliveryChanged;
        public ICommand DeliveryChanged
        {
            get
            {
                return deliveryChanged ??
                  (deliveryChanged = new Command(obj =>
                  {
                      Summary -= tmpDelivary.Price;
                      tmpDelivary = selectedDelivery;
                      Summary += selectedDelivery.Price;
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
                List<OrderedParts> details = new List<OrderedParts>();
                foreach (Part i in Parts)                                      //--Огромный ебучий кастыль(но работает)
                {                                                              //--Решает баг с дублированием объектов в бд
                    details.Add(new OrderedParts()
                    {
                        OrderId = order.OrderId,
                        PartId = i.PartId,
                        Amount = i.Amount
                    });
                    Summary += i.Price;
                }
                order.Parts = details;
                order.UserId = Settings.Default.UserId;
                order.DeliveryId = 1;
                db.Orders.Add(order);
                ConfirmOrderViewModel.orderId = order.OrderId;
                Singleton.getInstance(null).MainViewModel.CurrentViewModel = new ConfirmOrderViewModel();
                await db.SaveChangesAsync();
            }
            
        }
    }
}
