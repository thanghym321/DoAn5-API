using DoAn5.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Configurations
{
    public class Product_ImageConfiguration : IEntityTypeConfiguration<Product_Image>
    {
        public void Configure(EntityTypeBuilder<Product_Image> builder)
        {
            builder.ToTable("Product_Images");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Image).HasColumnType("varchar(500)");

            builder.HasOne<Product>().WithMany().HasForeignKey(fk => fk.Product_Id);

        }
    }
}
