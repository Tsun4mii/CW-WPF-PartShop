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
    public class HomeViewModel : ViewModelBase
    {
        public List<Part> Parts { get; set; }

        public HomeViewModel()
        {
            using (PartShopDbContext db = new PartShopDbContext())
            {
                Parts = new List<Part>();
                Parts = db.Parts.ToList();
            }
        }

    }
}
