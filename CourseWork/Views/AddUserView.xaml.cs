using CourseWork.Database;
using CourseWork.Models;
using CourseWork.Services;
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

namespace CourseWork.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUserView.xaml
    /// </summary>
    public partial class AddUserView : Window
    {
        public AddUserView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = log.Text;
            string pass = SecurePassService.Hash(pas.Text);
            bool is_admin = Convert.ToBoolean(isadm.Text);
            string firstName = fist.Text;
            string lastdName = sec.Text;
            string email = mail.Text;
            User user = new User();
            user.Login = login;
            user.Password = pass;
            user.Is_admin = is_admin;
            user.FirstName = firstName;
            user.LastName = lastdName;
            user.Email = email;
            using (PartShopDbContext db = new PartShopDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

        }
    }
}
