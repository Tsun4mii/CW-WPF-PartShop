﻿using CourseWork.ViewModels.AdminViewModels;
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

namespace CourseWork.Views.AdminViews
{
    /// <summary>
    /// Логика взаимодействия для ProvidersAdminView.xaml
    /// </summary>
    public partial class ProvidersAdminView : UserControl
    {
        ProvidersAdminVM a = new ProvidersAdminVM();
        public ProvidersAdminView()
        {
            InitializeComponent();
            DataContext = a;
        }
    }
}