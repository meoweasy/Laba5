using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicsContracts.BindingModels;
using BusinessLogicsContracts.ViewModels;

namespace BusinessLogicsContracts.StoragesContracts
{
    public interface IFurnitureStorage
    {
        List<FurnitureVM> GetFullList();
        List<FurnitureVM> GetFilteredList(FurnitureBM model);
        FurnitureVM GetElement(FurnitureBM model);
        void Insert(FurnitureBM model);
        void Update(FurnitureBM model);
        void Delete(FurnitureBM model);
    }
}
