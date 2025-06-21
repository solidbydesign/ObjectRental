using Microsoft.EntityFrameworkCore;
using ObjectRentalData.Models;
using ObjectRentalData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRentalData.Repositories;
public class SQLRentalObjectRepository : IRentalObjectRepository
{
    private readonly ObjectRentalDbContext context;

    public SQLRentalObjectRepository(ObjectRentalDbContext context)
    {
        this.context = context;
    }

    public RentalObject? Get(int id) => context.RentalObjects.Find(id);

    public IEnumerable<RentalObject> GetAll() => context.RentalObjects.AsNoTracking();

    public Book? GetBook(int id) => context.Books.Find(id);

    public Device? GetDevice(int id) => context.Devices
        .Include(d => d.Operatingsystem)
        .FirstOrDefault(x => x.Id == id);

    public void SetReturnDate(int rentalObjectId, DateTime dateTime)
    {
        var rental = context.Rentals
            .Where(u => u.Till == null)
            .Where(u => u.RentalObject.Id == rentalObjectId)
            .FirstOrDefault();
        if (rental != null)
        {
            rental.Till = dateTime;
            context.SaveChanges();
        }
    }

    public void SetStatus(int rentalObjectId, Status status)
    {
        var item = context.RentalObjects.Find(rentalObjectId);
        if (item != null)
        {
            item.Status = status;
            context.SaveChanges();
        }
    }
    public IEnumerable<RentalObject> GetAllIncludingDetails()
    {
        return context.RentalObjects
            .Include(r => (r as Device)!.Operatingsystem)  // Alleen voor Devices
            .ToList();
    }

}