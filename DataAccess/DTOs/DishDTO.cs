using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restaurant.DataAccess.DTOs
{
    public class DishDTO
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string Recipe { get; set; }

        public string Ingredients { get; set; }
    }
}
