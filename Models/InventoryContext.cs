using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Models
{
    public class InventoryContext:DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {

        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id=1, Name="Seller"},
                new Role { Id=2, Name="Manager"}
                );
            modelBuilder.Entity<Account>().HasData(
                new Account { ID=1, FirstName="Test", LastName="Account", Email="Test@gmail.com", Password="Admin@123", RoleID=2},
                new Account { ID=2, FirstName="Test", LastName="Seller", Email="Seller@gmail.com", Password="Seller@123",RoleID=1}
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, CategoryName="Food and Grocery"},
                new Category { Id=2, CategoryName="Clothing"},
                new Category { Id=3, CategoryName="Electronics"},
                new Category { Id=4, CategoryName="Home and Garden"}
            );
            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { ID=1, Location="Chicago", ContactPerson="Jhon", Email="Jhon@gmail.com", Phone="8745639836" },
                new Warehouse { ID=2, Location="NewYork", ContactPerson="Rony", Email="Rony@gmail.com", Phone="8445639836" }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId=1, ProductName="Melody", ProductCode="MLDY", CategoryID=1,WarehouseID=1,Price=10, Quantity=10,Vendor="Cadbury", Description="Choclate"}
                );

            modelBuilder.Entity<Category>().HasMany(c => c.Products).WithOne(c=>c.Category).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Warehouse>().HasMany(c => c.Products).WithOne(c=>c.Warehouse).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
