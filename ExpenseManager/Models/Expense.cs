using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExpenseManager.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseID { get; set; }

        [DataType(DataType.Currency)]
        public decimal Value { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        
        public DateTime Date { get; set; }

        public string Comments { get; set; }

        public ApplicationUser User { get; set; }

        public int CategoryID { get; set; }
        
        public  Category Category { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime SearchedDate { get; set; }
        
    }
}