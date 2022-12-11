using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DoAn5.DataContext.EF
{
    public class DoAn5DbContextFactory : IDesignTimeDbContextFactory<DoAn5DbContext>
    {
        public DoAn5DbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DoAn5Db");

            var optionsBuilder = new DbContextOptionsBuilder<DoAn5DbContext>();
            optionsBuilder.UseSqlServer(connectionString);
              
            return new DoAn5DbContext(optionsBuilder.Options);
        }
    }
}
