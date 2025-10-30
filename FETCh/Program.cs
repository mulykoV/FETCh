using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using FetchData.Data;
using FETChModels.Models;
using FetchData.Interfaces;
using FetchData.Repositories;
using FETCh.Configurations;

var builder = WebApplication.CreateBuilder(args);

// -------------------- CONFIGURATION --------------------
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("sharedsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();

// Ñòðîãî òèï³çîâàíà êîíô³ãóðàö³ÿ
var appConfig = new AppConfiguration();
builder.Configuration.Bind(appConfig);
builder.Services.AddSingleton(appConfig);

// Ï³äêëþ÷åííÿ äî ÁÄ
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<FETChDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("FetchData")));

// DI äëÿ ðåïîçèòîð³þ
builder.Services.AddScoped<IFETChRepository, FETChSQLServerRepository>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// -------------------- IDENTITY --------------------
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(5);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<FETChDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();

// -------------------- MVC --------------------
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// AddPartitionedLimiter with AddPolicy (îñíîâíå)
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy<string>("UserPartitioned", context =>
    {
        bool isAuthenticated = context.User?.Identity?.IsAuthenticated ?? false;

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: isAuthenticated ? "auth" : "anon",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = isAuthenticated ? 100 : 20, // 100 for authenticated, 10 for anonymous
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            });
        
    });
    options.RejectionStatusCode = 429;
});

// -------------------- BUILD APP --------------------
var app = builder.Build();

// -------------------- PIPELINE --------------------
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseRateLimiter(); // Rate Limiting middleware

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .RequireRateLimiting("UserPartitioned")
    .WithStaticAssets();

app.MapRazorPages().WithStaticAssets();

// -------------------- SEED ROLES --------------------
using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedRolesAsync(scope.ServiceProvider);
}

app.Run();
