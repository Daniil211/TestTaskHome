using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TestTask.DataBase
{
    public partial class ModelContainer : DbContext
    {
        public ModelContainer()
            : base("name=ModelContainer")
        {
        }

        public virtual DbSet<Managers> Managers { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Sells> Sells { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Managers>()
                .Property(e => e.Salary)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Managers>()
                .HasMany(e => e.Sells)
                .WithOptional(e => e.Managers)
                .HasForeignKey(e => e.ID_Manager);

            modelBuilder.Entity<Products>()
                .Property(e => e.Cost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Sells)
                .WithOptional(e => e.Products)
                .HasForeignKey(e => e.ID_Products);

            modelBuilder.Entity<Sells>()
                .Property(e => e.Sum)
                .HasPrecision(10, 2);
        }
    }
}
