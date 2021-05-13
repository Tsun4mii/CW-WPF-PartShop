using CourseWork.Commands;
using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        private Command addPart;
        public ICommand AddPart
        {
            get
            {
                return addPart ??
                  (addPart = new Command(obj =>
                  {
                      Part part = new Part();
                      part.Name = Name;
                      part.Quantity = Convert.ToInt32(Quantity);
                      part.ProviderId = Convert.ToInt32(ProviderId);
                      part.Price = Convert.ToDouble(Price);
                      part.Description = Description;
                      part.FullDescription = FullDescription;
                      part.ImageLink = ImageLink;
                      part.CategoryId = Convert.ToInt32(CategoryId);
                      App.db.Parts.Add(part);
                      App.db.SaveChanges();
                  }));
            }
        }
    }
}
