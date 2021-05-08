using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }
        public string CardNumber { get; set; }
        public int UserId { get; set; }
        public int CvvCode { get; set; }

    }
}
