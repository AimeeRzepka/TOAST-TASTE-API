using Microsoft.EntityFrameworkCore;
using Toast_and_Taste.Models;

namespace Toast_and_Taste.Services
{
    public class TNTContext : DbContext
    {
        public TNTContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<CheeseModel> Cheeses { get; set;}
    }
}
