using ObjectRentalData.Models;

namespace ObjectRentalWeb.Models;

public class BorrowerDetailViewModel
{
    public Borrower Borrower { get; set; }
    public IEnumerable<Rental> OpenRentals { get; set; }
    public IEnumerable<Reservation> Reservations { get; set; }
}
