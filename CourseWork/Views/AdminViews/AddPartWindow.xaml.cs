using CourseWork.ViewModels.AdminViewModels;
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
using System.Windows.Shapes;

namespace CourseWork.Views.AdminViews
{
    /// <summary>
    /// Логика взаимодействия для AddPartWindow.xaml
    /// </summary>
    public partial class AddPartWindow : Window
    {
        public AddPartVM a = new AddPartVM();
        public AddPartWindow()
        {
            InitializeComponent();
            DataContext = a;
        }
    }
}
