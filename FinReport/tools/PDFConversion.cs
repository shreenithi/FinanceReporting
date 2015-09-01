using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Pechkin;



namespace FinReport.tools
{
    public partial class PDFConversion : System.Web.UI.Page
    {
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
    }
}