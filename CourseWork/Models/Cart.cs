using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int PartId { get; set; }
        public Part Part { get; set; } 
        public int Quantity { get; set; }

    }
}
