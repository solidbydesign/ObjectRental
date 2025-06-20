using ObjectRentalData.Models;
using ObjectRentalData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectRentalData.Services;

public class ReservationService
{
    private readonly IReservationRepository reservationRepository;
    private readonly IRentalObjectRepository rentalObjectRepository;
    private readonly IBorrowerRepository borrowerRepository;

    public ReservationService(
        IReservationRepository reservationRepository,
        IRentalObjectRepository rentalObjectRepository,
        IBorrowerRepository borrowerRepository)
    {
        this.reservationRepository = reservationRepository;
        this.rentalObjectRepository = rentalObjectRepository;
        this.borrowerRepository = borrowerRepository;
    }

    public IEnumerable<Reservation> GetReservationsForRentalObject(int id)
    {
        return reservationRepository.GetReservationsForRentalObject(id);
    }

    public void ReserveItem(int itemId, int borrowerId)
    {
        var item = rentalObjectRepository.Get(itemId);
        var borrower = borrowerRepository.Get(borrowerId);

        if (item != null && borrower != null)
        {
            reservationRepository.Add(new Reservation
            {
                RentalObject = item,
                Borrower = borrower,
                ReservedOn = DateTime.Now
            });
        }
    }
    public bool Exists(int itemId, int borrowerId)
    {
        return reservationRepository
            .GetReservationsForRentalObject(itemId)
            .Any(r => r.Borrower.Id == borrowerId);
    }

    public IEnumerable<Borrower> GetReservationList(int rentalObjectId)
    {
        return reservationRepository
            .GetReservationsForRentalObject(rentalObjectId)?
            .Select(r => r.Borrower) ?? Enumerable.Empty<Borrower>();
    }

    public void RemoveReservation(int itemId, int borrowerId)
    {
        reservationRepository.RemoveReservation(itemId, borrowerId);
    }

    public IEnumerable<Reservation> GetReservationsByBorrower(int borrowerId)
    {
        return reservationRepository.GetReservationsByBorrower(borrowerId);
    }
}
