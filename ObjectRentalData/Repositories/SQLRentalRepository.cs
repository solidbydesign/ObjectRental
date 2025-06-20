using Microsoft.EntityFrameworkCore;
using ObjectRentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRentalData.Repositories;
public class SQLRentalRepository : IRentalRepository
{
    private readonly ObjectRentalDbContext context;

    public SQLRentalRepository(ObjectRentalDbContext context)
    {
        this.context = context;
    }

    public void Add(Rental newRental)
    {
        context.Rentals.Add(newRental);
        context.SaveChanges();
    }

    public IEnumerable<Rental> GetAll() => context.Rentals;

    public Rental? Get(int rentalId) => context.Rentals.Find(rentalId);

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

    public Rental? GetOpenRentalForRentalObject(int rentalObjectId)
    {
        return context.Rentals
            .Include(u => u.RentalObject)
            .Include(u => u.Borrower)
            .Where(u => u.Till == null)
            .FirstOrDefault(u => u.RentalObject.Id == rentalObjectId);
    }

    public IEnumerable<Rental> GetOpenRentalsByBorrower(int borrowerId)
    {
        return context.Rentals
            .Include(u => u.Borrower)
            .Include(u => u.RentalObject)
            .Where(u => u.Borrower.Id == borrowerId && u.Till == null)
            .OrderBy(u => u.From);
    }

   

   
}
