using diplom.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace diplom.Data
{
    public class VibeDateDbContext : IdentityDbContext<AppUser>
    {
        public VibeDateDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Products> products { get; set; }
        public DbSet<Category> categories { get; set; }

    }
}
