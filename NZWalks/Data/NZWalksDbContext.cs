using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Diffculty> Diffculties { get; set;}
        public DbSet<Region> Regions { get; set;}
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Seed data for Difficulties
            //Easy,Medium , Hard
            var difficulties = new List<Diffculty>()
            {
                new Diffculty()
                {
                    Id=Guid.Parse("78ad2ed7-695e-458a-93a4-acf616e7a9e6"),
                    Name="Easy"
                },
                new Diffculty()
                {
                    Id=Guid.Parse("d6cb3e68-ded2-47fe-beb2-116df7c2f63b"),
                    Name="Medium"
                },
                new Diffculty()
                {
                    Id=Guid.Parse("d9a355ae-e2e8-4a28-bd0c-55361063cf7a"),
                    Name="Hard"
                },
            };
            modelBuilder.Entity<Diffculty>().HasData(difficulties);
            var regions = new List<Region>()
           {
               new Region()
               {
                   Id=Guid.Parse("3ef37571-b117-46c3-a336-70423107e3e2"),
                   Name ="Acukland",
                   Code="AKL",
                   RegionImageUrl="Photo by Thirdman from Pexels: https://www.pexels.com/photo/mixing-acrylic-paint-with-a-chisel-6732552/"
               },
               new Region()
               {
                   Id=Guid.Parse("8180fd18-496e-4cde-a7bd-68e072cd805d"),
                   Name ="NorthLand",
                   Code="NTL",
                   RegionImageUrl="Photo by Nurgül  Kelebek  from Pexels: https://www.pexels.com/photo/white-ceramic-mug-on-white-ceramic-saucer-13708881/"
               },
               new Region()
               {
                   Id=Guid.Parse("9e0d1edc-9a01-4c11-9454-b571a52f2280"),
                   Name ="Bay Of Plenty",
                   Code="BOP",
                   RegionImageUrl="Photo by igovar igovar from Pexels: https://www.pexels.com/photo/grayscale-photo-of-man-in-polo-shirt-with-birds-5815451/"
               },
               new Region()
               {
                   Id=Guid.Parse("4bc0d799-646e-4862-94aa-fa546ae91661"),
                   Name ="SouthLand",
                   Code="STL",
                   RegionImageUrl="Photo by Finn Semmer from Pexels: https://www.pexels.com/photo/lonely-20590852/"
               }
           };
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
