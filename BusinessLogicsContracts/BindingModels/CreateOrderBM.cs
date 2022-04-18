using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicsContracts.BindingModels
{
    public class CreateOrderBM
    {
        public int? Id { get; set; }
        public int FurnitureId { get; set; }
        public string FurnitureName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public DateTime? DateImplement { get; set; }
    }
}
