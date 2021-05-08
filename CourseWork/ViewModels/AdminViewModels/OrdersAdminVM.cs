using CourseWork.Commands;
using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.ViewModels.AdminViewModels
{
    public class OrdersAdminVM : ViewModelBase
    {
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Order> deletedOrders { get; set; }
        public OrdersAdminVM()
        {
            Orders = new ObservableCollection<Order>(App.db.Orders);
            deletedOrders = new ObservableCollection<Order>();
        }
        private Command saveCommand;
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
        public UndoCommand<Order> deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new UndoCommand<Order>(obj =>
                  {
                      if (selectedOrder != null)
                      {
                          Order order = new Order();
                          order = selectedOrder;
                          Orders.Remove(order);
                          deletedOrders.Add(order);
                      }
                      return selectedOrder;
                  },
                  obj =>
                  {
                      if (selectedOrder != null)
                      {
                          Order user = new Order();
                          user = selectedOrder;
                          Orders.Add(user);
                          deletedOrders.Remove(user);
                      }
                  }
                ));
            }
        }
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new Command(obj =>
                  {
                      foreach (Order i in deletedOrders)
                      {
                          App.db.Orders.Remove(i);
                      }
                      App.db.SaveChanges();
                      deletedOrders.Clear();
                  }));
            }
        }
    }
}
