﻿using CourseWork.Commands;
using CourseWork.SingletonView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.ViewModels.AdminViewModels
{
    class MainAdminVM : ViewModelBase
    {
        private ViewModelBase currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        public MainAdminVM()
        {
            SingletonAdmin.getInstance(this);
            CurrentViewModel = new HomeAdminVM();
        }

        public Command openUsers;
        public ICommand OpenUsers
        {
            get
            {
                return openUsers ??
                  (openUsers = new Command(obj =>
                  {
                        SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new UsersAdminVM();
                  }));
            }
        }
        public Command openOrders;
        public ICommand OpenOrders
        {
            get
            {
                return openOrders ??
                  (openOrders = new Command(obj =>
                  {
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new OrdersAdminVM();
                  }));
            }
        }
        public Command openProviders;
        public ICommand OpenProviders
        {
            get
            {
                return openProviders ??
                  (openProviders = new Command(obj =>
                  {
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new ProvidersAdminVM();
                  }));
            }
        }
        public Command openParts;
        public ICommand OpenParts
        {
            get
            {
                return openParts ??
                  (openParts = new Command(obj =>
                  {
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new PartsAdminVM();
                  }));
            }
        }
        public Command openDeliveries;
        public ICommand OpenDeliveries
        {
            get
            {
                return openDeliveries ??
                  (openDeliveries = new Command(obj =>
                  {
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new DeliveriesAdminVM();
                  }));
            }
        }
    }
}