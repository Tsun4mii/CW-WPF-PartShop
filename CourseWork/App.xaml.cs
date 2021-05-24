using CourseWork.Database;
using CourseWork.Localization;
using CourseWork.Properties;
using CourseWork.SingletonView;
using CourseWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        CartViewModel a = new CartViewModel();
        public static PartShopDbContext db = new PartShopDbContext();
        private static Language language;
        public static Language Language
        {
            get => language ?? (language = new Language());
        }
        public App()
        {
            InitializeComponent();
            App.Language.Name = CourseWork.Properties.Settings.Default.DefaultLanguage;
        }
    }
}
