using Microsoft.EntityFrameworkCore;

namespace CrmToRecruit
{
    public class MyDbContext : DbContext
    {
        public DbSet<ExcelData> ExcelData { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("MyConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
