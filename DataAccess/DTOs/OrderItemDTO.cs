using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.DataAccess.DTOs
{
    public class OrderItemDTO
    {
        [Key]
        public int Id { get; set; }

        public bool IsCompleted { get; set; }

        public int DishId { get; set; }

        public virtual DishDTO Dish { get; set; }

        public int OrderId { get; set; }

        public virtual OrderDTO Order { get; set; }
    }
}
