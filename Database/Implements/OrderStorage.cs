using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicsContracts.BindingModels;
using BusinessLogicsContracts.ViewModels;
using Database.Models;
using BusinessLogicsContracts.StoragesContracts;

namespace Database.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderVM> GetFullList()
        {
            using var context = new Laba5Database();
            return context.Orders
            .Select(CreateModel)
            .ToList();
        }
        public List<OrderVM> GetFilteredList(OrderBM model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Laba5Database();
            return context.Orders
            .Where(rec => rec.FurnitureId == model.FurnitureId)
            .Select(CreateModel)
            .ToList();
        }
        public OrderVM GetElement(OrderBM model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Laba5Database();
            var order = context.Orders
            .FirstOrDefault(rec => rec.FurnitureId == model.FurnitureId || rec.Id
           == model.Id);
            return order != null ? CreateModel(order) : null;
        }
        public void Insert(OrderBM model)
        {
            using var context = new Laba5Database();
            context.Orders.Add(CreateModel(model, new Order()));
            context.SaveChanges();
        }
        public void Update(OrderBM model)
        {
            using var context = new Laba5Database();
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Заказ не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(OrderBM model)
        {
            using var context = new Laba5Database();
            Order element = context.Orders.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Заказ не найден");
            }
        }
        private static Order CreateModel(OrderBM model, Order order)
        {
            order.FurnitureId = model.FurnitureId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        private static OrderVM CreateModel(Order order)
        {
            using var context = new Laba5Database();
            return new OrderVM
            {
                Id = order.Id,
                FurnitureId = order.FurnitureId,
                FurnitureName = context.Furnitures.FirstOrDefault(t => t.Id == order.FurnitureId)?.FurnitureName,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
