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
            var allCategoriesAndExpenses = CalculatingCategoriesAndPrices(expensesList);
            return allCategoriesAndExpenses;
        }

        public static Dictionary<string, decimal> GetTodaysData(IEnumerable<Expense> expenses)
        {
            List<Expense> expensesList = expenses.Where(e=>e.Date==DateTime.Today).ToList();
            var allCategoriesAndExpenses = CalculatingCategoriesAndPrices(expensesList);
            return allCategoriesAndExpenses;
        }
        public static Dictionary<string, decimal> GetDataFromThisMonth(IEnumerable<Expense> expenses)
        {
            List<Expense> expensesList = expenses.Where(e=>e.Date.Month==DateTime.Now.Month).ToList();
            var allCategoriesAndExpenses = CalculatingCategoriesAndPrices(expensesList);
            return allCategoriesAndExpenses;
        }

        public static Dictionary<string, decimal> GetDataFromThisYear(IEnumerable<Expense> expenses)
        {
            List<Expense> expensesList = expenses.Where(e => e.Date.Year == DateTime.Now.Year).ToList();
            var allCategoriesAndExpenses= CalculatingCategoriesAndPrices(expensesList);
            return allCategoriesAndExpenses;
        }

        private static Dictionary<string, decimal> CalculatingCategoriesAndPrices(List<Expense> expList)
        {
            Dictionary<string, decimal> allCategoriesAndExpenses = new Dictionary<string, decimal>();

            foreach (var e in expList)
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