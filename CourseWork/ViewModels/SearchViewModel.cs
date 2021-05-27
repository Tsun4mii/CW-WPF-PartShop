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
using System.Windows;
using System.Windows.Input;

namespace CourseWork.ViewModels
{
    class SearchViewModel : ViewModelBase
    {
        public ObservableCollection<Part> Parts { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Mark> Marks { get; set; }
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
                Marks = new ObservableCollection<Mark>(db.Marks);
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
        private Part selectedPart;
        public Part SelectedPart
        {
            get { return selectedPart; }
            set
            {
                selectedPart = value;
                OnPropertyChanged("SelectedPart");
            }
        }
        public SearchViewModel(ObservableCollection<Part> parts)
        {
            Categories = new ObservableCollection<Category>(App.db.Categories);
            Parts = new ObservableCollection<Part>();
            foreach(Part i in parts)
            {
                Parts.Add(i);
            }
        }
        private Command openFullInfoCommand;
        public ICommand OpenFullInfo
        {
            get
            {
                return openFullInfoCommand ??
                  (openFullInfoCommand = new Command(obj =>
                  {
                      Singleton.getInstance(null).MainViewModel.CurrentViewModel = new FullInfoViewModel(selectedPart);
                  }));
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
                      try
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
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
    }
}
