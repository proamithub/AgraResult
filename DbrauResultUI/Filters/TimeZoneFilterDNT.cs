//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System;
//using System.Globalization;
//using TimeZoneConverter;

//namespace DbrauResultUI.Helpers
//{
//    //TODO: need to replace from TimeZoneFliter Attribute
//    public class TimeZoneFliterDNT : Attribute, IActionFilter
//    {
//        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            if (filterContext.HttpContext.Request.Cookies["timezoneoffset"] != null)
//            {
//                var timezoneoffset = filterContext.HttpContext.Request.Cookies["timezoneoffset"];
//                var tz = filterContext.HttpContext.Request.Cookies["timezone"];

//                filterContext.HttpContext.Session.SetString("timezoneoffset", timezoneoffset);

//                string timezone = TZConvert.IanaToWindows(tz ?? "Asia/Calcutta");
//                filterContext.HttpContext.Session.SetString("timezone", timezone);

//                if (timezone == "India Standard Time")
//                {
//                    RegionInfo ri = new RegionInfo("en-IN");
//                    var currency = ri.ISOCurrencySymbol;
//                    filterContext.HttpContext.Session.SetString("currency", currency);
//                }
//                else
//                {
//                    RegionInfo ri = new RegionInfo("en-US");
//                    var currency = ri.ISOCurrencySymbol;
//                    filterContext.HttpContext.Session.SetString("currency", currency);
//                }
//            }
//            else
//            {
//                filterContext.HttpContext.Session.SetString("timezoneoffset", "-330");
//                filterContext.HttpContext.Session.SetString("timezone", "India Standard Time");
//                filterContext.HttpContext.Session.SetString("currency", "INR");
//            }
//        }
//        public void OnActionExecuted(ActionExecutedContext context)
//        {

//        }
//    }
//}
