using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public DateTime OrderDate { get; set; } 
        public List<Part> Parts { get; set; }
        public string OrderState { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        //--ToDo: Добавить класс Delivery и создать навигационное свойство
    }
}
