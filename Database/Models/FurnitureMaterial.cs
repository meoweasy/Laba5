using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class FurnitureMaterial
    {
        public int Id { get; set; }
        public int FurnitureId { get; set; }
        public int MaterialId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Material Material { get; set; }
        public virtual Furniture Furniture { get; set; }
    }
}
