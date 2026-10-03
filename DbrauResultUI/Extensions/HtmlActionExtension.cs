

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DbrauResultUI.Extensions
{
    public static class HtmlHelperExtension
    {
        public static IHtmlContent BreadCrumb(this IHtmlHelper helper, string cssClass = "breadcrumb")
        {
            StringBuilder breadcrumb = new StringBuilder("<ol itemscope itemtype='http://schema.org/BreadcrumbList' class='" + cssClass + "' ><li itemprop='itemListElement' itemscope itemtype='http://schema.org/ListItem'><a itemprop='item' href='/'><span itemprop='name'>Home</span><meta itemprop='position' content='1' /></a></li>");

            bool Prev_Prev = false;
            //for previous-previous link
            if (helper.ViewContext.HttpContext.Session.GetObject<dynamic>("Prev_Prev") != null)
            {
                Prev_Prev = true;
                breadcrumb.Append("<li itemprop='itemListElement' itemscope itemtype='http://schema.org/ListItem'>");
                dynamic obj = helper.ViewContext.HttpContext.Session.GetObject<dynamic>("Prev_Prev");

                breadcrumb.Append("<a itemprop='item' href='" + obj.url + "'><span itemprop='name'>" + obj.label + "</span><meta itemprop='position' content='2' /></a>");
                breadcrumb.Append("</li>");
            }

            //for previous link
            if (helper.ViewContext.HttpContext.Session.GetObject<dynamic>("Prev") != null)
            {
                breadcrumb.Append("<li itemprop='itemListElement' itemscope itemtype='http://schema.org/ListItem'>");
                dynamic obj = helper.ViewContext.HttpContext.Session.GetObject<dynamic>("Prev");

                if (Prev_Prev)
                    breadcrumb.Append("<a itemprop='item' href='" + obj.url + "'><span itemprop='name'>" + obj.label + "</span><meta itemprop='position' content='3' /></a>");
                else
                    breadcrumb.Append("<a itemprop='item' href='" + obj.url + "'><span itemprop='name'>" + obj.label + "</span><meta itemprop='position' content='2' /></a>");

                breadcrumb.Append("</li>");
            }

            //for current action and controller
            breadcrumb.Append("<li class='active' itemprop='itemListElement' itemscope itemtype='http://schema.org/ListItem'>");
            string linkText = "<span itemprop='name'>";

            if (!String.IsNullOrEmpty(helper.ViewContext.HttpContext.Session.GetString("Current")))
            {
                linkText += helper.ViewContext.HttpContext.Session.GetString("Current").TitleCase();
            }
            else
            {
                linkText += helper.ViewContext.RouteData.Values["action"].ToString().TitleCase();
            }
            if (Prev_Prev)
                linkText += "</span><meta itemprop='position' content='4' />";
            else
                linkText += "</span><meta itemprop='position' content='3' />";

            breadcrumb.Append(linkText);
            breadcrumb.Append("</li>");

            return new HtmlString(breadcrumb.Append("</ol>").ToString());
        }
        public static IHtmlContent BreadCrumbMember(this IHtmlHelper helper, string cssClass = "breadcrumb")
        {
            if (helper.ViewContext.RouteData.Values["action"].ToString() == "Index" && helper.ViewContext.RouteData.Values["controller"].ToString() == "Dashboard")
            {
                return null;
            }
            StringBuilder breadcrumb = new StringBuilder("<ol class='" + cssClass + "'><li><a href='/member'>Dashboard</a></li>");

            //for previous-previous link
            if (helper.ViewContext.HttpContext.Session.GetObject<dynamic>("Prev_Prev") != null)
            {
                breadcrumb.Append("<li>");
                dynamic obj = helper.ViewContext.HttpContext.Session.GetObject<dynamic>("Prev_Prev");

                breadcrumb.Append("<a href='" + obj.url + "'>" + obj.label + "</a>");
                breadcrumb.Append("</li>");
            }

            //for previous link
            if (helper.ViewContext.HttpContext.Session.GetObject<dynamic>("Prev") != null)
            {
                breadcrumb.Append("<li>");
                dynamic obj = helper.ViewContext.HttpContext.Session.GetObject<dynamic>("Prev");

                breadcrumb.Append("<a href='" + obj.url + "'>" + obj.label + "</a>");
                breadcrumb.Append("</li>");
            }

            //for current action and controller
            breadcrumb.Append("<li class='active'>");
            string linkText = "";

            if (!String.IsNullOrEmpty(helper.ViewContext.HttpContext.Session.GetString("Current")))
            {
                linkText = helper.ViewContext.HttpContext.Session.GetString("Current").TitleCase();
            }
            else
            {
                linkText = helper.ViewContext.RouteData.Values["action"].ToString().TitleCase();
            }

            breadcrumb.Append(linkText);
            breadcrumb.Append("</li>");

            return new HtmlString(breadcrumb.Append("</ol>").ToString());
        }
        public static IHtmlContent Action(this IHtmlHelper helper, string action, object parameters = null)
        {
            var controller = (string)helper.ViewContext.RouteData.Values["controller"];

            return Action(helper, action, controller, parameters);
        }

        public static IHtmlContent Action(this IHtmlHelper helper, string action, string controller, object parameters = null)
        {
            var area = (string)helper.ViewContext.RouteData.Values["area"];

            return Action(helper, action, controller, area, parameters);
        }

        public static IHtmlContent Action(this IHtmlHelper helper, string action, string controller, string area, object parameters = null)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            if (controller == null)
                throw new ArgumentNullException("controller");


            var task = RenderActionAsync(helper, action, controller, area, parameters);

            return task.Result;
        }

        private static async Task<IHtmlContent> RenderActionAsync(this IHtmlHelper helper, string action, string controller, string area, object parameters = null)
        {
            // fetching required services for invocation
            var serviceProvider = helper.ViewContext.HttpContext.RequestServices;
            var actionContextAccessor = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<IActionContextAccessor>();
            var httpContextAccessor = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
            var actionSelector = serviceProvider.GetRequiredService<IActionSelector>();

            // creating new action invocation context
            var routeData = new RouteData();
            foreach (var router in helper.ViewContext.RouteData.Routers)
            {
                routeData.PushState(router, null, null);
            }
            routeData.PushState(null, new RouteValueDictionary(new { controller = controller, action = action, area = area }), null);
            routeData.PushState(null, new RouteValueDictionary(parameters ?? new { }), null);

            //get the actiondescriptor
            RouteContext routeContext = new RouteContext(helper.ViewContext.HttpContext) { RouteData = routeData };
            var candidates = actionSelector.SelectCandidates(routeContext);
            var actionDescriptor = actionSelector.SelectBestCandidate(routeContext, candidates);

            var originalActionContext = actionContextAccessor.ActionContext;
            var originalhttpContext = httpContextAccessor.HttpContext;
            try
            {
                var newHttpContext = serviceProvider.GetRequiredService<IHttpContextFactory>().Create(helper.ViewContext.HttpContext.Features);
                if (newHttpContext.Items.ContainsKey(typeof(IUrlHelper)))
                {
                    newHttpContext.Items.Remove(typeof(IUrlHelper));
                }
                newHttpContext.Response.Body = new MemoryStream();
                var actionContext = new ActionContext(newHttpContext, routeData, actionDescriptor);
                actionContextAccessor.ActionContext = actionContext;
                var invoker = serviceProvider.GetRequiredService<IActionInvokerFactory>().CreateInvoker(actionContext);
                await invoker.InvokeAsync();
                newHttpContext.Response.Body.Position = 0;
                using (var reader = new StreamReader(newHttpContext.Response.Body))
                {
                    return new HtmlString(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                return new HtmlString(ex.Message);
            }
            finally
            {
                actionContextAccessor.ActionContext = originalActionContext;
                httpContextAccessor.HttpContext = originalhttpContext;
                if (helper.ViewContext.HttpContext.Items.ContainsKey(typeof(IUrlHelper)))
                {
                    helper.ViewContext.HttpContext.Items.Remove(typeof(IUrlHelper));
                }
            }
        }
    }
}
