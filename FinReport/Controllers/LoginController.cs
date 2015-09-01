using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinReport.Models;
using FinReport.tools;
using Pechkin;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


namespace FinReport.Controllers
{

    public class LoginController : Controller
    {
       
        // GET: Login  
        public ActionResult loginPage()
        {
            return View((object)"");
            
        }
        [HttpPost]
        public ActionResult loginPage(string loginName, string loginPassword)
        {
            FinLoginValidation obj = new FinLoginValidation();

            string role = obj.loginDataValue(loginName, loginPassword);
            if (role == "Administrator")
            {
                //Admin page call statements
                Session["name"] = loginName;
               Response.Redirect("Administrator");
            }
            else if (role == "Representative")
            {
                Session["name"] = loginName;
                Response.Redirect("http://192.168.1.15:8080/CFO/RepPage.html");

            }
            else if (role == "Collection")
            {
                Session["name"] = loginName;
                Response.Redirect("http://192.168.1.15:8080/CFO/CollectionsHome.html");

            }
            else if (role == "Reporting")
            {
                user userObj = new user();
                userObj.populatingDb();
                Session["name"] = loginName;
                Response.Redirect("ReportingHome");
               
            }
            else
            {
                return View((object)"Invalid User/Password");
            }
            return View((object)"");
            
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Response.Redirect("loginPage");
            return View();
        }

        public ActionResult Administrator()
        {
            if (Session["name"] == null)
                Response.Redirect("loginPage");            
            return View();
        }

        public ActionResult Representative()
        {
            if (Session["name"] == null)
                Response.Redirect("loginPage");
            return View();
           
        }
        public ActionResult ReportingHome()
        {
            if (Session["name"] == null)
                Response.Redirect("loginPage");            
            return View();
        }

        public ActionResult Collections()
        {
            if (Session["name"] == null)
                Response.Redirect("loginPage");
            return View();
        }
        public ActionResult Search()
        {
            if (Session["name"] == null)
                Response.Redirect("loginPage");
            return View(new List<SearchModel>());
        }
        [HttpPost]
        public ActionResult Search(string customerName, string zipCode)
        {
            List<SearchModel> customerData = new List<SearchModel>();
            user obj = new user();
            
            customerData=obj.reporting(customerName,zipCode);
            return View(customerData);
            // call function in model to invoke search from database and return a list
        }
        public ActionResult StageWiseReport()
        {
            if (Session["name"] == null)
                Response.Redirect("Login/loginPage");

            List<string> StageList = new List<string>();
            user obj = new user();
            StageList = obj.StagewiseReport();
            return View(StageList);
        }
        public ActionResult DelinquentCustomers()
        {
            if (Session["name"] == null)
                Response.Redirect("Login/loginPage");

            List<SearchModel> customerData = new List<SearchModel>();
            user obj = new user();
            customerData = obj.Delinquent();
            return View(customerData);
        }
        public ActionResult TreatmentHistory()
        {
            if (Session["name"] == null)
                Response.Redirect("Login/loginPage");

            return View(new List<ActionModel>());           
        }
        [HttpPost]
        public ActionResult TreatmentHistory(string treatmentAccNo)
        {
            List<ActionModel> customerData = new List<ActionModel>();
            user obj = new user();
            customerData = obj.treatmentHistory(treatmentAccNo);
            return View(customerData);
        }
        public ActionResult DisconnectedCustomers()
        {
            if (Session["name"] == null)
                Response.Redirect("Login/loginPage");

            List<SearchModel> customerData = new List<SearchModel>();
            user obj = new user();
            customerData = obj.Disconnected();
            return View(customerData);
        }

        public partial class PdfTest : System.Web.UI.Page
        {
            protected void Page_Load(object sender, EventArgs e)
            {

            }

            protected override void Render(HtmlTextWriter writer)
            {
                // setup a TextWriter to capture the markup
                TextWriter tw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(tw);

                // render the markup into our surrogate TextWriter
                base.Render(htw);

                // get the captured markup as a string
                string pageSource = tw.ToString();

                // render the markup into the output stream verbatim
                writer.Write(pageSource);

                Session["source"] = pageSource;

                //// remove the viewstate field from the captured markup
                //string viewStateRemoved = Regex.Replace(pageSource,
                //    "<input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\" value=\".*?\" />",
                //    "", RegexOptions.IgnoreCase);


                // the page source, without the viewstate field, is in viewStateRemoved
                // do what you like with it
            }

            protected void Button1_Click(object sender, EventArgs e)
            {
                if (Session["source"] != null)
                {
                    byte[] pdfBuf = new SimplePechkin(new GlobalConfig()).Convert((string)Session["source"]);
                    System.IO.File.WriteAllBytes("C:\\test.pdf", pdfBuf);
                }
            }
        }

    }
}