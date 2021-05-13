using CourseWork.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Part : ViewModelBase
    {
        [Key]
        public int PartId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int ProviderId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }

        public string ImageLink { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Provider Provider { get; set; }
        [NotMapped]
        public int Amount {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }
        [NotMapped]
        private int amount;
    }
}
