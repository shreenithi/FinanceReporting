using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinReport.tools
{
    public class DatabaseLinkingString
    {
        public string ConnectionStringForLogin()
        {
            return "Data Source=orcl;Persist Security Info=True;User ID=frpa; Password=password";
        }
        public string ConnectionStringForCustomerDetails()
        {
            return " Data Source = orcl; Persist Security Info = True; User ID = frpa;Password=password";
        }

    }
}


//Provider=MSDAORA;Data Source=orcl;Persist Security Info=True;User ID=frpa