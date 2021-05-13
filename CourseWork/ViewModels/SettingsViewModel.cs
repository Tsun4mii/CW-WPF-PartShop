using CourseWork.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public List<CultureInfo> Langs { get; set; }
        private CultureInfo selectedLanguage;
        public CultureInfo SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                selectedLanguage = value;
                OnPropertyChanged("SelectedLanguage");
            }
        }
        public SettingsViewModel()
        {
            Langs = new List<CultureInfo>();
            foreach(var i in App.Language.Languages)
            {
                Langs.Add(i);
            }
        }
        private Command languageChanged;
        public ICommand LanguageChanged
        {
            get
            {
                return languageChanged ??
                  (languageChanged = new Command(obj =>
                  {
                      App.Language.Name = selectedLanguage;
                  }));
            }
        }
    }
}
