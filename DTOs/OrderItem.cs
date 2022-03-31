using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.DTOs
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public bool IsCompleted { get; set; }

        public int DishId { get; set; }

        public DishDTO Dish { get; set; }

        public int OrderId { get; set; }

        public OrderDTO Order { get; set; }
    }
}
