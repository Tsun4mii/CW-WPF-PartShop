using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } //--ToDo: Проверить соответсвие типов 
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int DeliveryId { get; set; }

        //--ToDo: Добавить класс Delivery и создать навигационное свойство
    }
}
