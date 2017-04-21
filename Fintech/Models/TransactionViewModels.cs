using Fintech.Models.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintech.Models
{
    public class TransactionViewModels
    {
        public int BankAccountId { get; set; }
        public List<Transaction> TA { get; set; }
        public BankAccount BankAccount { get; set; }
        public int HouseHoldId { get; set; }
    }
}