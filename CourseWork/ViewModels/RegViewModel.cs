using CourseWork.Commands;
using CourseWork.Database;
using CourseWork.Models;
using CourseWork.Services;
using CourseWork.SingletonView;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.ViewModels
{
    public class RegViewModel : ViewModelBase
    {
        public string login { get; set; }
        public string password { get; set; }
        public string mail { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        private string errorMes;
        public string ErrorMessage
        {
            get { return errorMes; }
            set
            {
                this.errorMes = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        private Command backCommand;
        public ICommand BackCommand
        {
            get
            {
                return backCommand ??
                  (backCommand = new Command(obj =>
                  {
                      try
                      {
                          SingletonAuth.getInstance(null).StartViewModel.CurrentViewModel = new LoginViewModel();
                      }
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        public Command regCommand;
        public ICommand RegCommand
        {
            get
            {
                return regCommand ??
                 (regCommand = new Command(obj =>
                 {
                     try
                     {
                         using (PartShopDbContext db = new PartShopDbContext())
                         {
                             User user = new User();
                             user.Login = login;
                             if (password != null & password[0] != ' ')
                             {
                                 user.Password = SecurePassService.Hash(password);
                             }
                             else
                             {
                                 throw new Exception("Не верный формат пароля");
                             }
                             if(firstname == null || lastname == null)
                             {
                                 throw new Exception("Не введены имя или фамилия");
                             }
                             user.FirstName = firstname;
                             user.LastName = lastname;
                             user.Is_admin = false;
                             user.Email = mail;
                             if (login != null && password != null)
                             {
                                 if (db.Users.Any(a => a.Login == login || a.Email == mail))
                                 {
                                     throw new Exception("Пользователь с такими данными уже существует");
                                 }
                                 else
                                 {
                                     db.Users.Add(user);
                                     db.SaveChanges();
                                     SingletonAuth.getInstance(null).StartViewModel.CurrentViewModel = new LoginViewModel();
                                 }
                             }
                         }
                     }
                     catch(DbEntityValidationException e)
                     {
                         foreach(DbEntityValidationResult validationRes in e.EntityValidationErrors)
                         {
                             foreach(DbValidationError err in validationRes.ValidationErrors)
                             {
                                 ErrorMessage = err.ErrorMessage;
                             }
                         }
                     }
                     catch(Exception e)
                     {
                         ErrorMessage = e.Message;
                     }
                 }));
            }
        }
    }
}
