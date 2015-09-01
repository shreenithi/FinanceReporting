using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using FinReport.Models;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Net;

namespace FinReport.tools
{
   
    public class DatabaseAccessForReporting
    {
        private static string connString = ConfigurationManager.ConnectionStrings["connectString"].ConnectionString;
        OracleConnection conn = new OracleConnection();
       
        public void EstablishConnection()//connecting the db to fincustomerdata
        {
            try
            {
                conn.ConnectionString = connString;
                conn.Open();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }           
        public List<SearchModel> Search(string name, string zipCode)
        {
            EstablishConnection();
            OracleCommand command = new OracleCommand();
            command.Connection = conn;
           
            if (!name.Equals("") && zipCode.Equals(""))
            {
                command.CommandText = "SELECT f.customer_id,f.first_name,f.last_name,f.customer_status,f.zipcode,f.email,f.contact_number,d.due_amount,((SELECT SYSDATE FROM DUAL) - d.days_elapsed) AS payment_date FROM fin_customer_data f, dlqtable d  WHERE f.customer_id = d.account_number AND f.first_name like '%' || :name1 || '%' ";
                command.Parameters.Add(":name", name);
            }

            else if (name.Equals("") && (!zipCode.Equals("")))
            {
                command.CommandText = "SELECT f.customer_id,f.first_name,f.last_name,f.customer_status,f.zipcode,f.email,f.contact_number,d.due_amount,((SELECT SYSDATE FROM DUAL) - d.days_elapsed) AS payment_date FROM fin_customer_data f, dlqtable d  WHERE f.customer_id = d.account_number AND f.zipCode = :zip";
                command.Parameters.Add(":zip", zipCode);
            }

            else if (!name.Equals("") && (!zipCode.Equals("")))
            {       
                command.CommandText = "SELECT f.customer_id,f.first_name,f.last_name,f.customer_status,f.zipcode,f.email,f.contact_number,d.due_amount,((SELECT SYSDATE FROM DUAL) - d.days_elapsed) AS payment_date FROM fin_customer_data f, dlqtable d  WHERE f.customer_id = d.account_number AND f.first_name like '%' || :name1 || '%' AND f.zipcode = :zip1";
                command.Parameters.Add(":name1", name);
                command.Parameters.Add(":zip1", zipCode);                  
            }
            //command.Connection = conn;
            List<SearchModel> list = new List<SearchModel>();
            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                SearchModel temp = new SearchModel();
                temp.customerId = reader["customer_id"].ToString();
                temp.firstName = reader["first_name"].ToString();
                temp.lastName = reader["last_name"].ToString();
                temp.consumerStatus = (reader["customer_status"]).ToString();
                temp.zipCode = reader["zipcode"].ToString();
                temp.email = reader["email"].ToString();
                temp.contactNumber = (reader["contact_number"]).ToString();
                temp.dueAmount = reader["due_amount"].ToString();
                temp.paymentDate = reader["payment_date"].ToString();
                temp.paymentDate = Regex.Replace(temp.paymentDate, "12:00:00 AM", "");                                              
                list.Add(temp);
            }

            conn.Close();
            return list;
        }       
        public List<SearchModel> SearchByDelinquent()
        {
            EstablishConnection();
            OracleCommand command = new OracleCommand("SELECT f.customer_id,f.first_name,f.last_name,f.customer_status,f.zipcode,f.email,f.contact_number,d.due_amount,((SELECT SYSDATE FROM DUAL) - d.days_elapsed) AS payment_date FROM  fin_customer_data f, dlqtable d WHERE  d.account_number= f.customer_id", conn);
            OracleDataReader reader = command.ExecuteReader();
            List<SearchModel> list = new List<SearchModel>();
            while (reader.Read())
            {
                SearchModel temp = new SearchModel();
                temp.customerId = reader["customer_id"].ToString();
                temp.firstName = reader["first_name"].ToString();
                temp.lastName = reader["last_name"].ToString();
                temp.consumerStatus = (reader["customer_status"]).ToString();
                temp.zipCode = reader["zipcode"].ToString();
                temp.email = reader["email"].ToString();
                temp.contactNumber = (reader["contact_number"]).ToString();
                temp.dueAmount = reader["due_amount"].ToString();                
                temp.paymentDate = reader["payment_date"].ToString();
                temp.paymentDate = Regex.Replace(temp.paymentDate, "12:00:00 AM", "");
                
                list.Add(temp);
            }
            conn.Close();
            return list;
        }

        public List<string> StageWiseReport()
        {
            string StageValue;
            List<string> StageList = new List<string>();
            EstablishConnection();
            OracleCommand command = new OracleCommand();
            command.Connection = conn;
           
            command.CommandText= "SELECT COUNT(*) FROM dlqtable WHERE days_elapsed BETWEEN 1 AND 4";
            
            StageValue = command.ExecuteScalar().ToString();
            StageList.Add(StageValue);

            command.CommandText = "SELECT COUNT(*) FROM dlqtable WHERE days_elapsed BETWEEN 5 AND 7";

            StageValue = command.ExecuteScalar().ToString();
            StageList.Add(StageValue);

            command.CommandText = "SELECT COUNT(*) FROM dlqtable WHERE days_elapsed BETWEEN 8 AND 11";

            StageValue = command.ExecuteScalar().ToString();
            StageList.Add(StageValue);

            command.CommandText = "SELECT COUNT(*) FROM dlqtable WHERE days_elapsed BETWEEN 12 AND 18";

            StageValue = command.ExecuteScalar().ToString();
            StageList.Add(StageValue);

            command.CommandText = "SELECT COUNT(*) FROM dlqtable WHERE days_elapsed BETWEEN 19 AND 21";

            StageValue = command.ExecuteScalar().ToString();
            StageList.Add(StageValue);

            command.CommandText = "SELECT COUNT(*) FROM dlqtable WHERE days_elapsed = 22";

            StageValue = command.ExecuteScalar().ToString();
            StageList.Add(StageValue);

            command.CommandText = "SELECT COUNT(*) FROM dlqtable WHERE days_elapsed BETWEEN 23 AND 25";

            StageValue = command.ExecuteScalar().ToString();
            StageList.Add(StageValue);

            command.CommandText = "SELECT COUNT(*) FROM dlqtable WHERE days_elapsed BETWEEN 26 AND 29";

            StageValue = command.ExecuteScalar().ToString();
            StageList.Add(StageValue);

            command.CommandText = "SELECT COUNT(*) FROM dlqtable WHERE days_elapsed >= 30 ";

            StageValue = command.ExecuteScalar().ToString();
            StageList.Add(StageValue);

            return StageList;
        }
        public List<ActionModel> searchTreatment(string customerId)
        {
            EstablishConnection();
            OracleCommand command = new OracleCommand("SELECT account_number,status, action_date,action_type FROM action_taken WHERE account_number = :custId", conn);
            command.Parameters.Add(":custId", customerId);
            OracleDataReader reader = command.ExecuteReader();
            List<ActionModel> list = new List<ActionModel>();
            while (reader.Read())
            {
                ActionModel temp = new ActionModel();
                temp.customerId = reader["account_number"].ToString();
                temp.status = reader["status"].ToString();
                temp.actionDate = reader["action_date"].ToString();
                temp.actionType = (reader["action_type"]).ToString();
                list.Add(temp);
            }
            return list;
        }
        public List<SearchModel> SearchByDisconnected()
        {
            EstablishConnection();
            OracleCommand command = new OracleCommand("SELECT f.customer_id,f.first_name,f.last_name,f.customer_status,f.zipcode,f.email,f.contact_number,d.due_amount,((SELECT SYSDATE FROM DUAL) - d.days_elapsed) AS payment_date FROM fin_customer_data f, dlqtable d WHERE  d.account_number= f.customer_id AND d.flag=0", conn);
            OracleDataReader reader = command.ExecuteReader();
            List<SearchModel> list = new List<SearchModel>();
            while (reader.Read())
            {
                SearchModel temp = new SearchModel();
                temp.customerId = reader["customer_id"].ToString();
                temp.firstName = reader["first_name"].ToString();
                temp.lastName = reader["last_name"].ToString();
                temp.consumerStatus = (reader["customer_status"]).ToString();
                temp.zipCode = reader["zipcode"].ToString();
                temp.email = reader["email"].ToString();
                temp.contactNumber = (reader["contact_number"]).ToString();
                temp.dueAmount = reader["due_amount"].ToString();                
                temp.paymentDate = reader["payment_date"].ToString();
                temp.paymentDate = Regex.Replace(temp.paymentDate, "12:00:00 AM", "");
                
                list.Add(temp);

            }
            conn.Close();
            return list;
        }
        public List<int> jsonQueryList()
        {
            EstablishConnection();
            OracleCommand command = new OracleCommand("SELECT account_number from dlqtable", conn);
            //conn.Open();
            List<int> listOfId = new List<int>();
            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int temp;
                temp=Convert.ToInt32(reader["account_number"].ToString());
                listOfId.Add(temp);
            }
            conn.Close();

            return listOfId;
        }

        public void populatingCustomerDb()
        {
           
            EstablishConnection();
            OracleCommand command = new OracleCommand();
            command.Connection = conn;
            List<int> idList = jsonQueryList();
            foreach (var temp in idList)
            {
                ProfilePullHelper helper = new ProfilePullHelper();
                ProfilePull customerPull = ProfilePullHelper.PullProfile(Convert.ToString(temp));                
                
                command.CommandText = "INSERT INTO fin_customer_data VALUES(:customerIDP, :firstNameP, :lastNameP, :customerStatusP, :streetNameP, :zipCodeP, :cityP, :stateP, :countryP, :emailP, :contactNumberP, :dateOfBirthP)";
                command.Parameters.Add(":customerIDP", customerPull.customerdetails.customerid);
                command.Parameters.Add(":firstNameP", customerPull.customerdetails.fname);
                command.Parameters.Add(":lastNameP", customerPull.customerdetails.lname);
                command.Parameters.Add(":customerStatusP", customerPull.customerdetails.customerstatus);
                command.Parameters.Add(":streetNameP", customerPull.customerdetails.connectionaddress.streetname);
                command.Parameters.Add(":zipCodeP", customerPull.customerdetails.connectionaddress.zipcode);
                command.Parameters.Add(":cityP", customerPull.customerdetails.connectionaddress.city);
                command.Parameters.Add(":stateP", customerPull.customerdetails.connectionaddress.state);
                command.Parameters.Add(":countryP", customerPull.customerdetails.connectionaddress.country);
                command.Parameters.Add(":emailP", customerPull.customerdetails.email);
                command.Parameters.Add(":contactNumberP", customerPull.customerdetails.contactnumber);
                command.Parameters.Add(":dateOfBirthP", customerPull.customerdetails.dateofbirth);
                //command.Parameters.Add(":billAmountP", customerPull.)
                command.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
}
/*
CUSTOMERID NUMBER
FIRSTNAME VARCHAR2(30 BYTE)
LASTNAME VARCHAR2(20 BYTE)
CUSTOMERSTATUS VARCHAR2(10 BYTE)
STREETNAME VARCHAR2(20 BYTE)
ZIPCODE NUMBER
CITY VARCHAR2(20 BYTE)
STATE VARCHAR2(20 BYTE)
COUNTRY VARCHAR2(20 BYTE)
EMAIL VARCHAR2(20 BYTE)
CONTACTNUMBER NUMBER(10,0)
DATEOFBIRTH DATE
BILLEDAMOUNT NUMBER
PAYMENTDATE DATE
DUEAMOUNT NUMBER*/