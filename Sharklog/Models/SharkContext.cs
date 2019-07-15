using Microsoft.EntityFrameworkCore;

namespace sharklog.Models
{

    public class SharkContext : DbContext
    {


        /*
        dotnet ef migrations add InitialCreate
        dotnet ef database update
        */
        public SharkContext(DbContextOptions<SharkContext> options)
            : base(options)
        { }

        public DbSet<LogModel> Logs { get; set; }
        public DbSet<ApplicationModel> Applications { get; set; }
    }

}