using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectRentalData.Models;

namespace ObjectRentalData.Models;
public class Book : RentalObject
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int NumberOfPages { get; set; }
}