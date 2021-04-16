using CourseWork.Commands;
using CourseWork.SingletonView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string index { get; set; }
        private ViewModelBase currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        public MainViewModel()
        {
            Singleton.getInstance(this);
            CurrentViewModel = new HomeViewModel();
        }

        public Command changeCommand;
        public ICommand ChangeCommand
        {
            get
            {
                return changeCommand ??
                 (changeCommand = new Command(obj =>
                 {
                     switch(index)
                     {
                         case "Search":
                             Singleton.getInstance(null).MainViewModel.CurrentViewModel = new SearchViewModel();
                             break;
                         case "Home":
                             Singleton.getInstance(null).MainViewModel.CurrentViewModel = new HomeViewModel();
                             break;
                         case "Settings":
                             Singleton.getInstance(null).MainViewModel.CurrentViewModel = new SettingsViewModel();
                             break;
                     }
                 }));
            }
        }
    }
}
