using CourseWork.Commands;
using CourseWork.Database;
using CourseWork.Models;
using CourseWork.SingletonView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.ViewModels
{
    class SearchViewModel : ViewModelBase
    {
        public ObservableCollection<Part> Parts { get; set; }
        public string textForSearch { get; set; }
        public SearchViewModel() 
        {
            using (PartShopDbContext db = new PartShopDbContext())
            {
                Parts = new ObservableCollection<Part>(db.Parts);
            }
        }
        public SearchViewModel(ObservableCollection<Part> parts)
        {
            Parts = new ObservableCollection<Part>();
            foreach(Part i in parts)
            {
                Parts.Add(i);
            }
        }
        private Command findByName;
        public ICommand FindByName
        {
            get
            {
                return findByName ??
                  (findByName = new Command(obj =>
                  {
                      Parts = new ObservableCollection<Part>(App.db.Parts.Where(x => x.Name.Contains(textForSearch)));
                      Singleton.getInstance(null).MainViewModel.CurrentViewModel = new SearchViewModel(Parts);
                  }));
            }
        }

    }
}
