using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gym_Management.DB_Context
{
    public class GymAuthContext : IdentityDbContext
    {
        public GymAuthContext(DbContextOptions<GymAuthContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerId = "f7f2eae4-ae5d-499b-8fbc-b7f4506ce60b";
            var writerId = "5cbd9cf3-3895-4f69-ab35-915341301992";

             var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerId,
                    ConcurrencyStamp = readerId,
                     Name = "Reader",
                     NormalizedName = "Reader".ToUpper()
                },
                 new IdentityRole
                 {
                    Id = writerId,
                    ConcurrencyStamp = writerId,
                     Name = "Writer",
                     NormalizedName = "Writer".ToUpper()
                 }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
