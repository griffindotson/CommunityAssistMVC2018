using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC2018.Models;

namespace CommunityAssistMVC2018.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Index([Bind(Include = "Email, Password, UserKey")]LoginClass loginClass)
        {
            //make connection to Ado Entity model classes
            CommunityAssist2017Entities user = new CommunityAssist2017Entities();

            //Assign user key a value of 0
            loginClass.UserKey = 0;
            //pass the values to the stored procedure and get result (-1 = failure)
            int result = user.usp_Login(loginClass.Email, loginClass.Password);
            //test the results
            if (result != -1)
            {
                //run a query to get the UserKey
                var ukey = (from r in user.People
                            where r.PersonEmail.Equals(loginClass.Email)
                            select r.PersonKey).FirstOrDefault();
                loginClass.UserKey = (int)ukey;
                Session["personKey"] = loginClass.UserKey;
            }

            //return the class to the Result view
            return View("Result", loginClass);
        }
        public ActionResult Result(LoginClass loginClass)
        {
            return View(loginClass);
        }

    }
}