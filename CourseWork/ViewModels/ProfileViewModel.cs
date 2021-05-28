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
using ToastNotifications.Messages;

namespace CourseWork.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        public ObservableCollection<Order> Orders { get; set; }
        public User User { get; set; }
        public Card Card { get; set; }
        public int plusBalance { get; set; }
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
                  (cancelOrder = new Command(obj =>
                  {
                      try
                      {
                          if(selectedOrder.OrderState == Resources.canceled)
                          {
                              throw new Exception("Данный заказ уже отменен");
                          }
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
                      try
                      {
                          if (App.db.Cards.Where(x => x.UserId == Settings.Default.UserId).FirstOrDefault() != null)
                          {
                              throw new Exception("Невозможно привязаать еще 1 карту");
                          }
                          else
                          {
                              AddCardView window = new AddCardView();
                              window.ShowDialog();
                          }
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        private Command deleteCard;
        public ICommand DeleteCard
        {
            get
            {
                return deleteCard ??
                  (deleteCard = new Command(obj =>
                  {
                      try
                      {
                          Card card = App.db.Cards.Where(x => x.UserId == Settings.Default.UserId).FirstOrDefault();
                          if (card != null)
                          {
                              App.db.Cards.Remove(card);
                              App.db.SaveChanges();
                              App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Привязка карты была отменена");
                          }
                          else
                          {
                              App.NotifyWindow(Application.Current.Windows[0]).ShowError("Невозможно отвязать непривазанную карту");
                          }
                      }
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
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
        private Command deposit;
        public ICommand Deposit
        {
            get
            {
                return deposit ??
                  (deposit = new Command(obj =>
                  {
                      try
                      {
                          if (plusBalance > 0)
                          {
                              App.db.Cards.Where(x => x.CardId == Card.CardId).FirstOrDefault().Balance += plusBalance;
                              App.db.SaveChanges();
                              App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess($"Баланс был пополнен на {plusBalance}");
                          }
                          else
                          {
                              throw new Exception("Невозможно пополнить на отрицательную или нулевую сумму");
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
