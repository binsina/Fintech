using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fintech.Models.ModelClass
{
    public class Transaction
    {
        public int Id { get; set; }

        //[Required]
        [Display (Name ="Bank Account")]
        public int BankAccountId { get; set; }

        [AllowHtml]
        public string Description { get; set; }
        
        [Display(Name ="Transaction Date")]
        public DateTimeOffset Date { get; set; }

        public decimal Amount { get; set; }

        [Display(Name ="Transaction Type")]

        public bool Type { get; set; }

        public int CategoryId { get; set; }

        [Display(Name ="Entered By")]
        public string UserId { get; set; }

        public bool Reconciled { get; set; }

        public bool SoftDelete { get; set; }

        [Display (Name ="Reconciled Amount")]
        public int ReconciledAmount { get; set; }

        public string EnteredById { get; set; }

        public virtual BankAccount BankAccount { get; set; }
        public virtual ApplicationUser EnteredBy{ get; set; }
        public virtual Category Category { get; set; }
    }
}