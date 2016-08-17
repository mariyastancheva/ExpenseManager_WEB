using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ExpenseManager.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var expenses = db.Expenses.Include(e => e.Category).Include(p => p.User).Where(e=>e.User.UserName == User.Identity.Name);
            return View(expenses.ToList());
        }
     
    }
}