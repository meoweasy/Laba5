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
    public class MaterialStorage : IMaterialStorage
    {
        public List<MaterialVM> GetFullList()
        {
            using var context = new Laba5Database();
            return context.Materials
            .Select(CreateModel)
            .ToList();
        }
        public List<MaterialVM> GetFilteredList(MaterialBM model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Laba5Database();
            return context.Materials
            .Where(rec => rec.MaterialName.Contains(model.MaterialName))
            .Select(CreateModel)
            .ToList();
        }
        public MaterialVM GetElement(MaterialBM model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new Laba5Database();
            var material = context.Materials
            .FirstOrDefault(rec => rec.MaterialName == model.MaterialName || rec.Id
           == model.Id);
            return material != null ? CreateModel(material) : null;
        }
        public void Insert(MaterialBM model)
        {
            using var context = new Laba5Database();
            context.Materials.Add(CreateModel(model, new Material()));
            context.SaveChanges();
        }
        public void Update(MaterialBM model)
        {
            using var context = new Laba5Database();
            var element = context.Materials.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Материал не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(MaterialBM model)
        {
            using var context = new Laba5Database();
            Material element = context.Materials.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                context.Materials.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Материал не найден");
            }
        }
        private static Material CreateModel(MaterialBM model, Material material)
        {
            material.MaterialName = model.MaterialName;
            return material;
        }
        private static MaterialVM CreateModel(Material material)
        {
            return new MaterialVM
            {
                Id = material.Id,
                MaterialName = material.MaterialName
            };
        }
    }
}
