using ObjectRentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRentalData.Models;
public class Reservation
{
    public int Id { get; set; }
    public RentalObject RentalObject { get; set; }
    public Borrower Borrower { get; set; }
    public DateTime ReservedOn { get; set; }
}