using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicsContracts.BindingModels;
using BusinessLogicsContracts.ViewModels;

namespace BusinessLogicsContracts.BusinessLogicsContracts
{
    public interface IFurnitureLogic
    {
        List<FurnitureVM> Read(FurnitureBM model);
        void CreateOrUpdate(FurnitureBM model);
        void Delete(FurnitureBM model);
    }
}
