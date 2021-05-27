using CourseWork.Properties;
using CourseWork.Services;
using CourseWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWork.Views
{
    /// <summary>
    /// Логика взаимодействия для CancelOrderView.xaml
    /// </summary>
    public partial class CancelOrderView : UserControl
    {
        CancelOrderViewModel a = new CancelOrderViewModel();
        public CancelOrderView()
        {
            InitializeComponent();
            DataContext = a;
            Random random = new Random();
            int code = random.Next(99999);
            EmailSenderService.SendCodeRefactor(Settings.Default.UserMail, code, "Код отмены", "Никому не сообщайте данный код! \nКод подтверждения: ").GetAwaiter();
            a.code = code;
        }
    }
}
