using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectRentalData.Models;
using ObjectRentalData.Services;
using ObjectRentalServices;
using ObjectRentalWeb.Models;

namespace ObjectRentalWeb.Controllers;

public class RentalObjectController : Controller
{
    private readonly RentalObjectService rentalObjectService;
    private readonly BorrowerService borrowerService;
    private readonly RentalService rentalService;
    private readonly ReservationService reservationService;

    public RentalObjectController
        (
        RentalObjectService rentalObjectService,
        BorrowerService borrowerService,
        RentalService rentalService,
        ReservationService reservationService)
    {
        this.rentalObjectService = rentalObjectService;
        this.borrowerService = borrowerService;
        this.rentalService = rentalService;
        this.reservationService = reservationService;
    }

    public IActionResult Index()
    {
        var objects = rentalObjectService.GetAllRentalObjectDetails();
        var objectList = objects.Select(obj => new RentalObjectDetailViewModel
        {
            Id = obj.Id,
            Name = obj.Name,
            Year = obj.Year,
            Price = obj.Price,
            Status = obj.Status,
            Type = rentalObjectService.GetRentalObjectType(obj.Id),
            Details = rentalObjectService.GetDetails(obj.Id)
        });

        return View(objectList);
    }



    public IActionResult Detail(int id)
    {
        RentalObjectDetailViewModel? model = null;
        RentalObject? obj = rentalObjectService.GetRentalObject(id);

        if (obj != null)
        {
            model = new RentalObjectDetailViewModel
            {
                Id = id,
                Name = obj.Name,
                Year = obj.Year,
                Price = obj.Price,
                Status = obj.Status,
                ImageUrl = obj.ImageUrl,
                Details = rentalObjectService.GetDetails(id),
                Type = rentalObjectService.GetRentalObjectType(id),
                CurrentBorrower = rentalService.GetCurrentBorrowerName(id),
                WaitingList = reservationService.GetReservationList(id)
            };
        }
        else
        {
            ViewBag.ErrorMessage = $"No information found for item with ID {id}";
        }

        return View(model);
    }

    public IActionResult Rent(int id)
    {
        var item = rentalObjectService.GetRentalObject(id);
        if (item != null)
        {
            var borrowerSelectList = new List<SelectListItem>(); // (3) 
            foreach (var borrower in borrowerService.GetAllBorrowers()) // (4) 
                borrowerSelectList.Add(
                    new SelectListItem
                    {
                        Text = borrower.FirstName + " " + borrower.LastName,
                        Value = borrower.Id.ToString()
                    });
            var model = new RentViewModel
            {
                ItemId = id,
                ImageUrl = item.ImageUrl!,
                Name = item.Name,
                Borrowers = borrowerSelectList
            };

            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult RegisterRental(int itemId, int selectedBorrowerId)
    {
        rentalService.RegisterRental(itemId, selectedBorrowerId);
        return RedirectToAction(nameof(Detail), new { id = itemId });
    }

    [HttpPost]
    public IActionResult ReturnItem(int id)
    {
        rentalService.ReturnItem(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Reserve(int id)
    {
        var item = rentalObjectService.GetRentalObject(id);
        if (item != null)
        {
            var reservations = reservationService.GetReservationsForRentalObject(id);
            var borrowerSelectList = new List<SelectListItem>();
            foreach (var borowwer in borrowerService.GetAllBorrowers())
                borrowerSelectList.Add(
                    new SelectListItem
                    {
                        Text = borowwer.FirstName + " " + borowwer.LastName,
                        Value = borowwer.Id.ToString()
                    });

            var model = new ReserveViewModel
            {
                ItemId = id,
                ImageUrl = item.ImageUrl,
                Name = item.Name,
                Reservations = reservations,
                Borrowers = borrowerSelectList
            };

            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult ReserveItem(int itemId, int selectedBorrowerId)
    {
        if (reservationService.Exists(itemId, selectedBorrowerId))
        {
            TempData["Error"] = "This borrower already reserved this item.";
            return RedirectToAction("Reserve", new { id = itemId });
        }

        reservationService.ReserveItem(itemId, selectedBorrowerId);
        return RedirectToAction("Detail", new { id = itemId });
    }


    [HttpPost]
    public IActionResult ProcessReservation(int itemId, int borrowerId)
    {
        reservationService.RemoveReservation(itemId, borrowerId);
        rentalService.RegisterRental(itemId, borrowerId);
        return RedirectToAction(nameof(Detail), new { id = itemId });
    }
    [HttpPost]
    public IActionResult RegisterLoan(int itemId, int SelectedBorrowerId)
    {
        rentalService.RegisterRental(itemId, SelectedBorrowerId);

        return RedirectToAction("Detail", new { id = itemId });
    }
    public IActionResult ClaimReservation(int itemId, int borrowerId)
    {
        // Remove the oldest reservation for this item
        reservationService.RemoveReservation(itemId, borrowerId);

        // Register the borrowing
        rentalService.RegisterRental(itemId, borrowerId);

        // Redirect back to the detail page
        return RedirectToAction("Detail", new { id = itemId });
    }

    [HttpPost]
    public IActionResult Reserve(int itemId, int borrowerId)
    {
        if (reservationService.Exists(itemId, borrowerId))
        {
            TempData["Error"] = "You have already reserved this item.";
            return RedirectToAction("Detail", new { id = itemId });
        }

        reservationService.ReserveItem(itemId, borrowerId);
        return RedirectToAction("Detail", new { id = itemId });
    }


}
