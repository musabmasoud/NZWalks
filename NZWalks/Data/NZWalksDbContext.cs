using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Diffculty> Diffculties { get; set;}
        public DbSet<Region> Regions { get; set;}
        public DbSet<Walk> Walks { get; set; }
    }
}
