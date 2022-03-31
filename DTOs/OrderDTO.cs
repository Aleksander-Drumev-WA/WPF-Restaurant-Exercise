using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restaurant.DTOs
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            OrderItems = new HashSet<OrderItem>();
            CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
