using ObjectRentalData.Models;
using ObjectRentalData.Repositories;
using System;

namespace ObjectRentalData.Services;

public class RentalService
{
    private readonly IRentalObjectRepository rentalObjectRepository;
    private readonly IRentalRepository rentalRepository;
    private readonly IBorrowerRepository borrowerRepository;
    private readonly IReservationRepository reservationRepository;

    public RentalService(
        IRentalObjectRepository rentalObjectRepository,
        IRentalRepository rentalRepository,
        IBorrowerRepository borrowerRepository,
        IReservationRepository reservationRepository)
    {
        this.rentalObjectRepository = rentalObjectRepository;
        this.rentalRepository = rentalRepository;
        this.borrowerRepository = borrowerRepository;
        this.reservationRepository = reservationRepository;
    }

    public void RegisterRental(int rentalObjectId, int borrowerId)
    {
        var item = rentalObjectRepository.Get(rentalObjectId);
        var borrower = borrowerRepository.Get(borrowerId);

        if (item != null && borrower != null)
        {
            item.Status = Status.Rented;

            rentalRepository.Add(new Rental
            {
                RentalObject = item,
                Borrower = borrower,
                From = DateTime.Now,
                Till = null
            });
        }
    }

    public void ReturnItem(int rentalObjectId)
    {
        rentalRepository.SetReturnDate(rentalObjectId, DateTime.Now);

        if (reservationRepository.IsReserved(rentalObjectId))
            rentalObjectRepository.SetStatus(rentalObjectId, Status.Reserved);
        else
            rentalObjectRepository.SetStatus(rentalObjectId, Status.Available);
    }

    public string? GetCurrentBorrowerName(int rentalObjectId)
    {
        var rental = rentalRepository.GetOpenRentalForRentalObject(rentalObjectId);
        return rental != null
            ? $"{rental.Borrower.FirstName} {rental.Borrower.LastName}"
            : null;
    }

    public IEnumerable<Rental> GetOpenRentalsByBorrower(int borrowerId)
    {
        return rentalRepository.GetOpenRentalsByBorrower(borrowerId);
    }
}
