using Fintech.Models.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintech.Models
{
    public class HouseHoldToUserViewModels
    {
        public HouseHold HouseHold { get; set; }
        public ApplicationUser User { get; set; }

    }
}