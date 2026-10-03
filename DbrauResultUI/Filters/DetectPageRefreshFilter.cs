using DbrauResultUI.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace DbrauResultUI.Helpers
{
    public class DetectPageRefreshFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookie = filterContext.HttpContext.Request.Cookies["RefreshFilter"];
            filterContext.RouteData.Values["IsRefreshed"] = cookie != null && cookie == filterContext.HttpContext.Request.Url();

            string controller = filterContext.RouteData.Values["Controller"].ToString();
            if (controller != "Payment")
            {
                filterContext.HttpContext.Response.Cookies.Append("RefreshFilter", filterContext.HttpContext.Request.Url());
            }
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //if (controller != "Payment")
            //{
            //    filterContext.HttpContext.Response.SetCookie(new HttpCookie("RefreshFilter", filterContext.HttpContext.Request.Url.ToString()));
            //}
        }
    }
}
