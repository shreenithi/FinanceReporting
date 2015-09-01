using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinReport.tools
{
    public class DatabaselinkingString
    {
        public string ConnectionStringForLogin()
        {
            return "Provider = MSDAORA; Data Source = orcl; Persist Security Info = True; User ID = frpa;Password=password";
        }
    }
}