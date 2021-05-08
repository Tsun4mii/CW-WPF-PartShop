using CourseWork.Commands;
using CourseWork.Models;
using CourseWork.SingletonView;
using CourseWork.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.ViewModels.AdminViewModels
{
    public class DeliveriesAdminVM : ViewModelBase
    {
        public ObservableCollection<Delivery> Deliveries { get; set; }
        public ObservableCollection<Delivery> deletedDeliveries { get; set; }
        public DeliveriesAdminVM()
        {
            Deliveries = new ObservableCollection<Delivery>(App.db.Deliveries);
            deletedDeliveries = new ObservableCollection<Delivery>();
        }
        private Command saveCommand;
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
        public UndoCommand<Delivery> deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new UndoCommand<Delivery>(obj =>
                  {
                      if (selectedDelivery != null)
                      {
                          Delivery delivery = new Delivery();
                          delivery = selectedDelivery;
                          Deliveries.Remove(delivery);
                          deletedDeliveries.Add(delivery);
                      }
                      return selectedDelivery;
                  },
                  obj =>
                  {
                      if (selectedDelivery != null)
                      {
                          Delivery delivery = new Delivery();
                          delivery = selectedDelivery;
                          Deliveries.Add(delivery);
                          deletedDeliveries.Remove(delivery);
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
                      foreach (Delivery i in deletedDeliveries)
                      {
                          App.db.Deliveries.Remove(i);
                      }
                      App.db.SaveChanges();
                      deletedDeliveries.Clear();
                  }));
            }
        }
        private Command openAddWindow;
        public ICommand OpenAddWindow
        {
            get
            {
                return openAddWindow ??
                  (openAddWindow = new Command(obj =>
                  {
                      AddDeliveryWindow add = new AddDeliveryWindow();
                      add.ShowDialog();
                  }));
            }
        }
        private Command refreshView;
        public ICommand RefreshView
        {
            get
            {
                return refreshView ??
                  (refreshView = new Command(obj =>
                  {
                      
                  }));
            }
        }
    }
}
