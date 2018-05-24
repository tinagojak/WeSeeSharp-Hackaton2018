using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Hackaton2018.Models.DbModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Hackaton2018.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name_Surname { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        //public Location UserLocation { get; set; }
        //public int UserLocationId { get; set; }
        public string BloodTypeString { get; set; }
        //public BloodType UserBloodType { get; set; }
        //public int BloodTypeId { get; set; }
        public string QR_Code { get; set; }
        public bool IsBloodDonor { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BloodType> BloodTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<BloodSupply> BloodSupplies { get; set; }
        public DbSet<BloodDonorRecord> BloodDonorRecords { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BloodType>()
                .HasMany(c => c.BloodTypeGives)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("BloodTypeGivesCompatibility");
                    m.MapLeftKey("BloodTypeId");
                    m.MapRightKey("BloodTypeGivesId");
                });

            modelBuilder.Entity<BloodType>()
                .HasMany(c => c.BloodTypeTakes)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("BloodTypeTakesCompatibility");
                    m.MapLeftKey("BloodTypeId");
                    m.MapRightKey("BloodTypeTakesId");
                });

        }
    }
}