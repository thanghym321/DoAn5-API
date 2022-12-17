using DoAn5.DataContext.Configurations;
using DoAn5.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.EF
{
    public class DoAn5DbContext : DbContext
    {
        public DoAn5DbContext( DbContextOptions<DoAn5DbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new Export_Invoice_DetailConfiguration());
            modelBuilder.ApplyConfiguration(new Export_InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new Import_Invoice_DetailConfiguration());
            modelBuilder.ApplyConfiguration(new Import_InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new ProducerConfiguration());
            modelBuilder.ApplyConfiguration(new Product_ImageConfiguration());
            modelBuilder.ApplyConfiguration(new Product_PriceConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());
            modelBuilder.ApplyConfiguration(new SlideConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Account> Accounts { get; set; }   
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Export_Invoice> Export_Invoices { get; set; }
        public DbSet<Export_Invoice_Detail> Export_Invoice_Details { get; set; }
        public DbSet<Import_Invoice> Import_Invoices { get; set; }
        public DbSet<Import_Invoice_Detail> Import_Invoice_Details { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Image> Product_Images { get; set; }
        public DbSet<Product_Price> Product_Prices { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
