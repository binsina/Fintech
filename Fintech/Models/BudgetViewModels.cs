using Fintech.Models.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintech.Models
{
    public class BudgetViewModels
    {
        public int HouseHoldId { get; set; }
        public List<Budget> Bud { get; set; }
    }
}