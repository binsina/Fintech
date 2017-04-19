using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintech.Models.ModelClass
{
    public class Budget
    {
        public Budget()
        {
            BudgetItems = new HashSet<BudgetItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int HouseHoldId { get; set; }

        public virtual HouseHold HouseHold { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }
    }
}