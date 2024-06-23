using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyIdentityServer.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Duende.IdentityServer.EntityFramework.Interfaces;
using System.Threading.Tasks;
using Duende.IdentityServer.EntityFramework.Extensions;

namespace MyIdentityServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IPersistedGrantDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Duende.IdentityServer.EntityFramework.Entities.PersistedGrant> PersistedGrants { get; set; }
        public DbSet<Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes> DeviceFlowCodes { get; set; }
        public DbSet<Duende.IdentityServer.EntityFramework.Entities.Key> Keys { get; set; }
        public DbSet<Duende.IdentityServer.EntityFramework.Entities.ServerSideSession> ServerSideSessions { get; set; }
        public DbSet<Duende.IdentityServer.EntityFramework.Entities.PushedAuthorizationRequest> PushedAuthorizationRequests { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigurePersistedGrantContext(new OperationalStoreOptions());
        }
    }
}
