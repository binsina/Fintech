using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fintech.Models.ModelClass
{
    public class BankAccount
    {
        public BankAccount()
        {
            Transactions = new HashSet<Transaction>();
        }



        public int Id { get; set; }

      
        public int HouseHoldId { get; set; }
        [Required]
        [Display(Name = "Account Name")]
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public bool ReconciledBalance { get; set; }

        public bool SoftDelete { get; set; }

        [Display (Name="Date Account Created")]
        public DateTimeOffset Date { get; set; }

        public virtual HouseHold HouseHold { get; set; }
        
        public virtual ICollection<Transaction> Transactions { get; set; }


    }
}