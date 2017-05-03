using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fintech.Models;
using Fintech.Models.ModelClass;
using Microsoft.AspNet.Identity;
using Fintech.HelperClass;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Fintech.Models.CodeFirst;

namespace Fintech.Controllers
{
    public class HouseHoldsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

      
        // GET: HouseHolds
        public ActionResult Index()
        {
            
            var user = User.Identity.GetUserId();
            HouseHold hh = new HouseHold();
            ViewBag.HouseHoldId = hh.Id;
            hh = db.HouseHolds.FirstOrDefault(m => m.Users.FirstOrDefault().Id == user);

            if (hh != null)
            {
                return View(hh);
            }
            else
            {
               return View("_ErrorPage");
            }
            
               
        }

        public ActionResult LeaveHouseHold()
        {
            return View();
        }


        //[HttpPost]
        //public async Task<ActionResult> LeaveHouseHold(HouseHold model)
        //{
        //    HouseHold hh = db.HouseHolds.Find(model.Id);
        //    ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
        //   await  hh.Users.Remove(model.Id);
        //    db.SaveChanges();

        //    return RedirectToAction("Login", "Account");

        //}




        //GeT
        public ActionResult Invite()
        {


            return View();

        }

        [HttpPost]
        public async Task<ActionResult> Invite(string Email)
        {
            var code = Guid.NewGuid();
            var callbackUrl = Url.Action("CreateJoinHouseHold", "Home", new {code = code }, protocol: Request.Url.Scheme);

            EmailService EmailSend = new EmailService();
            IdentityMessage message = new IdentityMessage();

            message.Body = "Please Join my Household. Use the following Token to join" +" "+ code +" "+ "Please Click <a href=\" " + callbackUrl +" "+ "\">here to join!!</a>";
            message.Subject = "Your Invited to a Household";
            message.Destination = Email;

            await (EmailSend.SendMailAsync(message));


            //create a record in the invite table
            Invite InvitePerson = new Invite();
            InvitePerson.Email = Email;
            InvitePerson.Token = code;
            InvitePerson.HouseHoldId = User.Identity.GetHouseHoldId().Value;
            InvitePerson.InviteDate = DateTimeOffset.Now;
            InvitePerson.InvitedbyId = User.Identity.GetUserId();
            db.Invites.Add(InvitePerson);
            db.SaveChanges();

            return RedirectToAction("Index","Home");

        }






        // GET: HouseHolds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseHold houseHold = db.HouseHolds.Find(id);
            if (houseHold == null)
            {
                return HttpNotFound();
            }
            return View(houseHold);
        }

        // GET: HouseHolds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HouseHolds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AuthorizeHouseHold]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] HouseHold houseHold)

        {
            //var userId = User.Identity.GetUserId();
            //var user = db.Users.FirstOrDefault(u => u.Id == userId);

            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
                if (user.HouseHold != null)
                {
                   
                    ViewBag.ErrorMessage = "You can only belong to one household at a time. If you would  like to create a new household, please leave your current household.";
                    return PartialView("_ErrorPage");
                }


                db.HouseHolds.Add(houseHold);
                user.HouseHoldId = houseHold.Id;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(houseHold);
        }

        // GET: HouseHolds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseHold houseHold = db.HouseHolds.Find(id);
            if (houseHold == null)
            {
                return HttpNotFound();
            }
            return View(houseHold);
        }

        // POST: HouseHolds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] HouseHold houseHold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(houseHold).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(houseHold);
        }

        // GET: HouseHolds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseHold houseHold = db.HouseHolds.Find(id);
            if (houseHold == null)
            {
                return HttpNotFound();
            }
            return View(houseHold);
        }

        // POST: HouseHolds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HouseHold houseHold = db.HouseHolds.Find(id);
            db.HouseHolds.Remove(houseHold);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
