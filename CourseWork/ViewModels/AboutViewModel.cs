using CourseWork.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private Command openVk;
        public ICommand OpenVk
        {
            get
            {
                return openVk ??
                 (openVk = new Command(obj =>
                 {
                     Process.Start("https://vk.com/tsun4mi");
                 }));
            }
        }
        private Command openInst;
        public ICommand OpenInst
        {
            get
            {
                return openInst ??
                 (openInst = new Command(obj =>
                 {
                     Process.Start("https://www.instagram.com/shust_ts/");
                 }));
            }
        }
        private Command openTeleg;
        public ICommand OpenTeleg
        {
            get
            {
                return openTeleg ??
                 (openTeleg = new Command(obj =>
                 {
                     Process.Start("https://t.me/Tsun4mi");
                 }));
            }
        }
    }
}
