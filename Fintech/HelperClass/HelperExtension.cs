using Fintech.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace Fintech.HelperClass
{
    public static class HelperExtension
    {

        public static ApplicationDbContext db = new ApplicationDbContext();


        public static string GetName(this IIdentity user)
        {
            var ClaimsUser = (ClaimsIdentity)user;
            var claim = ClaimsUser.Claims.FirstOrDefault(c => c.Type == "Name");
            if (claim != null)
            {
                return claim.Value;

            }
            else
            {
                return null;
            }
        }

        public static int? GetHouseHoldId(this IIdentity user)
        {
            var claimsIdentity = (ClaimsIdentity)user;
            var HouseHoldClaims = claimsIdentity.Claims
                .FirstOrDefault(m => m.Type == "HouseHoldId");
            if (HouseHoldClaims != null)
            {
                return int.Parse(HouseHoldClaims.Value);
            }
            else
            {
                return null;
            }
        }

        public static bool IsInHouseHoldId(this IIdentity user)
        {
            var cUser = (ClaimsIdentity)user;
            var hid = cUser.Claims.FirstOrDefault(m => m.Type == "HouseHoldId");

            return (hid != null && string.IsNullOrWhiteSpace(hid.Value));
        }

        public static async Task RefreshAuthentication(this HttpContextBase context, ApplicationUser user)
        {
            context.GetOwinContext().Authentication.SignOut();
            await context.GetOwinContext().Get<ApplicationSignInManager>().SignInAsync(user, isPersistent: false, rememberBrowser: false);


        }





    }
}