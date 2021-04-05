using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Логин должен состоять только из английских символов и цифр")]
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Is_admin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [RegularExpression(@"^([a-zA-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessage = "Неверный формат email")]
        public string Email { get; set; }
    }
}
