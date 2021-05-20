using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Mark
    {
        [Key]
        public int MarkId { get; set; }
        public string MarkName { get; set; }
        public List<Part> Parts { get; set; } 
    }
}
