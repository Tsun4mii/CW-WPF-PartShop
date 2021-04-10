using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Delivery
    {
        [Key]
        public int DeliveryId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }

    }
}
