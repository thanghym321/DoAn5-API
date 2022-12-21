using DoAn5.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DoAn5.DataContext.Enums;

namespace DoAn5.DataContext.Configurations
{
    public class Export_InvoiceConfiguration : IEntityTypeConfiguration<Export_Invoice>
    {
        public void Configure(EntityTypeBuilder<Export_Invoice> builder)
        {
            builder.ToTable("Export_Invoices");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Export_Date).HasDefaultValueSql("getdate()");
            builder.HasOne<Customer>().WithMany().HasForeignKey(fk => fk.Customer_Id);
            builder.Property(x => x.Status).HasDefaultValue(StatusEI.Ordered);


        }
    }
}
