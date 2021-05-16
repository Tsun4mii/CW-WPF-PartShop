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
        public ObservableCollection<Category> Categories { get; set; }
        private double lowValue;
        public double LowValue
        {
            get { return lowValue; }
            set
            {
                lowValue = Math.Round(value, 2);
                OnPropertyChanged("LowValue");
            }
        }
        private double maxValue;
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = Math.Round(value);
                OnPropertyChanged("MaxValue");
            }
        }
        public string textForSearch { get; set; }
        public SearchViewModel() 
        {
            using (PartShopDbContext db = new PartShopDbContext())
            {
                Parts = new ObservableCollection<Part>(db.Parts);
                Categories = new ObservableCollection<Category>(db.Categories);
            }
        }
        private Category selectedCategory;
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
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
        private Command detailedSearch;
        public ICommand DetailedSearch
        {
            get
            {
                return detailedSearch ??
                  (detailedSearch = new Command(obj =>
                  {
                      if (selectedCategory != null)
                      {
                          Parts = new ObservableCollection<Part>(App.db.Parts.Where(x => (x.CategoryId == selectedCategory.CategoryId) &&
                                                                x.Price >= lowValue && x.Price <= maxValue));
                      }
                      else
                      {
                          Parts = new ObservableCollection<Part>(App.db.Parts.Where(x =>
                                                                 x.Price >= lowValue && x.Price <= maxValue));
                      }
                      Singleton.getInstance(null).MainViewModel.CurrentViewModel = new SearchViewModel(Parts);
                  }));
            }
        }
    }
}
