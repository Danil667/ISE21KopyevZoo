using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooCalculationWebClient.Models
{
    public class ReportModel
    {
        public int From { get; set; }
        public int To { get; set; }
        public bool SendMail { get; set; }
    }
}
