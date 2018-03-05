using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC2018.Models;

namespace CommunityAssistMVC2018.Controllers
{
    public class DonationController : Controller
    {
        // GET: Donation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Result()
        {
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Index([Bind(Include = "Donation")]DonationClass donate)
        {
            CommunityAssist2017Entities db = new CommunityAssist2017Entities();

            if (Session["PersonKey"] != null)
            {
                Donation d = new Donation();
                d.PersonKey = (int) Session["PersonKey"];
                d.DonationDate = DateTime.Now;
                d.DonationAmount = donate.Donation;
       
                d.DonationConfirmationCode = Guid.NewGuid();
                db.Donations.Add(d);
                db.SaveChanges();
            }

            return View("Result");
        }
    }
}