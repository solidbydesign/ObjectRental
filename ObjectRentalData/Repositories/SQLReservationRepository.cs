using Microsoft.EntityFrameworkCore;
using ObjectRentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRentalData.Repositories;
public class SQLReservationRepository : IReservationRepository
{
    private readonly ObjectRentalDbContext context;

    public SQLReservationRepository(ObjectRentalDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<Reservation> GetReservationsForRentalObject(int rentalObjectId)
    {
        return context.Reservations
            .Include(r => r.Borrower)
            .Where(r => r.RentalObject.Id == rentalObjectId)
            .OrderBy(r => r.ReservedOn);
    }

    public void Add(Reservation reservation)
    {
        context.Reservations.Add(reservation);
        context.SaveChanges();
    }

    public bool IsReserved(int rentalObjectId)
    {
        var reservation = context.Reservations
            .Include(r => r.RentalObject)
            .FirstOrDefault(r => r.RentalObject.Id == rentalObjectId);

        return reservation != null;
    }

    public void RemoveReservation(int itemId, int borrowerId)
    {
        var reservation = context.Reservations
            .Include(r => r.RentalObject)
            .Include(r => r.Borrower)
            .FirstOrDefault(r => r.RentalObject.Id == itemId && r.Borrower.Id == borrowerId);
        if (reservation != null)
        {
            context.Remove(reservation);
            context.SaveChanges();
        }
    }

    public IEnumerable<Reservation> GetReservationsByBorrower(int borrowerId)
    {
        return context.Reservations
            .Include(r => r.Borrower)
            .Include(r => r.RentalObject)
            .Where(r => r.Borrower.Id == borrowerId)
            .OrderBy(r => r.ReservedOn);
    }
}
