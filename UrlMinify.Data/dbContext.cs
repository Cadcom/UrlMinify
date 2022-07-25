using Microsoft.EntityFrameworkCore;
using UrlMinify.Shared.Entities;

namespace UrlMinify.Data
{
    public class dbContext: DbContext
    {
        public DbSet<UrlItem> Urls { get; set; }

        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {

        }
    }
}