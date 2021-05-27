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
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;

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
        public Command deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new Command(obj =>
                  {
                      if (selectedDelivery != null)
                      {
                          Delivery delivery = new Delivery();
                          delivery = selectedDelivery;
                          Deliveries.Remove(delivery);
                          deletedDeliveries.Add(delivery);
                          App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Служба доставки была удалена");
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
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new DeliveriesAdminVM();
                  }));
            }
        }
    }
}
