
using Fintech.Models;
using Fintech.Models.ModelClass;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fintech.Controllers
{
    public class HomeController : Controller

    {
        ApplicationDbContext db = new ApplicationDbContext();
        //Get
        //[Authorize]
        public ActionResult JoinHouseHold(HouseHoldViewModel model)
        {
            HouseHold hh = db.HouseHolds.Find(model.HHId);
            model.Member = db.Users.Find(User.Identity.GetUserId());
            hh.Users.Add(model.Member);
            db.SaveChanges();

            return RedirectToAction("Index","HouseHolds");

        }

        //Post
        [HttpPost]
        public ActionResult CreateHouseHold(HouseHoldViewModel model)
        {
            HouseHold hh = new HouseHold();
            hh.Name = model.HHName;

            model.Member = db.Users.Find(User.Identity.GetUserId());

            db.HouseHolds.Add(hh);
            db.SaveChanges();

            hh.Users.Add(model.Member);
            db.SaveChanges();

            return RedirectToAction("Index", "HouseHolds");
              

        }

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}