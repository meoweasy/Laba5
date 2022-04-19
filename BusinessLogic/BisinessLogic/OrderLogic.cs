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
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        public OrderLogic(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }
        public List<OrderVM> Read(OrderBM model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderVM> { _orderStorage.GetElement(model)
};
            }
            return _orderStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(OrderBM model)
        {
            var element = _orderStorage.GetElement(new OrderBM
            {
                Id = model.Id
            }) ;
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть заказ с таким ID");
            }
            if (model.Id.HasValue)
            {
                _orderStorage.Update(model);
            }
            else
            {
                _orderStorage.Insert(model);
            }
        }
        public void Delete(OrderBM model)
        {
            var element = _orderStorage.GetElement(new OrderBM
            {
                Id =
           model.Id
            });
            if (element == null)
            {
                throw new Exception("Заказ не найден");
            }
            _orderStorage.Delete(model);
        }
    }
}
