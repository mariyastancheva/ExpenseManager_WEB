using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpenseManager.Models
{
    public class Category
    {

        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        public ApplicationUser User { get; set; }

        public  ICollection<Expense> Expenses { get; set; }
    }
}