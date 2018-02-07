using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC2018.Models;

namespace CommunityAssistMVC2018.Controllers
{
    public class RegistrationController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        // GET: Registration
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "lastName, firstName, email, password, " +
            "apartmentNumber, street, city, state, zipcode, phone")]NewPerson p )
        {
            int result = db.usp_Register(p.LastName, p.FirstName, p.Email,
                p.Password, p.ApartmentNumber, p.Street, p.City, p.State, p.ZipCode, p.Phone);

            if(result != -1)
            {
                return RedirectToAction("ThankYou");
            }

            return View();
        }
    }
}