using Microsoft.EntityFrameworkCore;
using MyFirstApi.API.Models.Domain;
namespace MyFirstApi.API.Data
{
    public class MyFirstApiDbContext : DbContext
    {
        //skrót to ctor double tab
        public MyFirstApiDbContext(DbContextOptions<MyFirstApiDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        //skrót to prop double tab
        public DbSet<Difficulty> Difficulties{ get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed the data for difficulties
            //easy, medium, hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("c901f38e-b052-408a-9546-5ce1d7853556"), //Guid.NewGuid(), tu chcesz mieć stały Guid dlatego tego nie używasz. Chcesz go znać. Zamiast tego view->other windows->C# interactive i wpisujesz Guid.NewGuid()
                    Name = "Easy",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("61e5e03d-4a5f-479d-9378-77da1e3eca24"),
                    Name = "Normal"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("52c40148-d621-41e2-9bde-640ad48363b9"),
                    Name = "Hard"
                },
            };

            //seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for Regions
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Warsaw",
                    Code = "WRS",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Lublin",
                    Code = "LUB",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Rzeszow",
                    Code = "RZS",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Janow",
                    Code = "LJA",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Zamosc",
                    Code = "LZM",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Gdansk",
                    Code = "GDS",
                    RegionImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
