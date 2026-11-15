using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace MyIdentityServer.Data;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        using IServiceScope scope = services.CreateScope();
        ApplicationDbContext applicationDatabase = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        ConfigurationDbContext configurationDatabase = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        await applicationDatabase.Database.MigrateAsync();
        await configurationDatabase.Database.MigrateAsync();

        if (!await configurationDatabase.Clients.AnyAsync())
        {
            configurationDatabase.Clients.AddRange(Config.Clients.Select(client => client.ToEntity()));
        }

        if (!await configurationDatabase.IdentityResources.AnyAsync())
        {
            configurationDatabase.IdentityResources.AddRange(Config.IdentityResources.Select(resource => resource.ToEntity()));
        }

        if (!await configurationDatabase.ApiScopes.AnyAsync())
        {
            configurationDatabase.ApiScopes.AddRange(Config.ApiScopes.Select(scope => scope.ToEntity()));
        }

        await configurationDatabase.SaveChangesAsync();
    }
}
