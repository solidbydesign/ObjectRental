using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ObjectRentalData.Models;
using ObjectRentalData.Repositories;
using ObjectRentalData.Services;
using ObjectRentalServices;
using ObjectRentalWeb.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ObjectRentalConnection")
    ?? throw new InvalidOperationException("Connection string 'ObjectRentalConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ObjectRentalDbContext>(options =>
    options.UseSqlServer(connectionString, x => x.MigrationsAssembly("ObjectRentalData")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<BorrowerService>();
builder.Services.AddTransient<IBorrowerRepository, SQLBorrowerRepository>();
builder.Services.AddTransient<IRentalObjectRepository, SQLRentalObjectRepository>();
builder.Services.AddTransient<RentalObjectService>();
builder.Services.AddTransient<IRentalRepository, SQLRentalRepository>();
builder.Services.AddTransient<RentalService>();
builder.Services.AddTransient<ReservationService>();
builder.Services.AddTransient<IReservationRepository, SQLReservationRepository>();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
