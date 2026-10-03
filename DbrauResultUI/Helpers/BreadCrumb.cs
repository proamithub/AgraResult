using DbrauResultUI.Extensions;
using Microsoft.AspNetCore.Http;

namespace DbrauResultUI.Helpers
{
    public class BreadCrumbHelper
    {
        protected ISession Session;
        public BreadCrumbHelper(IHttpContextAccessor _httpContextAccessor)
        {
            Session = _httpContextAccessor.HttpContext.Session;
        }

        public void Clear()
        {
            Session.SetObject("Prev_Prev", null);
            Session.SetObject("Prev", null);
            Session.SetString("Current", string.Empty);
        }
        public void AddPrev_Prev(string url, string label)
        {
            Session.SetObject("Prev_Prev", new { url = url, label = label });
        }

        public void AddPrev(string url, string label)
        {
            Session.SetObject("Prev", new { url = url, label = label });
        }

        public void SetLabel(string label)
        {
            if (label.Length > 25)
                label = label.Substring(0, 25) + "..";

            Session.SetString("Current", label);
        }
    }
}
