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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace CourseWork.ViewModels
{
    public class FullInfoViewModel : ViewModelBase
    {
        public Part Part { get; set; }
        public Category Category { get; set; }

        public FullInfoViewModel(Part d)
        {
            Part = new Part();
            Part = d;
            Category = App.db.Categories.Where(x => x.CategoryId == Part.CategoryId).FirstOrDefault();
        }
        public FullInfoViewModel()
        {

        }
        public Command addToCart;
        public ICommand AddToCart
        {
            get
            {
                return addToCart ??
                  (addToCart = new Command(obj =>
                  {
                      try
                      {
                          Part item = CartViewModel.Parts.Where(x => x.PartId == Part.PartId).FirstOrDefault();
                          if (item != null)
                          {
                              item.Amount++;
                          }
                          else
                          {
                              Part.Amount = 1;
                              CartViewModel.Parts.Add(Part);
                          }
                          App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Товар был добавлен в корзину");
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
