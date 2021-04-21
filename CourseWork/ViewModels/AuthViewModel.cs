using CourseWork.Commands;
using CourseWork.Database;
using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.ViewModels
{
    public class AuthViewModel : ViewModelBase
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
                         authUser = db.Users.Where(a => a.Login == login && a.Password == password).FirstOrDefault();
                         if(authUser != null)
                         {
                             //MainWindow main = new MainWindow();
                             //main.Show();
                             //this.Close();
                         }
                     }
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
