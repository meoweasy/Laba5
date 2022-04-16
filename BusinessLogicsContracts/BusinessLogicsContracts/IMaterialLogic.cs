using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicsContracts.BindingModels;
using BusinessLogicsContracts.ViewModels;

namespace BusinessLogicsContracts.BusinessLogicsContracts
{
    public interface IMaterialLogic
    {
        List<MaterialVM> Read(MaterialBM model);
        void CreateOrUpdate(MaterialBM model);
        void Delete(MaterialBM model);
    }
}
