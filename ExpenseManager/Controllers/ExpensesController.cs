using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExpenseManager.Models;
using ExpenseManager.Utils;
using System.Globalization;

namespace ExpenseManager.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

   
        public DateTime SearchedDate { get; set; }
        public ActionResult Index(string sortOrder,string searchString,string searchedDate)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
      
            var expenses = db.Expenses.Include(e => e.Category).Include(p => p.User);
            
           
            switch (sortOrder)
            {
                case "name_desc":
                    expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).OrderByDescending(s => s.Category.Title);
                    break;
                case "Date":
                    expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).OrderBy(s => s.Date);
                    break;
                case "value_desc":
                    expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).OrderByDescending(s => s.Value);
                    break;
                default:
                    expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).OrderBy(s => s.Category.Title);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).Where(e => e.Category.Title.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchedDate))
            {
                //var newSearchDate = Utils.Utils.ConvertSearchDate(searchedDate);
                //DateTime wantedDate = DateTime.ParseExact(newSearchDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime.ParseExact(searchedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).Where(e => e.Date.Equals(searchedDate));
            }
            return View(expenses.ToList());
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories.Where(c=>c.User.UserName==User.Identity.Name), "CategoryId", "Title");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpenseID,Value,Date,Comments,CategoryID")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                expense.User = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "Title", expense.CategoryID);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            if(expense.User.ToString() == User.Identity.Name.ToString()) { 
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "Title", expense.CategoryID);
            }
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpenseID,Value,Date,Comments,CategoryID")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "Title", expense.CategoryID);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
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
        public ActionResult ShowTotal()
        {

         
            var totalExpenses = db.Expenses.Where(e=>e.User.UserName == User.Identity.Name)
                .Sum(e => e.Value);
           // var dailyExpenses = db.Expenses.Where(e => e.User.UserName == User.Identity.Name
                                                //   && e.Date.Equals(SelectedDate));
           // var categoryExpenses = db.Expenses.Where(e => e.User.UserName == User.Identity.Name && 
                                                      // e.CategoryID == );





            ViewBag.totalExpense = totalExpenses;

            return View();
        }
    }
}
