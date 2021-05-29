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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace CourseWork.ViewModels.AdminViewModels
{
    public class AddPartVM : ViewModelBase
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string ProviderId { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public string ImageLink { get; set; }
        public string CategoryId { get; set; }
        public ObservableCollection<Mark> Marks { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Provider> Providers { get; set; }
        public AddPartVM()
        {
            Marks = new ObservableCollection<Mark>(App.db.Marks);
            Categories = new ObservableCollection<Category>(App.db.Categories);
            Providers = new ObservableCollection<Provider>(App.db.Providers);
        }
        private Mark selectedMark;
        public Mark SelectedMark
        {
            get { return selectedMark; }
            set
            {
                selectedMark = value;
                OnPropertyChanged("SelectedMark");
            }
        }
        private Provider selectedProvider;
        public Provider SelectedProvider
        {
            get { return selectedProvider; }
            set
            {
                selectedProvider = value;
                OnPropertyChanged("SelectedProvider");
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
        private Command addPart;
        public ICommand AddPart
        {
            get
            {
                return addPart ??
                  (addPart = new Command(obj =>
                  {
                      try
                      {
                          if(Name == null | Quantity == null | selectedProvider == null| selectedMark == null | Price == null | Description == null | FullDescription == null | selectedCategory == null)
                          {
                              throw new Exception("Все ключевые поля должны быть заполнены");
                          }
                          if (Convert.ToInt32(Quantity) <= 0)
                          {
                              throw new Exception("Количество не может быть меньше или равна 0");
                          }
                          if (Convert.ToInt32(Price) <= 0)
                          {
                              throw new Exception("Цена не может быть меньше или равно 0");
                          }

                          Part part = new Part();
                          part.Name = Name;
                          part.Quantity = Convert.ToInt32(Quantity);
                          part.ProviderId = selectedProvider.ProviderId;
                          part.Price = Convert.ToDouble(Price);
                          part.Description = Description;
                          part.FullDescription = FullDescription;
                          part.ImageLink = ImageLink;
                          part.CategoryId = selectedCategory.CategoryId;
                          part.MarkId = selectedMark.MarkId;
                          App.db.Parts.Add(part);
                          App.db.SaveChanges();
                          this.Close();
                          App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Деталь была добавлена");
                      }
                      catch(Exception e)
                      {
                          App.NotifyWindow(Application.Current.Windows[0]).ShowError(e.Message);
                      }
                  }));
            }
        }
        public void Close()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
    }
}
