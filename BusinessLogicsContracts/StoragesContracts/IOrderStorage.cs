using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicsContracts.BindingModels;
using BusinessLogicsContracts.ViewModels;

namespace BusinessLogicsContracts.StoragesContracts
{
    public interface IOrderStorage
    {
        List<OrderVM> GetFullList();
        List<OrderVM> GetFilteredList(OrderBM model);
        OrderVM GetElement(OrderBM model);
        void Insert(OrderBM model);
        void Update(OrderBM model);
        void Delete(OrderBM model);
    }
}
