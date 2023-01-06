using DoAn5.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Configurations
{
    public class Export_Invoice_DetailConfiguration : IEntityTypeConfiguration<Export_Invoice_Detail>
    {
        public void Configure(EntityTypeBuilder<Export_Invoice_Detail> builder)
        {
            builder.ToTable("Export_Invoice_Details");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Quantity).IsRequired();
            builder.Property(x => x.Price).IsRequired();

            builder.HasOne<Export_Invoice>().WithMany().HasForeignKey(fk => fk.Export_Invoice_Id);
            builder.HasOne<Product>().WithMany().HasForeignKey(fk => fk.Product_Id);

        }
    }
}
