using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fintech.Models.ModelClass
{
    public class BudgetItem
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Please provide a category for reporting purposes.")]
        public int CategoryId { get; set; }

        public int BudgetId { get; set; }
        public decimal Amount { get; set; }


        public virtual Category Category { get; set; }
        public virtual Budget Budget { get; set; }

    }
}