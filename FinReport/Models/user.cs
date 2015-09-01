using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinReport.tools;
using System.Web.Mvc;
using FinReport.Controllers;

namespace FinReport.Models
{
    public class user
    {
        LoginController control = new LoginController();
        public void populatingDb()
        {
            DatabaseAccessForReporting ReportingObj = new DatabaseAccessForReporting();
           ReportingObj.populatingCustomerDb();
        }
        public List<SearchModel> reporting(string custName, string Zip)
        {
            List<SearchModel> customerData = new List<SearchModel>();
            DatabaseAccessForReporting ReportingObj = new DatabaseAccessForReporting();
            customerData=ReportingObj.Search(custName,Zip);
            return customerData;
        }

        public List<SearchModel> Delinquent()
        {
            List<SearchModel> customerData = new List<SearchModel>();
            DatabaseAccessForReporting ReportingObj = new DatabaseAccessForReporting();
            customerData = ReportingObj.SearchByDelinquent();
            return customerData;
        }
        public List<ActionModel> treatmentHistory(string number)
        {
            List<ActionModel> customerData = new List<ActionModel>();
            DatabaseAccessForReporting ReportingObj = new DatabaseAccessForReporting();
            customerData = ReportingObj.searchTreatment(number);       
            return (customerData);
        }
        public List<SearchModel> Disconnected()
        {
            List<SearchModel> customerData = new List<SearchModel>();
            DatabaseAccessForReporting ReportingObj = new DatabaseAccessForReporting();
            customerData = ReportingObj.SearchByDisconnected();
            return customerData;
        }

        public List<String> StagewiseReport()
        {
            List<string> StagewiseList = new List<string>();
            DatabaseAccessForReporting ReportingObj = new DatabaseAccessForReporting();
            StagewiseList = ReportingObj.StageWiseReport();
            return StagewiseList;

        }


    }
}