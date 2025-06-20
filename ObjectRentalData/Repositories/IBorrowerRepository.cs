using ObjectRentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRentalData.Repositories;
public interface IBorrowerRepository
{
    Borrower? Get(int id);
    IEnumerable<Borrower> GetAll();
}