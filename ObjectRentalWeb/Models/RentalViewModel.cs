using Microsoft.AspNetCore.Mvc.Rendering;

namespace ObjectRentalWeb.Models;


public class RentViewModel
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public int SelectedBorrowerId { get; set; }
    public IEnumerable<SelectListItem> Borrowers { get; set; }
}
