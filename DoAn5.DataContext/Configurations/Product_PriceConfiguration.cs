using DoAn5.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Configurations
{
    public class Product_PriceConfiguration : IEntityTypeConfiguration<Product_Price>
    {
        public void Configure(EntityTypeBuilder<Product_Price> builder)
        {
            builder.ToTable("Product_Prices");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Price).IsRequired();

            builder.HasOne<Product>().WithMany().HasForeignKey(x => x.Product_Id);
        }
    }
}
