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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace CourseWork.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ObservableCollection<Part> Parts { get; set; }
        public ObservableCollection<Part> PartsForSearch { get; set; }
        public Part part;
        public string textForSearch { get; set; }
        public ObservableCollection<Category> categories { get; set; }
        public ObservableCollection<Mark> Marks { get; set; }
        private Command addToCartCommand;
        private Command openFullInfoCommand;
        private Command findByCategory;
        private Command findByName;
        public int id { get; set; }
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
        private Category selectedCategory;
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");    
            }
        }
        
        public HomeViewModel()
        {
            using (PartShopDbContext db = new PartShopDbContext())
            {
                try
                {
                    Parts = new ObservableCollection<Part>(db.Parts);
                    PartsForSearch = new ObservableCollection<Part>(db.Parts);
                    categories = new ObservableCollection<Category>(db.Categories);
                    Marks = new ObservableCollection<Mark>(db.Marks);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        public ICommand AddToCartCommand
        {
            get
            {
                return addToCartCommand ??
                  (addToCartCommand = new Command(obj =>
                    {
                        Singleton.getInstance(null).MainViewModel.CurrentViewModel = new SearchViewModel(Parts);
                    }));
            }
        }
        public ICommand FindByCategory
        {
            get
            {
                return findByCategory ??
                  (findByCategory = new Command(obj =>
                  {
                      try
                      {
                          using (PartShopDbContext db = new PartShopDbContext())
                          {
                              Parts = new ObservableCollection<Part>(db.Parts.Where(x => x.CategoryId == SelectedCategory.CategoryId));
                              Singleton.getInstance(null).MainViewModel.CurrentViewModel = new SearchViewModel(Parts);
                          }
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
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
        public ICommand FindByName
        {
            get
            {
                return findByName ??
                  (findByName = new Command(obj =>
                  {
                      try
                      {
                          PartsForSearch = new ObservableCollection<Part>(App.db.Parts.Where(x => x.Name.Contains(textForSearch)));
                          Singleton.getInstance(null).MainViewModel.CurrentViewModel = new SearchViewModel(PartsForSearch);
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        private Command testCommand;
        public ICommand TestCommand
        {
            get
            {
                return testCommand ??
                  (testCommand = new Command(obj =>
                  {
                      try
                      {
                          App.db.Parts.Where(x => x.PartId == 13).FirstOrDefault().Quantity -= 3;
                          App.db.SaveChanges();
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
