using Microsoft.EntityFrameworkCore;
using ObjectRentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRentalData.Repositories;
public class SQLBorrowerRepository : IBorrowerRepository
{
    private readonly ObjectRentalDbContext context;

    public SQLBorrowerRepository(ObjectRentalDbContext context)
    {
        this.context = context;
    }

    public Borrower? Get(int id) => context.Borrowers.Find(id);

    public IEnumerable<Borrower> GetAll() => context.Borrowers.AsNoTracking();
}

