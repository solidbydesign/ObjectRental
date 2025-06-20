using ObjectRentalData.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ObjectRentalWeb.Models;

public class ReserveViewModel
{
    public int SelectedBorrowerId { get; set; }
    public int ItemId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public IEnumerable<Reservation> Reservations { get; set; }
    public IEnumerable<SelectListItem> Borrowers { get; set; }
}
