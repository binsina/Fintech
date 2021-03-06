﻿using Fintech.HelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fintech.Models
{
    public class AuthorizeHouseHold : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);

            if (!isAuthorized)
            {
                return false;
            }


            return httpContext.User.Identity.IsInHouseHoldId();
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }


            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                   new { controller = "Home", action = "CreatJoinHouseHold" }));
            }


        }
    }
}