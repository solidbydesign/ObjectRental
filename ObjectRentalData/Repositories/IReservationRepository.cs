using ObjectRentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRentalData.Repositories;

public interface IReservationRepository
{
    void Add(Reservation reservation);
    IEnumerable<Reservation> GetReservationsForRentalObject(int rentalObjectId);
    bool IsReserved(int rentalObjectId);
    void RemoveReservation(int itemId, int borrowerId);
    IEnumerable<Reservation> GetReservationsByBorrower(int borrowerId);
}
