using ObjectRentalData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRentalData.Models;
public class RentalObjectDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public Status Status { get; set; }
    [DisplayFormat(DataFormatString = "{0:€ #,##0.00}")]
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string Details { get; set; }
    public string Type { get; set; }
    public string? CurrentBorrower { get; set; }
    public IEnumerable<Borrower> WaitingList{ get; set; }
}