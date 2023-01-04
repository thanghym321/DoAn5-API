using DoAn5.DataContext.Entities;
using DoAn5.DataContext.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Name).HasColumnType("nvarchar(250)");
            builder.Property(x => x.Description).HasColumnType("ntext");
            builder.Property(x => x.Image).HasColumnType("varchar(500)");
 

            builder.HasOne<Category>().WithMany().HasForeignKey(fk => fk.Category_Id);
            builder.HasOne<Producer>().WithMany().HasForeignKey(fk => fk.Producer_Id);
            builder.HasOne<Unit>().WithMany().HasForeignKey(fk => fk.Unit_Id);


        }
    }
}
