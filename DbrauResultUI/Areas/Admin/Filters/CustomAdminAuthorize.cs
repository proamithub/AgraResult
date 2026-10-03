using DbrauResultUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;

namespace DbrauResultUI.Areas.Admin.Filters
{
    public class CustomAdminAuthorize : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //authentication
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userData = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
                if (!string.IsNullOrEmpty(userData))
                {
                    var user = JsonConvert.DeserializeObject<CustomPrincipal>(userData);
                    //Authorization
                    if (!user.Roles.Any(r => Roles.Contains(r)))
                    {
                        context.Result = new RedirectToActionResult("UnAuthorize", "Account", new { area = "" });
                    }
                }
                else
                {
                    context.Result = new RedirectToActionResult("Login", "Account", new { area = "Admin" });
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "Admin" });
            }
        }
    }
}
