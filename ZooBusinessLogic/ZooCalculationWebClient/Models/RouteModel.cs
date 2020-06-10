using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooCalculationWebClient.Models
{
    public class RouteModel
    {
        public string RouteName { get; set; }
        public DateTime StartRoute { get; set; }
        public decimal Cost { get; set; }
    }
}
