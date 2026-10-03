using System.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace DbrauResultUI.Helpers
{
    public class TimeZoneFliter : Attribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Cookies["timezoneoffset"] != null)
            {
                filterContext.HttpContext.Session.SetString("timezoneoffset", filterContext.HttpContext.Request.Cookies["timezoneoffset"]);

                var defaultzone = filterContext.HttpContext.Session.Get("timezoneoffset") != null ? filterContext.HttpContext.Session.GetString("timezoneoffset") : "-330";
              
                double minutes2 = Convert.ToDouble(defaultzone);
                IEnumerable<TimeZoneInfo> zones = TimeZoneInfo.GetSystemTimeZones();
                var zone = zones.Where(p => p.BaseUtcOffset.TotalMinutes == minutes2 * -1).FirstOrDefault();
                var timeOffSet = zone.Id;
                if (timeOffSet == "India Standard Time")
                {
                    RegionInfo ri = new RegionInfo("en-IN");
                    var currency = ri.ISOCurrencySymbol;
                    filterContext.HttpContext.Session.SetString("currency", currency);
                }
                else
                {
                    RegionInfo ri = new RegionInfo("en-US");
                    var currency = ri.ISOCurrencySymbol;
                    filterContext.HttpContext.Session.SetString("currency", currency);
                }
            }
            else
            {
                filterContext.HttpContext.Session.SetString("timezoneoffset", "-330");
                filterContext.HttpContext.Session.SetString("currency", "INR");
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}