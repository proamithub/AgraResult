using System.Collections.Specialized;

namespace DbrauResultUI.PaymentGateway
{
    public class FormPost
    {
        public NameValueCollection Inputs = new NameValueCollection();
        public string Url = "";
        public void Add(string name, string value)
        {
            Inputs.Add(name, value);
        }

        public void Post()
        {
            //httpContextAccessor.HttpContext.Response.Clear();
            //httpContextAccessor.HttpContext.Response.WriteAsync("<html><head>");
            //httpContextAccessor.HttpContext.Response.WriteAsync(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            //httpContextAccessor.HttpContext.Response.WriteAsync("<style>*{padding:0;margin:0;}</style><div style='border-top: 4px solid #2196F3;margin:0 px;'><center><div style='padding:5px; font-size:12px;text-align:center;width:270px;background-color:#2196F3;border-bottom-left-radius:6px;border-bottom-right-radius:6px;font-family: Arial, Helvetica, sans-serif;color:#fff'>We are processing your request. Please wait...</div></center></div>");
            //httpContextAccessor.HttpContext.Response.WriteAsync(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
            //for (int i = 0; i < Inputs.Keys.Count; i++)
            //{
            //    httpContextAccessor.HttpContext.Response.WriteAsync(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
            //}
            //httpContextAccessor.HttpContext.Response.WriteAsync("</form>");
            //httpContextAccessor.HttpContext.Response.WriteAsync("</body></html>");
            //httpContextAccessor.HttpContext.Response.CompleteAsync();
        }
    }      
}