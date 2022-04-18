using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class Laba5Database : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-O76UPIGE\SQLEXPRESS;Initial Catalog=Laba5Database;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Material> Materials { set; get; }
        public virtual DbSet<Furniture> Furnitures { set; get; }
        public virtual DbSet<FurnitureMaterial> FurnitureMaterials { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
    }
}
