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

namespace CourseWork.ViewModels.AdminViewModels
{
    public class UsersAdminVM : ViewModelBase
    {
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<User> deletedUsers { get; set; }

        public UsersAdminVM()
        {
            Users = new ObservableCollection<User>(App.db.Users);
            deletedUsers = new ObservableCollection<User>();
        }
        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public static UndoManager<User> undoCommandManager = new UndoManager<User>();

        public UndoCommand<User> deleteCommand;
        private Command undoCommand;
        private Command redoCommand;
        private Command saveCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new UndoCommand<User>(obj =>
                  {
                      if(selectedUser != null)
                      {
                          User user = new User();
                          user = selectedUser;
                          Users.Remove(user);
                          deletedUsers.Add(user);
                      }
                      return selectedUser;
                  },
                  obj =>
                  {
                      if(selectedUser != null)
                      {
                          User user = new User();
                          user = selectedUser;
                          Users.Add(user);
                          deletedUsers.Remove(user);
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
                      foreach(User i in deletedUsers)
                      {
                          App.db.Users.Remove(i);
                      }
                      App.db.SaveChanges();
                      deletedUsers.Clear();
                  }));
            }
        }
        public ICommand UndoCommand
        {
            get
            {
                return undoCommand ??
                  (undoCommand = new Command(obj =>
                  {
                      undoCommandManager.Undo();
                  }));
            }
        }
        public ICommand RedoCommand
        {
            get
            {
                return redoCommand ??
                  (redoCommand = new Command(obj =>
                  {
                      undoCommandManager.Redo();
                  }));
            }
        }
    }
}
