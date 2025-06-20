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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ObjectRentalDbContext>();
    context.Database.EnsureCreated();

    // Vereist: minimaal 1 Operating System (voor Device FK)
    if (!context.Set<Operatingsystem>().Any())
    {
        context.Set<Operatingsystem>().Add(new Operatingsystem { Name = "Default OS" });
        context.SaveChanges();
    }

    var defaultOs = context.Set<Operatingsystem>().First();

    // Vereist: minimaal 1 Book (subtype van RentalObject)
    if (!context.Books.Any())
    {
        context.Books.Add(new Book
        {
            Title = "Testboek",
            Name = "Testboek",
            Author = "Auteur X",
            ISBN = "0000000000",
            Price = 1.0m
        });
    }

    // Vereist: minimaal 1 Device (ook subtype RentalObject)
    if (!context.Devices.Any())
    {
        context.Devices.Add(new Device
        {
            Brand = "MerkX",
            Type = "TestDevice",
            Name = "Test Device",
            Price = 2.0m,
            Operatingsystem = defaultOs
        });
    }

    // Minimaal 1 Borrower
    if (!context.Borrowers.Any())
    {
        context.Borrowers.Add(new Borrower
        {
            FirstName = "Test",
            LastName = "Gebruiker",
            Email = "test@example.com",
            PhoneNumber = "0123456789",
            Adress = "Straat 1, 1000 Brussel"
        });
    }

    context.SaveChanges();
}






app.Run();
