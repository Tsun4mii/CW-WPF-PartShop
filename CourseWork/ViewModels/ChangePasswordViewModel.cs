using CourseWork.Commands;
using CourseWork.Properties;
using CourseWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.ViewModels
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        public string newPassword { get; set; }
        public string repeatPassword { get; set; }
        public int code;
        public int codeFromView { get; set; }

        public ChangePasswordViewModel()
        {

        }
        private Command generteCode;
        public ICommand GenerateCode
        {
            get
            {
                return generteCode ??
                  (generteCode = new Command(obj =>
                  {
                      try
                      {
                          Random random = new Random();
                          code = random.Next(99999);
                          EmailSenderService.SendCode(Settings.Default.UserMail, code).GetAwaiter();
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        private Command confirmChange;
        public ICommand ConfirmChange
        {
            get
            {
                return confirmChange ??
                  (confirmChange = new Command(obj =>
                  {
                      try
                      {
                          if (newPassword == repeatPassword & code == codeFromView)
                          {
                              App.db.Users.Where(x => x.Id == Settings.Default.UserId).FirstOrDefault().Password = SecurePassService.Hash(newPassword);
                              App.db.SaveChanges();
                          }
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
