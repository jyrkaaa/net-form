using App.BLL;
using App.BLL.Contracts;
using App.DAL;
using App.DAL.Contracts;
using App.DAL.DataSeeding;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IAppUOW, AppUOW>();
builder.Services.AddScoped<ISessionService, SessionService>();

// Add services to the container.
//builder.Services.AddRazorPages();


builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Add session services
builder.Services.AddDistributedMemoryCache(); // Required for storing session in memory
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5); // Optional: session expiration
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Needed for GDPR compliance
});

var app = builder.Build();
SetupAppData(app, app.Environment, app.Configuration);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.MapStaticAssets();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

// app.MapStaticAssets();
// app.MapRazorPages()
//     .WithStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Example: use UserController.Start as homepage



app.Run();

static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
{
    using var serviceScope = ((IApplicationBuilder)app).ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope();
    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<IApplicationBuilder>>();

    using var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

    WaitDbConnection(context, logger);

    if (context.Database.ProviderName!.Contains("InMemory"))
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return;
    }

    if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
    {
        logger.LogWarning("DropDatabase");
        AppDataInit.DeleteDatabase(context);
    }

    if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
    {
        logger.LogInformation("MigrateDatabase");
        AppDataInit.MigrateDatabase(context);
    }
    

    if (configuration.GetValue<bool>("DataInitialization:SeedData"))
    {
        logger.LogInformation("SeedData");
        AppDataInit.SeedAppData(context);
        AppDataInit.SeedSectors(context);

    }
}

static void WaitDbConnection(AppDbContext ctx, ILogger<IApplicationBuilder> logger)
{

    while (true)
    {
        try
        {
            ctx.Database.OpenConnection();
            ctx.Database.CloseConnection();
            return;
        }
        catch (SqliteException e)
        {
            logger.LogWarning("Checked sqlite db connection. Got: {}", e.Message);

            if (e.Message.Contains("does not exist"))
            {
                logger.LogWarning("Applying migration, probably db is not there (but server is)");
                return;
            }

            logger.LogWarning("Waiting for db connection. Sleep 1 sec");
            System.Threading.Thread.Sleep(1000);
        }
    }
}

