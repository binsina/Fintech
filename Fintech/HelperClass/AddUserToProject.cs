using Fintech.Models;
using Fintech.Models.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintech.HelperClass
{
    public class AddUserToProject
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public bool IsUserOnProject(string userId, int HouseHoldId)
        {
            var project = db.HouseHolds.Find(HouseHoldId);
            var flag = project.Users.Any(u => u.Id == userId);
            return (flag);
        }


        public void AddNewUserToProject(string userId, int HouseHoldId)
        {
            if (!IsUserOnProject(userId, HouseHoldId))
            {
                HouseHold proj = db.HouseHolds.Find(HouseHoldId);
                var newUser = db.Users.Find(userId);

                proj.Users.Add(newUser);
                db.SaveChanges();
            }
        }






    }
}