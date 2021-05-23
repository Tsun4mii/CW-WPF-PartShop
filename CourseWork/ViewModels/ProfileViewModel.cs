using CourseWork.Commands;
using CourseWork.Models;
using CourseWork.Properties;
using CourseWork.Services;
using CourseWork.SingletonView;
using CourseWork.Views;
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
    public class ProfileViewModel : ViewModelBase
    {
        public ObservableCollection<Order> Orders { get; set; }
        public User User { get; set; }
        public Card Card { get; set; }
        public string FullName { get; set; }
        public double Sum = 0;
        public ProfileViewModel()
        {
            Orders = new ObservableCollection<Order>(App.db.Orders.Where(x => x.UserId == Settings.Default.UserId));
            User = App.db.Users.Where(x => x.Id == Settings.Default.UserId).FirstOrDefault();
            Card = App.db.Cards.Where(x => x.UserId == Settings.Default.UserId).FirstOrDefault();
            if(Card == null)
            {
                Balance = 0;
                FullName = "Добавьте карту для покупок";
            }
            else
            {
                Balance = Card.Balance;
                FullName = User.FirstName + " " + User.LastName;
            }
        }
        private double balance;
        public double Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
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
        private Order selectedOrder;
        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }
        private Command acceptOrder;
        public ICommand AcceptOrder
        {
            get
            {
                return acceptOrder ??
                  (acceptOrder = new Command(obj =>
                  {
                      try
                      {
                          if(selectedOrder.OrderState == Resources.canceled)
                          {
                              throw new Exception("Невозможно подтвердить отмененный заказ");
                          }
                          if(selectedOrder.OrderState == Resources.acepted)
                          {
                              throw new Exception("Заказ уже подтвержден");
                          }
                          ConfirmOrderViewModel.orderId = selectedOrder.OrderId;
                          Singleton.getInstance(null).MainViewModel.CurrentViewModel = new ConfirmOrderViewModel();
                      }
                      catch(Exception e)
                      {
                          ErrorMessage = e.Message;
                      }
                  }));
            }
        }
        private Command cancelOrder;
        public ICommand CancelOrder
        {
            get
            {
                return cancelOrder ??
                  (cancelOrder = new Command(async obj =>
                  {
                      try
                      {
                          if(selectedOrder.OrderState == Resources.canceled)
                          {
                              throw new Exception("Данный заказ уже отменен");
                          }
                          //List<OrderedParts> prts = new List<OrderedParts>(App.db.OrderedParts.Where(x => x.OrderId == selectedOrder.OrderId));
                          //foreach (var p in prts)
                          //{
                          //    App.db.Parts.Where(x => x.PartId == p.PartId).FirstOrDefault().Quantity += p.Amount;
                          //    Sum += App.db.Parts.Where(x => x.PartId == p.PartId).FirstOrDefault().Price * p.Amount;
                          //}
                          //Sum += selectedOrder.Delivery.Price;
                          //selectedOrder.OrderState = Resources.canceled;
                          //Card.Balance += Sum;
                          //await App.db.SaveChangesAsync();
                          CancelOrderViewModel.orderId = selectedOrder.OrderId;
                          Singleton.getInstance(null).MainViewModel.CurrentViewModel = new CancelOrderViewModel();
                      }
                      catch(Exception e)
                      {
                          ErrorMessage = e.Message;
                      }
                  }));
            }
        }
        private Command addCard;
        public ICommand AddCard
        {
            get
            {
                return addCard ??
                  (addCard = new Command(obj =>
                  {
                      AddCardView window = new AddCardView();
                      window.ShowDialog();
                  }));
            }
        }
        private Command changePassword;
        public ICommand ChangePassword
        {
            get
            {
                return changePassword ??
                  (changePassword = new Command(obj =>
                  {
                      Singleton.getInstance(null).MainViewModel.CurrentViewModel = new ChangePasswordViewModel();
                  }));
            }
        }

    }
}
