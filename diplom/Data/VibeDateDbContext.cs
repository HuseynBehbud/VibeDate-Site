using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace diplom.Data
{
    public class VibeDateDbContext : DbContext
    {
        public VibeDateDbContext(DbContextOptions options) : base(options)
        {
        }

        protected VibeDateDbContext()
        {
        }

       
    }
}
