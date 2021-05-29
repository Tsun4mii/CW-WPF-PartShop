using CourseWork.Commands;
using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace CourseWork.ViewModels.AdminViewModels
{
    public class CategoriesAdminVM : ViewModelBase
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Category> deletedCategories { get; set; }
        public CategoriesAdminVM()
        {
            Categories = new ObservableCollection<Category>(App.db.Categories);
            deletedCategories = new ObservableCollection<Category>();
        }
        public string CategoryName { get; set; }
        public string Description { get; set; }
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
        private Command deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new Command(obj =>
                  {
                      if (SelectedCategory != null)
                      {
                          Category category = new Category();
                          category = selectedCategory;
                          Categories.Remove(category);
                          deletedCategories.Add(category);
                      }
                  }));
            }
        }
        private Command saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new Command(obj =>
                  {
                      foreach (Category i in deletedCategories)
                      {
                          App.db.Categories.Remove(i);
                      }
                      App.db.SaveChanges();
                      deletedCategories.Clear();
                  }));
            }
        }
        private Command addCommand;
        public ICommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new Command(obj =>
                  {
                      try
                      {
                          if (CategoryName == null | Description == null)
                          {
                              throw new Exception("Для добавления категории должны быть введены все данные");
                          }
                          Category category = new Category();
                          category.Name = CategoryName;
                          category.Description = Description;
                          App.db.Categories.Add(category);
                          App.db.SaveChanges();
                          App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Категория была добавлена");
                      }
                      catch(Exception e)
                      {
                          App.NotifyWindow(Application.Current.Windows[0]).ShowError(e.Message);
                      }
                  }));
            }
        }
    }
}

