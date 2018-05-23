using _4TierProject.WEB.UI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace _4TierProject.WEB.UI.Controllers
{
    //Just for testing
    [CustomAuthorize]
    public class FacebookController : BaseController
    {
        // GET: Facebook
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Posts()
        {
            var currentClaims = await UserManager.GetClaimsAsync(HttpContext.User.Identity.GetUserId());
            var accessToken = currentClaims.FirstOrDefault(x => x.Type == "urn:tokens:facebook");

            if (accessToken == null)
            {
                return (new HttpStatusCodeResult(HttpStatusCode.NotFound, "Token Not Found"));
            }

            string url = String.Format("https://graph.facebook.com/me?fields=id,email,first_name,last_name&access_token={0}", accessToken.Value);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";

            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string result = await reader.ReadToEndAsync();

                dynamic jsonObject = System.Web.Helpers.Json.Decode(result);

                FacebookInformationModel info = new FacebookInformationModel(jsonObject);

                ViewBag.JSON = result;

                return View(info);
            }

        }
    }
}