using CourseWork.Database;
using CourseWork.Models;
using CourseWork.Properties;
using CourseWork.Services;
using CourseWork.ViewModels;
using CourseWork.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel a = new MainViewModel();
        public static int Code { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = a;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    AuthView a = new AuthView();
        //    a.ShowDialog();
        //

        private void searchRadio_Checked(object sender, RoutedEventArgs e)
        {
            a.index = ((RadioButton)sender).Content.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthView v = new AuthView();
            v.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EmailSenderService.SendEmail(Settings.Default.UserMail).GetAwaiter();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddUserView a = new AddUserView();
            a.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (MainWindow.Code == Convert.ToInt32(CodeChk.Text))
            {
                MessageBox.Show("OK");
            }
            else
                MessageBox.Show("No");
        }
    }
}
