using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyIdentityServer.Data;
using MyIdentityServer.Models;
using Duende.IdentityServer;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on custom HTTP and HTTPS ports
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8085); // HTTP
    options.ListenAnyIP(8441, listenOptions =>
    {
        listenOptions.UseHttps("./https-aspnetcore.pfx", "password");
    }); // HTTPS
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddIdentityServer()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
            sql => sql.MigrationsAssembly(migrationsAssembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
            sql => sql.MigrationsAssembly(migrationsAssembly));
    })
    .AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
