using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyFirstApi.API.Data
{
    public class MyFirstApiAuthDbContext : IdentityDbContext
    {
        public MyFirstApiAuthDbContext(DbContextOptions<MyFirstApiAuthDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerId = "9877735e-c689-429f-bdf0-9db385315b0e";
            var writerId = "c7ccf26d-e5a4-452b-90ca-0b723de0c272";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=readerId,
                    ConcurrencyStamp= readerId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                },
                new IdentityRole
                {
                    Id=writerId,
                    ConcurrencyStamp= writerId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
