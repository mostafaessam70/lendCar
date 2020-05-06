using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Models
{
    public class Img
    {
        public int Id { get; set; }
      
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
    }
}
