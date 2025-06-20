using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRentalData.Models;
public class Rental
{
    public int Id { get; set; }
    public RentalObject RentalObject { get; set; }
    public Borrower Borrower { get; set; }
    public DateTime From { get; set; }
    public DateTime? Till { get; set; }
}