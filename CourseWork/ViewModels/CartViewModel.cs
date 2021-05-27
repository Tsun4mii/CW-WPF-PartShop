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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace CourseWork.ViewModels
{
    public class CartViewModel : ViewModelBase
    {
        public static ObservableCollection<Part> Parts { get; set; }
        public static ObservableCollection<Category> Categories { get; set; }
        public static ObservableCollection<Mark> Marks { get; set; }
        public ObservableCollection<Delivery> Deliveries { get; set; }
        public Delivery tmpDelivary = new Delivery { Price = 0 };
        public Card Card { get; set; }
        public CartViewModel()
        {
            Parts = ConnectionBetweenViews.Parts;
            Deliveries = new ObservableCollection<Delivery>(App.db.Deliveries);
            Marks = new ObservableCollection<Mark>(App.db.Marks);
            Categories = new ObservableCollection<Category>(App.db.Categories);
            foreach(Part i in Parts)
            {
                Summary += i.Price * i.Amount;
            }
            Card = App.db.Cards.Where(x => x.UserId == Settings.Default.UserId).FirstOrDefault();
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
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
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
                          selectedPart.Amount--;
                          Summary -= selectedPart.Price;
                      }
                      else
                      {
                          Summary -= selectedPart.Price;
                          Parts.Remove(SelectedPart);
                      }
                      App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Товар был удален из корзины");
                  }));
            }
        }

        public Command buyCommand;
        public ICommand BuyCommand
        {
            get
            {
                return buyCommand ??
                  (buyCommand = new Command(obj =>
                  {
                      try
                      {
                          if (Card == null)
                          {
                              throw new Exception("Для покупки должна быть привязана карта");
                          }
                          AddOrder(Parts);
                      }
                      catch(Exception e)
                      {
                          ErrorMessage = e.Message;
                      }
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
        private Command openFullInfoCommand;
        public ICommand OpenFullInfo
        {
            get
            {
                return openFullInfoCommand ??
                  (openFullInfoCommand = new Command(obj =>
                  {
                      Singleton.getInstance(null).MainViewModel.CurrentViewModel = new FullInfoViewModel(selectedPart);
                  }));
            }
        }

        public void AddOrder(ObservableCollection<Part> parts)
        {
            using (PartShopDbContext db = new PartShopDbContext())
            {
                try
                {
                    if(Card.Balance < Summary)
                    {
                        throw new Exception("Недостаточно средств на счете");
                    }
                    else if( selectedDelivery == null)
                    {
                        throw new Exception("Должна быть выбрана доставка");
                    }
                    Order order = new Order();
                    order.OrderDate = DateTime.Now;
                    order.OrderState = Resources.waiting;
                    List<OrderedParts> details = new List<OrderedParts>();
                    foreach (Part i in Parts)                                      //--Огромный ебучий кастыль(но работает)
                    {                                                              //--Решает баг с дублированием объектов в бд
                        if(i.Amount > db.Parts.Where(x => x.PartId == i.PartId).FirstOrDefault().Quantity)
                        {
                            throw new Exception($"Товаров {i.Name} недостаточно на складе для заказа");
                        }
                        details.Add(new OrderedParts()
                        {
                            OrderId = order.OrderId,
                            PartId = i.PartId,
                            Amount = i.Amount
                        });
                        db.Parts.Where(x => x.PartId == i.PartId).FirstOrDefault().Quantity -= i.Amount;
                    }
                    order.Parts = details;
                    order.UserId = Settings.Default.UserId;
                    order.DeliveryId = selectedDelivery.DeliveryId;
                    db.Orders.Add(order);
                    Card.Balance -= Summary;
                    Parts.Clear();
                    Summary = 0;
                    db.SaveChanges();
                    ConfirmOrderViewModel.orderId = order.OrderId;
                    Singleton.getInstance(null).MainViewModel.CurrentViewModel = new ConfirmOrderViewModel();
                }
                catch(Exception e)
                {
                    ErrorMessage = e.Message;
                }
            }
            
        }
    }
}
