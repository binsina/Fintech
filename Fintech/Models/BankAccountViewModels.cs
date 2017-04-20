using Fintech.Models.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintech.Models
{
    public class BankAccountViewModels
    {

        public int HouseHoldId { get; set; } 

        
        public List<BankAccount> BA { get; set; }


    }
}