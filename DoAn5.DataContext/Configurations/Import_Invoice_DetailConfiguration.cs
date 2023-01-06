using DoAn5.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Configurations
{
    public class Import_Invoice_DetailConfiguration : IEntityTypeConfiguration<Import_Invoice_Detail>
    {
        public void Configure(EntityTypeBuilder<Import_Invoice_Detail> builder)
        {
            builder.ToTable("Import_Invoice_Details");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).IsRequired();

            builder.HasOne<Product>().WithMany().HasForeignKey(fk => fk.Product_Id);
            builder.HasOne<Import_Invoice>().WithMany().HasForeignKey(fk => fk.Import_Invoice_Id);

        }
    }
}
