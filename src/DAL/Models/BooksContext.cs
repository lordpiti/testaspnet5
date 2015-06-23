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
            var connString = Config["Data:DefaultConnection:ConnectionString"];
            optionsBuilder.UseSqlServer(connString);
        }

        public BooksContext()
        {
            var config = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Config = config;
        }

    }
}
