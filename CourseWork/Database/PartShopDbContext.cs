using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Database
{
    public class PartShopDbContext : DbContext
    {
        public PartShopDbContext() : base("DBConnection") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<OrderedParts> OrderedParts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderedParts>().HasKey(t => new { t.PartId, t.OrderId });
            modelBuilder.Entity<OrderedParts>()
               .HasRequired(u => u.Order)
               .WithMany(ur => ur.Parts)
               .HasForeignKey(ui => ui.PartId);

            modelBuilder.Entity<OrderedParts>()
                .HasRequired(r => r.Order)
                .WithMany(ur => ur.Parts)
                .HasForeignKey(ri => ri.OrderId);
        }
    }
}
