using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRentalData.Models;

public class ObjectRentalDbContext : DbContext
{
    public ObjectRentalDbContext() { }
    public ObjectRentalDbContext(DbContextOptions<ObjectRentalDbContext> options) : base(options) { }
    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<RentalObject> RentalObjects { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Device> Devices { get; set; }
}
