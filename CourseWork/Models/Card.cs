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
        [RegularExpression(@"[0-9]{4}+\s{1}+[0-9]{4}+\s{1}+[0-9]{4}+\s{1}+[0-9]{4}", ErrorMessage = "Номер карты не соответствует стандарту")]
        public string CardNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public double Balance { get; set; }
        [RegularExpression(@"[0-9]{3}", ErrorMessage = "CVV код должен состоят из 3-ех цифр")]
        public int CvvCode { get; set; }

    }
}
