using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace BusinessLogicsContracts.ViewModels
{
    public class MaterialVM
    {
        public int Id { get; set; }
        [DisplayName("Название материала")]
        public string MaterialName { get; set; }
    }
}
