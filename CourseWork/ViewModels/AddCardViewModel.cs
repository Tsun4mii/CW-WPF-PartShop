using CourseWork.Commands;
using CourseWork.Models;
using CourseWork.Properties;
using CourseWork.Views;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace CourseWork.ViewModels
{
    public class AddCardViewModel : ViewModelBase
    {
        public string CardNumber { get; set; }
        public int CvvCode { get; set; }
        public double Balance { get; set; }

        private Command addCard;
        public ICommand AddCard
        {
            get
            {
                return addCard ??
                  (addCard = new Command(obj =>
                  {
                      try
                      {
                          Card card = new Card();
                          card.CardNumber = CardNumber;
                          card.CvvCode = CvvCode;
                          card.Balance = Balance;
                          card.UserId = Settings.Default.UserId;
                          App.db.Cards.Add(card);
                          App.db.SaveChanges();
                          this.Close();
                          App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Карта была успешно привязана");
                          
                      }
                      catch (DbEntityValidationException e)
                      {
                          foreach (DbEntityValidationResult validationRes in e.EntityValidationErrors)
                          {
                              foreach (DbValidationError err in validationRes.ValidationErrors)
                              {
                                  MessageBox.Show(err.ErrorMessage);
                              }
                          }
                      }
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
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
