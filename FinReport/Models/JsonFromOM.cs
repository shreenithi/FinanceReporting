using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinReport.tools;
namespace FinReport.Models
{



    public class ProfilePull
    {
        public Orderhistory[] orderhistory { get; set; }
        public string lineofbusiness { get; set; }
        public Existingsnp existingsnp { get; set; }
        public Contractdetail[] contractdetails { get; set; }
        public Customerdetails customerdetails { get; set; }
        public static ProfilePull GetProfile(string customerId)
        {
            return ProfilePullHelper.PullProfile(customerId);
        }
    }

    public class Existingsnp
    {
        public string services { get; set; }
        public string products { get; set; }
    }

    public class Customerdetails
    {
        public long contactnumber { get; set; }
        public string lname { get; set; }
        public string customerstatus { get; set; }
        public int customerid { get; set; }
        public string email { get; set; }
        public Connectionaddress connectionaddress { get; set; }
        public string dateofbirth { get; set; }
        public Billingaddress billingaddress { get; set; }
        public string fname { get; set; }
    }

    public class Connectionaddress
    {
        public int stateid { get; set; }
        public int zipcode { get; set; }
        public string state { get; set; }
        public string streetname { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }

    public class Billingaddress
    {
        public int stateid { get; set; }
        public int zipcode { get; set; }
        public string state { get; set; }
        public string streetname { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }

    public class Orderhistory
    {
        public Service[] services { get; set; }
        public string orderstatus { get; set; }
        public string dateoforder { get; set; }
        public string duedate { get; set; }
        public int orderid { get; set; }
        public string[] products { get; set; }
    }

    public class Service
    {
        public string servicename { get; set; }
        public Quantity quantity { get; set; }
        public string servicecode { get; set; }
    }

    public class Quantity
    {
        public int max { get; set; }
        public int current { get; set; }
    }

    public class Contractdetail
    {
        public string modeltype { get; set; }
        public int max { get; set; }
        public string classofservice { get; set; }
        public int contractid { get; set; }
        public int current { get; set; }
        public string fromdate { get; set; }
        public int discountpercentage { get; set; }
        public string todate { get; set; }
    }



}