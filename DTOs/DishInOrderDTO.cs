using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.DTOs
{
    public class DishInOrderDTO
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public bool IsReady { get; set; }

        public int OrderId { get; set; }

        public OrderDTO Order { get; set; }
    }
}
