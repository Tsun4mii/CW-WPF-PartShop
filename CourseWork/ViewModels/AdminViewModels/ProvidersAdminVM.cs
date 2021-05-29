using CourseWork.Commands;
using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
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
        public string Name { get; set; }
        public string Mail { get; set; }
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
        private Command addCommand;
        public ICommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new Command(obj =>
                  {
                      try
                      {
                          if (Name == null | Mail == null)
                          {
                              throw new Exception("Для добавления должны быть введены все параметры");
                          }
                          Provider provider = new Provider();
                          provider.Name = Name;
                          provider.Email = Mail;
                          App.db.Providers.Add(provider);
                          App.db.SaveChanges();
                      }
                      catch (DbEntityValidationException e)
                      {
                          foreach (DbEntityValidationResult validationRes in e.EntityValidationErrors)
                          {
                              foreach (DbValidationError err in validationRes.ValidationErrors)
                              {
                                  App.NotifyWindow(Application.Current.Windows[0]).ShowError(err.ErrorMessage);
                              }
                          }
                      }
                      catch (Exception e)
                      {
                          App.NotifyWindow(Application.Current.Windows[0]).ShowError(e.Message);
                      }
                  }));
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

