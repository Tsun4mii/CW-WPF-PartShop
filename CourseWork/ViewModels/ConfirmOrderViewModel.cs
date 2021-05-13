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
    public class ConfirmOrderViewModel : ViewModelBase
    {
        public int code;
        public string codeFromBox { get; set; }
        public Command submitCode;
        public static int orderId;
        public ConfirmOrderViewModel()
        { }
        public ICommand SubmitCode
        {
            get
            {
                return submitCode ??
                  (submitCode = new Command(obj =>
                  {
                      if (code == Convert.ToInt32(codeFromBox)) //--ToDo: Переделать. Передавать определенный заказ и его потом изменять.
                      {
                          App.db.Orders.Where(x => x.OrderId == orderId).FirstOrDefault().OrderState = Resources.acepted;
                          App.db.SaveChanges();
                      }
                  }));
            }
        }
    }
}
