using diplom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace diplom.Data
{
    public class VibeDateDbContext : DbContext
    {
        public VibeDateDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Products> products { get; set; }
        public DbSet<Category> categories { get; set; }

    }
}
