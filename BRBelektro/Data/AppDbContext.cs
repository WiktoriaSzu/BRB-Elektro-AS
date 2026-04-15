using Microsoft.EntityFrameworkCore;
using BRBelektro.Models;

namespace BRBelektro.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Inquiry> Inquiries { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}