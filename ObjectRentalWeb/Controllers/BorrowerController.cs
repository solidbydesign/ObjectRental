using Microsoft.AspNetCore.Mvc;
using ObjectRentalData.Models;
using ObjectRentalData.Services;
using ObjectRentalServices;
using ObjectRentalWeb.Models;



namespace ObjectRentalWeb.Controllers
{
    public class BorrowerController : Controller
    {
        private readonly BorrowerService borrowerService;
        private readonly RentalService rentalService;
        private readonly ReservationService reservationService;

        public BorrowerController(
            BorrowerService borrowerService,
            ReservationService reservationService,
            RentalService rentalService)
        {
            this.borrowerService = borrowerService;
            this.rentalService = rentalService;
            this.reservationService = reservationService;
        }

        public IActionResult Index()
        {
            return View(borrowerService.GetAllBorrowers());
        }

        public IActionResult Detail(int id)
        {
            BorrowerDetailViewModel? viewModel = null;
            var borrower = borrowerService.GetBorrower(id);

            if (borrower == null)
            {
                ViewBag.ErrorMessage = $"No information found for borrower with ID {id}";
            }
            else
            {
                viewModel = new BorrowerDetailViewModel
                {
                    Borrower = borrower!,
                    OpenRentals = rentalService.GetOpenRentalsByBorrower(id),
                    Reservations = reservationService.GetReservationsByBorrower(id)
                };
            }

            return View(viewModel);
        }
    }
}
