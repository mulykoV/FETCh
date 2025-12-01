using FETCh.Authorization;
using FETCh.Configurations;
using FetchData.Data;
using FetchData.Interfaces;
using FetchData.Repositories;
using FETChModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// -------------------- CONFIGURATION --------------------
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("sharedsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();

var appConfig = new AppConfiguration();
builder.Configuration.Bind(appConfig);
builder.Services.AddSingleton(appConfig);

// -------------------- DATABASE --------------------
var connectionString = builder.Configuration.GetConnectionString("FETChDbContextConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("❌ Connection string not found.");
}

builder.Services.AddDbContext<FETChDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("FetchData")));

builder.Services.AddScoped<IFETChRepository, FETChSQLServerRepository>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// -------------------- AUTHORIZATION --------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("VerifiedClientOnly", policy => policy.RequireClaim("IsVerifiedClient", "true"));
    options.AddPolicy("PremiumOnly", policy => policy.Requirements.Add(new MinimumWorkingHoursRequirement(100)));
    options.AddPolicy("ForumAccess", policy => policy.Requirements.Add(new ForumAccessRequirement()));
});

builder.Services.AddScoped<IAuthorizationHandler, IsCourseAuthorHandler>();
builder.Services.AddScoped<IAuthorizationHandler, MinimumWorkingHoursHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ForumAccessHandler>();

// -------------------- IDENTITY --------------------
builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromMinutes(5));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<FETChDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

// -------------------- LOCALIZATION SETUP --------------------
// 1. Вказуємо шлях до ресурсів
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// 2. Налаштовуємо мови
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("uk-UA"),
        new CultureInfo("en-US"),
        new CultureInfo("pl-PL")
    };

    options.DefaultRequestCulture = new RequestCulture("uk-UA");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// 3. MVC + DataAnnotations (БЕЗ SharedResource)
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(); // Тут тепер пусто, воно саме знайде файли по папках

builder.Services.AddRazorPages();

// -------------------- RATE LIMITING --------------------
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy<string>("UserPartitioned", context =>
    {
        bool isAuthenticated = context.User?.Identity?.IsAuthenticated ?? false;
        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: isAuthenticated ? "auth" : "anon",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = isAuthenticated ? 100 : 20,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            });
    });
    options.RejectionStatusCode = 429;
});

// ==================== BUILD APP ====================
var app = builder.Build();

// -------------------- PIPELINE --------------------

var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .RequireRateLimiting("UserPartitioned");

app.MapRazorPages();

// -------------------- SEED ROLES --------------------
using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedRolesAsync(scope.ServiceProvider);
}

app.Run();