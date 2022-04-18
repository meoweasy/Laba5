using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicsContracts.BindingModels;
using BusinessLogicsContracts.ViewModels;
using Database.Models;
using BusinessLogicsContracts.StoragesContracts;
using Microsoft.EntityFrameworkCore;

namespace Database.Implements
{
    public class FurnitureStorage : IFurnitureStorage
    {
        public List<FurnitureVM> GetFullList()
        {
            using var context = new Laba5Database();
            return context.Furnitures
            .Include(rec => rec.FurnitureMaterials)
            .ThenInclude(rec => rec.Material)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<FurnitureVM> GetFilteredList(FurnitureBM model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Laba5Database();
            return context.Furnitures
            .Include(rec => rec.FurnitureMaterials)
            .ThenInclude(rec => rec.Material)
            .Where(rec => rec.FurnitureName.Contains(model.FurnitureName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public FurnitureVM GetElement(FurnitureBM model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Laba5Database();
            var furniture = context.Furnitures
            .Include(rec => rec.FurnitureMaterials)
            .ThenInclude(rec => rec.Material)
            .FirstOrDefault(rec => rec.FurnitureName == model.FurnitureName || rec.Id == model.Id);
            return furniture != null ? CreateModel(furniture) : null;
        }
        public void Insert(FurnitureBM model)
        {
            using var context = new Laba5Database();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Furniture furniture = new Furniture()
                {
                    FurnitureName = model.FurnitureName,
                    Price = model.Price
                };
                context.Furnitures.Add(furniture);
                context.SaveChanges();
                CreateModel(model, furniture, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(FurnitureBM model)
        {
            using var context = new Laba5Database();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Furnitures.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(FurnitureBM model)
        {
            using var context = new Laba5Database();
            Furniture element = context.Furnitures.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.Furnitures.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Furniture CreateModel(FurnitureBM model, Furniture furniture,
       Laba5Database context)
        {
            furniture.FurnitureName = model.FurnitureName;
            furniture.Price = model.Price;
            if (model.Id.HasValue)
            {
                var furnitureMaterials = context.FurnitureMaterials.Where(rec =>
               rec.FurnitureId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.FurnitureMaterials.RemoveRange(furnitureMaterials.Where(rec =>
               !model.FurnitureMaterials.ContainsKey(rec.MaterialId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateMaterial in furnitureMaterials)
                {
                    updateMaterial.Count = model.FurnitureMaterials[updateMaterial.MaterialId].Item2;
                    model.FurnitureMaterials.Remove(updateMaterial.MaterialId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.FurnitureMaterials)
            {
                context.FurnitureMaterials.Add(new FurnitureMaterial
                {
                    FurnitureId = furniture.Id,
                    MaterialId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            return furniture;
        }
        private static FurnitureVM CreateModel(Furniture furniture)
        {
            return new FurnitureVM
            {
                Id = furniture.Id,
                FurnitureName = furniture.FurnitureName,
                Price = furniture.Price,
                FurnitureMaterials = furniture.FurnitureMaterials
            .ToDictionary(recPC => recPC.MaterialId,
            recPC => (recPC.Material?.MaterialName, recPC.Count))
            };
        }
    }
}
