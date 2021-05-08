using CourseWork.Database;
using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.ViewModels
{
    class SearchViewModel : ViewModelBase
    {
        public ObservableCollection<Part> Parts { get; set; }

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

    }
}
