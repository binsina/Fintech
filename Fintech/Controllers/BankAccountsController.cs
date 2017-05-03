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
using Fintech.HelperClass;

namespace Fintech.Controllers
{
    public class BankAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccounts
        public ActionResult Index(int HouseHoldId)
        {
            ViewBag.Id = HouseHoldId;
            BankAccountViewModel model = new BankAccountViewModel();
            
            var bankAccounts = db.BankAccounts
                .Where(m => m.HouseHoldId == HouseHoldId)
                .Include(b => b.HouseHold);

            model.BA = bankAccounts.ToList();
            model.HouseHoldId = HouseHoldId;
            

            return View(model);

            //return View(bankAccounts.ToList());
        }
        
        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }


            var Debt = bankAccount.Transactions.Where(m => m.Type == true).Sum(m => m.Amount);
           bankAccount.Balance = bankAccount.Balance - Debt;

            var Credit = bankAccount.Transactions.Where(m => m.Type == false).Sum(m => m.Amount);
            bankAccount.Balance = bankAccount.Balance + Credit;
            ViewBag.TotalBalance = bankAccount.Balance;

            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        //[AuthorizeHouseHold]
        public ActionResult Create(int HouseHoldId)
        {
            BankAccount bankAccount = new BankAccount();
            bankAccount.HouseHoldId = User.Identity.GetHouseHoldId().Value;
            

            return View(bankAccount);
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HouseHoldId,Name,Balance,ReconciledBalance,SoftDelete,Date")] BankAccount bankAccount)
        {
            ViewBag.Id = bankAccount.HouseHoldId;
            if (ModelState.IsValid)
            {
                bankAccount.Date = DateTimeOffset.Now;

                db.BankAccounts.Add(bankAccount);
                db.SaveChanges();

                return RedirectToAction("Index", new { HouseHoldId = bankAccount.HouseHoldId });
            }

            ViewBag.HouseHoldId = new SelectList(db.HouseHolds, "Id", "Name", bankAccount.HouseHoldId);
            return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseHoldId = new SelectList(db.HouseHolds, "Id", "Name", bankAccount.HouseHoldId);
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseHoldId,Name,Balance,ReconciledBalance,SoftDelete,Date")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {HouseHoldId = bankAccount.HouseHoldId });
                
            }
            ViewBag.HouseHoldId = new SelectList(db.HouseHolds, "Id", "Name", bankAccount.HouseHoldId);
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(Id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            BankAccount bankAccount = db.BankAccounts.Find(Id);
            db.BankAccounts.Remove(bankAccount);
            db.SaveChanges();
            return RedirectToAction("Index", new { HouseHoldId = bankAccount.HouseHoldId });
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
