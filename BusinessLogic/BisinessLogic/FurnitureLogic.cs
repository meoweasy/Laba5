using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicsContracts.BindingModels;
using BusinessLogicsContracts.BusinessLogicsContracts;
using BusinessLogicsContracts.StoragesContracts;
using BusinessLogicsContracts.ViewModels;

namespace BusinessLogic.BisinessLogic
{
    public class FurnitureLogic : IFurnitureLogic
    {
        private readonly IFurnitureStorage _furnitureStorage;
        public FurnitureLogic(IFurnitureStorage furnitureStorage)
        {
            _furnitureStorage = furnitureStorage;
        }
        public List<FurnitureVM> Read(FurnitureBM model)
        {
            if (model == null)
            {
                return _furnitureStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<FurnitureVM> { _furnitureStorage.GetElement(model)
};
            }
            return _furnitureStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(FurnitureBM model)
        {
            var element = _furnitureStorage.GetElement(new FurnitureBM
            {
                FurnitureName = model.FurnitureName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            if (model.Id.HasValue)
            {
                _furnitureStorage.Update(model);
            }
            else
            {
                _furnitureStorage.Insert(model);
            }
        }
        public void Delete(FurnitureBM model)
        {
            var element = _furnitureStorage.GetElement(new FurnitureBM
            {
                Id =
           model.Id
            });
            if (element == null)
            {
                throw new Exception("Изделие не найдено");
            }
            _furnitureStorage.Delete(model);
        }

    }
}
