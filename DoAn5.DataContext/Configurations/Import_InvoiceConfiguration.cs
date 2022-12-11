using DoAn5.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Configurations
{
    public class Import_InvoiceConfiguration : IEntityTypeConfiguration<Import_Invoice>
    {
        public void Configure(EntityTypeBuilder<Import_Invoice> builder)
        {
            builder.ToTable("Import_Invoices");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Import_Date).HasDefaultValueSql("getdate()");
            builder.Property(x=> x.Total).IsRequired();

            builder.HasOne<User>().WithMany().HasForeignKey(fk => fk.User_Id);
            builder.HasOne<Provider>().WithMany().HasForeignKey(fk => fk.Provider_Id);

        }
    }
}
