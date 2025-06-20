using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectRentalData.Models;


namespace ObjectRentalData.Repositories;
public interface IRentalRepository
{
    Rental? Get(int rentalId);
    IEnumerable<Rental> GetAll();
    void Add(Rental newRental);
    void SetReturnDate(int rentalObjectId, DateTime now);
    Rental? GetOpenRentalForRentalObject(int rentalObjectId);
    IEnumerable<Rental> GetOpenRentalsByBorrower(int borrowerId);
}
