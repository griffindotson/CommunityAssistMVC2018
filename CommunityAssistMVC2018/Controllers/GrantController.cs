using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC2018.Models;

namespace CommunityAssistMVC2018.Controllers
{
    public class GrantController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        // GET: Grant
        public ActionResult Index()
        {
            //check to make sure they are logged in

            if (Session["PersonKey"] == null)

            {

                Message m = new Message();

                m.MessageText = "You must be logged in to apply for a grant";

                return RedirectToAction("Result", m);

            }

            ViewBag.GrantType = new SelectList(db.GrantTypes, "GrantTypeKey", "GrantTypeName");
            return View();
        }

        public ActionResult Result(Message m)

        {

            return View(m);

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Index([Bind(Include = "GrantType, GrantApplicationReason, GrantApplicationRequestAmount")]GrantApplication app)
        {
            CommunityAssist2017Entities db = new CommunityAssist2017Entities();

            if (Session["PersonKey"] != null)
            {
                GrantApplication g = new GrantApplication();
                g.PersonKey = (int)Session["PersonKey"];
                g.GrantAppicationDate = DateTime.Now;
                g.GrantApplicationRequestAmount = app.GrantApplicationRequestAmount;
                g.GrantApplicationReason = app.GrantApplicationReason;
                g.GrantApplicationStatusKey = 1;
                g.GrantType = app.GrantType;
                


                db.GrantApplications.Add(g);
                db.SaveChanges();

                Message m = new Message();

                m.MessageText = "Your application has been submitted";
                return RedirectToAction("Result", m);
            }
            return View("Result");
        }
    }
}