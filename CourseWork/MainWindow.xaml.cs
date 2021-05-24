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



    }
}
