using Fintech.Models.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintech.Models.CodeFirst
{
    public class Invite
    {
        public int Id { get; set; }
        public int HouseHoldId { get; set; }
        public string Email { get; set; }
        public Guid Token { get; set; }
        public DateTimeOffset InviteDate { get; set; }
        public string InvitedbyId { get; set; }
        public bool hasbeenUsed { get; set; }



        public virtual HouseHold HouseHold { get; set; }
        public virtual ApplicationUser Invitedby { get;set;}

    }
}