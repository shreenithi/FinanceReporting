using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinReport.Models
{
    public class modelForDelinquentCustomertable
    {
        public string accountNumber
        {
            get; set;
        }
        public string daysElapsed { get; set; }
        public string dueAmount { get; set; }
        public string status { get; set; }
        public string flag { get; set; }
        public string P2PDays { get; set; }
    }
}