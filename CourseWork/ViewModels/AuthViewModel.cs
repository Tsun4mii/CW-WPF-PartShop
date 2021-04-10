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
    public class AuthViewModel
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
                             MessageBox.Show("Evrethin is ok");
                         }
                     }
                 }));
            }
        }
    }
}
