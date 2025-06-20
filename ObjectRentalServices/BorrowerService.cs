using ObjectRentalData.Models;
using ObjectRentalData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRentalServices;


public class BorrowerService
{
    private readonly IBorrowerRepository borrowerRepository;

    public BorrowerService(IBorrowerRepository borrowerRepository)
    {
        this.borrowerRepository = borrowerRepository;
    }

    public IEnumerable<Borrower> GetAllBorrowers()
    {
        return borrowerRepository.GetAll();
    }

    public Borrower? GetBorrower(int id)
    {
        return borrowerRepository.Get(id);
    }
}