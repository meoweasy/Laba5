using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicsContracts.BindingModels;
using BusinessLogicsContracts.ViewModels;

namespace BusinessLogicsContracts.BusinessLogicsContracts
{
    public interface IOrderLogic
    {
        List<OrderVM> Read(OrderBM model);
        void CreateOrder(CreateOrderBM model);
    }
}
