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
    public class MarksAdminVM : ViewModelBase
    {
        public ObservableCollection<Mark> Marks { get; set; }
        public ObservableCollection<Mark> deletedMarks { get; set; }
        public MarksAdminVM()
        {
            Marks = new ObservableCollection<Mark>(App.db.Marks);
            deletedMarks = new ObservableCollection<Mark>();
        }
        public string NewMark { get; set; }
        private Mark selectedMark;
        public Mark SelectedMark
        {
            get { return selectedMark; }
            set
            {
                selectedMark = value;
                OnPropertyChanged("SelectedMark");
            }
        }
        private Command deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new Command(obj =>
                  {
                      if (SelectedMark != null)
                      {
                          Mark mark = new Mark();
                          mark = selectedMark;
                          Marks.Remove(mark);
                          deletedMarks.Add(mark);
                      }
                  }));
            }
        }
        private Command saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new Command(obj =>
                  {
                      foreach (Mark i in deletedMarks)
                      {
                          App.db.Marks.Remove(i);
                      }
                      App.db.SaveChanges();
                      deletedMarks.Clear();
                  }));
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
                      Mark mark = new Mark();
                      mark.MarkName = NewMark;
                      App.db.Marks.Add(mark);
                      App.db.SaveChanges();
                      deletedMarks.Clear();
                  }));
            }
        }
    }
}
