using ObjectRentalData.Models;
using ObjectRentalData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ObjectRentalData.Services;

public class RentalObjectService
{
    private readonly IRentalObjectRepository rentalObjectRepository;

    public RentalObjectService(IRentalObjectRepository rentalObjectRepository)
    {
        this.rentalObjectRepository = rentalObjectRepository;
    }

    public IEnumerable<RentalObject> GetAllRentalObjects()
    {
        return rentalObjectRepository.GetAll();
    }

    public string GetRentalObjectType(int id)
    {
        var rentalObject = rentalObjectRepository.Get(id);
        if (rentalObject is Book) return "Book";
        if (rentalObject is Device) return "Device";
        return "Unknown";
    }

    public string GetDetails(int id)
    {
        switch (GetRentalObjectType(id))
        {
            case "Book":
                var book = rentalObjectRepository.GetBook(id);
                return book != null
                    ? $"{book.ISBN} ({book.Author}, {book.NumberOfPages}p.)"
                    : $"No info found for book with id {id}";

            case "Device":
                var device = rentalObjectRepository.GetDevice(id);
                return device != null
                    ? $"{device.Operatingsystem.Name} - {device.Screensize}\""
                    : $"No info found for device with id {id}";

            default:
                return "Unknown object type";
        }
    }

    public RentalObject? GetRentalObject(int id)
    {
        return rentalObjectRepository.Get(id);
    }
}
