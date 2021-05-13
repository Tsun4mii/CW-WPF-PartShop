using CourseWork.Commands;
using CourseWork.Models;
using CourseWork.Properties;
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
    public class ProfileViewModel : ViewModelBase
    {
        public ObservableCollection<Order> Orders { get; set; }
        public ProfileViewModel()
        {
            Orders = new ObservableCollection<Order>(App.db.Orders.Where(x => x.UserId == Settings.Default.UserId));
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
                      ConfirmOrderViewModel.orderId = selectedOrder.OrderId;
                      Singleton.getInstance(null).MainViewModel.CurrentViewModel = new ConfirmOrderViewModel();
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
                      List<OrderedParts> prts = new List<OrderedParts>(App.db.OrderedParts.Where(x => x.OrderId == selectedOrder.OrderId));
                      //foreach(var p in selectedOrder.Parts)
                      //{
                      //    var orderedParts = App.db.Orders.Where(a => a.OrderId == selectedOrder.OrderId)
                      //    .Join(App.db.OrderedParts, t => t.OrderId, x => x.PartId, (t, x) => new { t, x })
                      //    .Join(App.db.Parts, l => l.x.PartId, k => k.PartId, (l, k) => new { l, k }).ToList();
                      //    foreach(var i in orderedParts)
                      //    {
                      //        MessageBox.Show(i.k.Name + i.k.Amount);
                      //    }
                      //}
                      foreach(var p in prts)
                      {
                          App.db.Parts.Where(x => x.PartId == p.PartId).FirstOrDefault().Quantity += p.Amount;
                      }
                      await App.db.SaveChangesAsync();
                  }));
            }
        }
    }
}
