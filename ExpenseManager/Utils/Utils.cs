using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseManager.Utils
{
    public class Utils
    {
        public static string ConvertSearchDate(string searchDate)
        {
            string[] array = searchDate.Split('-').ToArray();
            string[] reversed = array.Reverse().ToArray(); 
            string newDate = String.Join("-", reversed);
            return newDate;
        }
    }
}