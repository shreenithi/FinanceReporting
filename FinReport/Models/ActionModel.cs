using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinReport.Models
{
    public class ActionModel
    {
        public string customerId { get; set; }
        public string status { get; set; }
        public string actionDate { get; set; }
        public string actionType { get; set; }
    }
}