using CourseWork.Commands;
using CourseWork.Models;
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
    public class ProvidersAdminVM : ViewModelBase
    {
        public ObservableCollection<Provider> Providers { get; set; }
        public ObservableCollection<Provider> deletedProviders { get; set; }
        public ProvidersAdminVM()
        {
            Providers = new ObservableCollection<Provider>(App.db.Providers);
            deletedProviders = new ObservableCollection<Provider>();
        }
        private Command saveCommand;
        private Provider selectedProvider;
        public Provider SelectedProvider
        {
            get { return selectedProvider; }
            set
            {
                selectedProvider = value;
                OnPropertyChanged("SelectedProvider");
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
                      try
                      {
                          if (selectedProvider != null)
                          {
                              Provider provider = new Provider();
                              provider = selectedProvider;
                              Providers.Remove(provider);
                              deletedProviders.Add(provider);
                              App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Поставщик был удален");
                          }
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
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
                      try
                      {
                          foreach (Provider i in deletedProviders)
                          {
                              App.db.Providers.Remove(i);
                          }
                          App.db.SaveChanges();
                          deletedProviders.Clear();
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

