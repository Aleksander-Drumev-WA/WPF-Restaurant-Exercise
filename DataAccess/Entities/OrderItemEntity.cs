using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.DataAccess.Entities
{
    public class OrderItemEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsCompleted { get; set; }

        public int DishId { get; set; }

        public virtual DishEntity Dish { get; set; }

        public int OrderId { get; set; }

        public virtual OrderEntity Order { get; set; }
    }
}
