using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinReport.Models
{
    public class modelForFinCustomerData
    {
        public string customerId { get; set; }
        public string firstName {get; set;}
        public string lastName { get; set;}
        public string consumerStatus { get; set;} 
        public string streetName { get; set;}
        public string zipCode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string dateOfBirth { get; set; }
        public string dueAmount { get; set; }        
        public string paymentDate { get; set; }
        
    }
}