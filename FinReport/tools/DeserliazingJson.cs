using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;
using FinReport.Models;


namespace FinReport.tools
{
    public class ProfilePullHelper
    {
        private static string profilePullUrl = "http://192.168.1.19:8080/OrderManagement/rest/om/profilePull/";

        public static ProfilePull PullProfile(string customerId)
        {
            ProfilePull customerProfile = null;
            string url = profilePullUrl + "42";//customerid
            string customerProfileJson = GetJsonFromUrl(url);
            //string customerProfileJson = File.ReadAllText("D:\\profile.json");//give json file path here
            if (!String.IsNullOrWhiteSpace(customerProfileJson))
            {
                customerProfile = GetObjectFromJson<ProfilePull>(customerProfileJson);

            }

            return customerProfile;
        }

        public static string GetJsonFromUrl(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString(url);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static T GetObjectFromJson<T>(string json)
        {
            JavaScriptSerializer convertor = new JavaScriptSerializer();
            //T temp = new T;
            T temp = convertor.Deserialize<T>(json);
            return temp;
        }


    }
}