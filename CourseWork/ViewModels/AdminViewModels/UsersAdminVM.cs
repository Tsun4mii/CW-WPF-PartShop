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

        public Command deleteCommand;
        private Command saveCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new Command(obj =>
                  {
                      try
                      {
                          if (selectedUser != null)
                          {
                              User user = new User();
                              user = selectedUser;
                              Users.Remove(user);
                              deletedUsers.Add(user);
                              App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Пользователь был удален");
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
                          foreach (User i in deletedUsers)
                          {
                              App.db.Users.Remove(i);
                          }
                          App.db.SaveChanges();
                          deletedUsers.Clear();
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
