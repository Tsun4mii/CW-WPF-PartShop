using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
