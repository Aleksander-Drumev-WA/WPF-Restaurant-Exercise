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
            DishesInOrder = new HashSet<DishInOrderDTO>();
        }

        [Key]
        public int Id { get; set; }

        public ICollection<DishInOrderDTO> DishesInOrder { get; set; }
    }
}
