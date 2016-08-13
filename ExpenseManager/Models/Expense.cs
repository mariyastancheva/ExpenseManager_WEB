using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpenseManager.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Value { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public Category Category { get; set; }

        public DateTime Date { get; set; }
    }
}