using CourseWork.Commands;
using CourseWork.Models;
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
    public class PartsAdminVM : ViewModelBase
    {
        public ObservableCollection<Part> Parts { get; set; }
        public ObservableCollection<Part> deletedParts { get; set; }
        public PartsAdminVM()
        {
            Parts = new ObservableCollection<Part>(App.db.Parts);
            deletedParts = new ObservableCollection<Part>();
        }
        private Command saveCommand;
        private Part selectedPart;
        public Part SelectedPart
        {
            get { return selectedPart; }
            set
            {
                selectedPart = value;
                OnPropertyChanged("SelectedPart");
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
                          if (selectedPart != null)
                          {
                              Part part = new Part();
                              part = selectedPart;
                              Parts.Remove(part);
                              deletedParts.Add(part);
                              App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Товар был удален");
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
                          foreach (Part i in deletedParts)
                          {
                              App.db.Parts.Remove(i);
                          }
                          App.db.SaveChanges();
                          deletedParts.Clear();
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        private Command addPart;
        public ICommand AddPart
        {
            get
            {
                return addPart ??
                  (addPart = new Command(obj =>
                  {
                      AddPartWindow window = new AddPartWindow();
                      window.ShowDialog();
                  }));
            }
        }
    }
}
