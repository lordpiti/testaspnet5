using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;


namespace DAL.Models
{
    public class BooksContext : DbContext
    {

        public DbSet<Libro> Libros { get; set; }

        public IConfiguration Config { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //When applying migrations to the azure db, use the right connection string only, comment the if #debug stuff

            var connString = Config["Data:DefaultConnection:ConnectionString"];
            #if !DEBUG
                connString = Config["Data:AzureConnection:ConnectionString"];                
            #endif
            //var connString = Config["Data:AzureConnection:ConnectionString"];
            optionsBuilder.UseSqlServer(connString);
        }

        public BooksContext()
        {
            var config = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Config = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ForSqlServer().UseIdentity();
        }

    }
}
