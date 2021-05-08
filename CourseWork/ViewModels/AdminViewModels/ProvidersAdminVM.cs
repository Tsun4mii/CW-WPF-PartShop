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
        public UndoCommand<Provider> deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new UndoCommand<Provider>(obj =>
                  {
                      if (selectedProvider != null)
                      {
                          Provider provider = new Provider();
                          provider = selectedProvider;
                          Providers.Remove(provider);
                          deletedProviders.Add(provider);
                      }
                      return selectedProvider;
                  },
                  obj =>
                  {
                      if (selectedProvider != null)
                      {
                          Provider provider = new Provider();
                          provider = selectedProvider;
                          Providers.Add(provider);
                          deletedProviders.Remove(provider);
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
                      foreach (Provider i in deletedProviders)
                      {
                          App.db.Providers.Remove(i);
                      }
                      App.db.SaveChanges();
                      deletedProviders.Clear();
                  }));
            }
        }
    }
}

