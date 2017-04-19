using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fintech.Models.ModelClass
{
    public class HouseHold
    {
        public HouseHold()
        {
            Budgets = new HashSet<Budget>();
            Categories = new HashSet<Category>();
            BankAccounts = new HashSet<BankAccount>();
            Users = new HashSet<ApplicationUser>();
        }


        public int Id { get; set; }
        [Required]
        [Display(Name = "HouseHold Name")]
        public string Name { get; set; }


        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Category>Categories { get; set; }
        public virtual ICollection<BankAccount>BankAccounts { get; set; }
        public virtual ICollection<ApplicationUser>Users { get; set; }

    }
}