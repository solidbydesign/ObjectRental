using ObjectRentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ObjectRentalData.Repositories;
public interface IRentalObjectRepository
{
    RentalObject? Get(int id);
    IEnumerable<RentalObject> GetAll();
    Book? GetBook(int id);
    Device? GetDevice(int id);
    IEnumerable<RentalObject> GetAllIncludingDetails();

    void SetStatus(int rentalObjectId, Status status);
}
