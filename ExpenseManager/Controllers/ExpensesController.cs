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
using PagedList;
using PagedList.Mvc;

namespace ExpenseManager.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

   
        public DateTime SearchedDate { get; set; }
        public ActionResult Index(string sortOrder,string searchString,string searchedDate,int? page)
        {
            ViewBag.TotalValue = 0;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
      
            var expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).Where(e => e.User.UserName == User.Identity.Name);
            
           
            switch (sortOrder)
            {
                case "name_desc":
                    expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).OrderByDescending(s => s.Category.Title).Where(e => e.User.UserName == User.Identity.Name);
                    break;
                case "Date":
                    expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).OrderBy(s => s.Date).Where(e => e.User.UserName == User.Identity.Name);
                    break;
                case "value_desc":
                    expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).OrderByDescending(s => s.Value).Where(e => e.User.UserName == User.Identity.Name);
                    break;
                default:
                    expenses = db.Expenses.Where(e => e.User.UserName == User.Identity.Name).Include(e => e.Category).Include(p => p.User).OrderBy(s => s.Category.Title);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).Where(e => e.Category.Title.Contains(searchString)).Where(e => e.User.UserName == User.Identity.Name);
            }
            if (!String.IsNullOrEmpty(searchedDate))
            {
                var newSearchDate = Utils.Utils.ConvertSearchDate(searchedDate);
                DateTime wantedDate = DateTime.ParseExact(newSearchDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime.ParseExact(searchedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).Where(e => e.Date.Equals(wantedDate)).Where(e => e.User.UserName == User.Identity.Name);
               
            }
            if (expenses.Any())
            {
                ViewBag.TotalValue = expenses.Sum(e => e.Value);
            }
            
            return View(expenses.ToList().ToPagedList(page ?? 1,10));
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
            if (db.Categories.Where(e=>e.User.UserName==User.Identity.Name).Count()!=0)
            {
                ViewBag.HasCategories = true;
            }
            else
            {
                ViewBag.HasCategories = false;
            }
            ViewBag.CategoryID = new SelectList(db.Categories.Where(c=>c.User.UserName==User.Identity.Name), "CategoryId", "Title");
            return View();
        }

        // POST: Expenses/Create    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpenseID,Value,Date,Comments,CategoryID")] Expense expense)
        {

            if (ModelState.IsValid)
            {
               
                expense.User = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                db.Expenses.Add(expense);
                try
                {
                    db.SaveChanges();
                   
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {  
                    return RedirectToAction("Create","Expenses");
                }
                
               
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
            
            ViewBag.CategoryID = new SelectList(db.Categories.Where(c=>c.User.UserName==User.Identity.Name), "CategoryId", "Title", expense.CategoryID);
            
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
                try
                {
                    db.Entry(expense).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    return RedirectToAction("Edit","Expenses");
                }
               
            }
            ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.User.UserName == User.Identity.Name), "CategoryId", "Title", expense.CategoryID);
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
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //// POST: Expenses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Expense expense = db.Expenses.Find(id);
        //    db.Expenses.Remove(expense);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
            ViewBag.totalExpense = totalExpenses;

            return View();
        }
    }
}
