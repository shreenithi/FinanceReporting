using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Configuration;
/*
using iTextSharp.text;

using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
*/
namespace FinReport.tools
{
    public class FinLoginValidation
    {
        private static string connString = ConfigurationManager.ConnectionStrings["connectString"].ConnectionString;
        string finDept;
        string finUserName;
        string finPwd;
        public FinLoginValidation()
        {
            // TODO: Add constructor logic here
        }
        void validate()
        {
            try
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = connString;
                conn.Open();
                DatabaseAccessForReporting obj = new DatabaseAccessForReporting();
                obj.EstablishConnection();
                OracleCommand command = new OracleCommand("Select role from finlogin where username= :username and password= :password", conn);
                command.Parameters.Add(":username", finUserName);
                command.Parameters.Add(":password", finPwd);            
                finDept = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public string loginDataValue(string username, string pwd)
        {
            finUserName = username;
            finPwd = pwd;
            validate();
            return (finDept);
        }
    }
}