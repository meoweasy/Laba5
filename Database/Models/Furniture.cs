using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Furniture
    {
        public int Id { get; set; }
        [Required]
        public string FurnitureName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("FurnitureId")]
        public virtual List<FurnitureMaterial> FurnitureMaterials { get; set; }
        [ForeignKey("FurnitureId")]
        public virtual List<Order> Orders { get; set; }
    }
}
