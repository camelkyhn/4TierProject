using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4TierProject.WEB.UI.Models
{
    public class FacebookInformationModel
    {
        public dynamic jsonObj { get; set; }

        public FacebookInformationModel(dynamic json)
        {
            jsonObj = json;
            id = jsonObj.id;
            first_name = jsonObj.first_name;
            last_name = jsonObj.last_name;
            email = jsonObj.email;
        }

        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
    }

    public class target
    {
        public string id { get; set; }
        public string url { get; set; }
    }
}