using Fintech.Models.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintech.Models
{
    public class BudgetItemViewModels
    {
        public int BudgetId { get; set; }
        public int CategoryId { get; set; }
        public List<BudgetItem> BudItem { get; set; }

    }
}