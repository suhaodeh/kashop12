using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KASHOP12.DAL.Data
{
 public   class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _ihttpContextAccessor;

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> categoryTranslations { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor ihttpContextAccessor)
    : base(options)
        {
            _ihttpContextAccessor = ihttpContextAccessor;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");

            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
         
    }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseModel>();
            var currentuserId = _ihttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach(var entityentry in entries)
            {
                if(entityentry.State== EntityState.Added)
                {
                    entityentry.Property(x => x.CreatedBy).CurrentValue = currentuserId;
                    entityentry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;

                }else if (entityentry.State == EntityState.Modified)
                {
                    entityentry.Property(x => x.UpdatedBy).CurrentValue = currentuserId;
                    entityentry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
    }
}
