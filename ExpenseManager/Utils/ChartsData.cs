using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseManager.Utils
{
    public static class ChartsData
    {
        public static Dictionary<string, decimal> GetTotalData(IEnumerable<Expense> expenses)
        {
            List<Expense> expensesList = expenses.ToList();
            Dictionary<string, decimal> allCategoriesAndExpenses = new Dictionary<string, decimal>();
            //Dictionary<string, decimal> categoriesAndExpenses = new Dictionary<string, decimal>();
            foreach (var e in expensesList)
            {
                if (allCategoriesAndExpenses.ContainsKey(e.Category.Title))
                {
                    allCategoriesAndExpenses[e.Category.Title] += e.Value;
                }
                else
                {
                    allCategoriesAndExpenses.Add(e.Category.Title, e.Value);
                }
            }
            return allCategoriesAndExpenses;
        }
    }
}