using CourseWork.Commands;
using CourseWork.Database;
using CourseWork.Models;
using CourseWork.Properties;
using CourseWork.Services;
using CourseWork.SingletonView;
using CourseWork.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public string login { get; set; }
        public string password { get; set; }
        public Command authCommand;
        public ICommand AuthCommand
        {
            get
            {
                return authCommand ??
                 (authCommand = new Command(obj =>
                 {
                     using (PartShopDbContext db = new PartShopDbContext())
                     {
                         User authUser = null;
                         password = SecurePassService.Hash(password);
                         authUser = db.Users.Where(a => a.Login == login && a.Password == password).FirstOrDefault();
                         if (authUser != null)
                         {
                             if(authUser.Is_admin == false)
                             {
                                 MainWindow main = new MainWindow();
                                 main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                 main.Show();
                                 AuthViewModel.Close();
                             }
                             else
                             {
                                 MainAdminView mainAdmin = new MainAdminView();
                                 mainAdmin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                 mainAdmin.Show();
                                 AuthViewModel.Close();
                             }
                             Settings.Default.UserMail = authUser.Email;
                             Settings.Default.UserId = authUser.Id;
                         }
                         else
                         {
                             MessageBox.Show("Bad");
                         }
                     }
                 }));
            }
        }
        public Command openRegCommand;
        public ICommand OpenRegCommand
        {
            get
            {
                return openRegCommand ??
                 (openRegCommand = new Command(obj =>
                 {
                     SingletonAuth.getInstance(null).StartViewModel.CurrentViewModel = new RegViewModel();
                 }));
            }
        }
        public void Close()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
    }
}
