using RDATA;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbrauResultUI.Helpers;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using RRepo;

namespace DbrauResultUI.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected ISession Session => httpContextAccessor.HttpContext.Session;
        protected BreadCrumbHelper BreadCrumb;
        protected IConfiguration config;
        protected IUnitOfWork UOF;

        public BaseController(IHttpContextAccessor _httpContextAccessor, IConfiguration _config, IUnitOfWork _UOF)
        {
            httpContextAccessor = _httpContextAccessor;
            BreadCrumb = new BreadCrumbHelper(_httpContextAccessor);
            config = _config;
            UOF = _UOF;
            
        }
        protected CustomPrincipal CurrentUser
        {
            get
            {
                if (User.Claims.Count() > 0)
                {
                    string userData = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
                    var user = JsonConvert.DeserializeObject<CustomPrincipal>(userData);
                    return user;
                }
                return null;
            }
        }
        protected async void UpdateTicket(CustomPrincipal member, bool IsPersistent = false)
        {
            string userData = JsonConvert.SerializeObject(member);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.UserData, userData),
                    new Claim(ClaimTypes.Email, member.Email),
                    new Claim(ClaimTypes.Role, member.Roles.ToString()),
                };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties
            {
                IsPersistent = IsPersistent,
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
            });
        }

       
    }
}