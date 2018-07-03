using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SecurityWithIOT.API.Data;

namespace SecurityWithIOT.API
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            //throw new System.NotImplementedException();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DataContext>();

            var connectionString = configuration.GetConnectionString("MSSQLConnection");

            builder.UseSqlServer(connectionString);

            return new DataContext(builder.Options);
        }
       
    }
}