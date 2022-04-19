using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicsContracts.BindingModels;
using BusinessLogicsContracts.ViewModels;

namespace BusinessLogicsContracts.StoragesContracts
{
    public interface IMaterialStorage
    {
        List<MaterialVM> GetFullList();
        List<MaterialVM> GetFilteredList(MaterialBM model);
        MaterialVM GetElement(MaterialBM model);
        void Insert(MaterialBM model);
        void Update(MaterialBM model);
        void Delete(MaterialBM model);

    }
}
