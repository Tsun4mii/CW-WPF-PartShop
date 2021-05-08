using CourseWork.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.SingletonView
{
    class SingletonAdmin
    {
        private static SingletonAdmin instance;
        public MainAdminVM StartViewModel { get; set; }
        public object MainAdminVM { get; internal set; }

        private SingletonAdmin(MainAdminVM startView)
        {
            StartViewModel = startView;
        }
        public static SingletonAdmin getInstance(MainAdminVM startViewModel = null)
        {
            return instance ?? (instance = new SingletonAdmin(startViewModel));
        }
    }
}
