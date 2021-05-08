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
        public UndoCommand<Part> deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new UndoCommand<Part>(obj =>
                  {
                      if (selectedPart != null)
                      {
                          Part part = new Part();
                          part = selectedPart;
                          Parts.Remove(part);
                          deletedParts.Add(part);
                      }
                      return selectedPart;
                  },
                  obj =>
                  {
                      if (selectedPart != null)
                      {
                          Part part = new Part();
                          part = selectedPart;
                          Parts.Add(part);
                          deletedParts.Remove(part);
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
                      foreach (Part i in deletedParts)
                      {
                          App.db.Parts.Remove(i);
                      }
                      App.db.SaveChanges();
                      deletedParts.Clear();
                  }));
            }
        }

    }
}
