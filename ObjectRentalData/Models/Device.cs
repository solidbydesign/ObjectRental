using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Identity.Client.BrokerOptions;

namespace ObjectRentalData.Models; 
public class Device : RentalObject
{
    public Operatingsystem Operatingsystem { get; set; }
    public float Screensize { get; set; }
    public string Brand { get; set; }
    public string Type { get; set; } 
}
