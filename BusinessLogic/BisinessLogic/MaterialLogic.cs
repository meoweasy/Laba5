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
    public class MaterialLogic : IMaterialLogic
    {
        private readonly IMaterialStorage _materialStorage;
        public MaterialLogic(IMaterialStorage materialStorage)
        {
            _materialStorage = materialStorage;
        }
        public List<MaterialVM> Read(MaterialBM model)
        {
            if (model == null)
            {
                return _materialStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<MaterialVM> { _materialStorage.GetElement(model)
};
            }
            return _materialStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(MaterialBM model)
        {
            var element = _materialStorage.GetElement(new MaterialBM
            {
                MaterialName = model.MaterialName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть материал с таким названием");
            }
            if (model.Id.HasValue)
            {
                _materialStorage.Update(model);
            }
            else
            {
                _materialStorage.Insert(model);
            }
        }
        public void Delete(MaterialBM model)
        {
            var element = _materialStorage.GetElement(new MaterialBM
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Материал не найден");
            }
            _materialStorage.Delete(model);
        }
    }
}
