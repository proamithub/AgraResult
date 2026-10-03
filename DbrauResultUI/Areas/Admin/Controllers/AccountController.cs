using RModel;
using RRepo;
using RRepo.Utilities;
using DbrauResultUI.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RModel.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using RRepo;

namespace DbrauResultUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected ISession Session => HttpContext.Session;
        private IHostingEnvironment hostingEnv;
        SmsClient sms = new SmsClient();
        int pageSize;
        int pageSizeJobs;
        string ipAddressName="";

        private IMailClient mailClient;
        public string _baseUrl;
        IUnitOfWork UOF;
        public AccountController(IUnitOfWork uow, IHttpContextAccessor _httpContextAccessor, IMailClient _mailClient)
        {
            _baseUrl = WebConfigSetting.BaseURL;
            mailClient = _mailClient;
            httpContextAccessor = _httpContextAccessor;
            UOF = uow;
        }
        public string GetClientIp()
        {
            string _ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            if (_ipAddress.Contains("::1"))
                _ipAddress = "127.0.0.1";

            return _ipAddress;
        }
        public string GetComputerName()
        {
            string _ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            if (_ipAddress.Contains("::1"))
                _ipAddress = "127.0.0.1";
            string computerName = "Unknown";
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(_ipAddress);
                computerName = hostEntry.HostName;
            }
            catch
            {
                // Handle exceptions if needed, e.g., logging
            }


            return computerName;
        }

        [Route("~/admin/login")]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        [Route("~/admin/login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginDTO model, string ReturnUrl)
        {
            try
            {
                // LoginDTO model = JsonConvert.DeserializeObject<LoginDTO>(data.ToString()); && model.OTP.Trim() != ""

                if (model.Email.Trim() != "" && model.Password.Trim() != "")
                {
                    //var otp = Session.GetString("otp").ToString().ToUpper();
                    //if (!string.IsNullOrEmpty(model.OTP) && otp == model.OTP)
                    //{

                    AdminMasterDTO adminData = await UOF.IAdminMaster.AuthenticateAdmin(model.Email, model.Password);
                    if (adminData != null)
                    {
                        if (adminData.IsVerified != true)
                        {
                            return Json("The admin account is not verified. Please verify you account.");
                        }
                        if (adminData.IsActive != true)
                        {
                            return Json("The admin account is de-activated. Please contact website administrator.");

                        }
                        GenerateTicket(adminData, model.IsRemember);
                        if (adminData.Roles.Contains("Staff"))
                        {

                            return Json("Staff");
                        }

                        if (!string.IsNullOrEmpty(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            ipAddressName = GetClientIp() + "_" + GetComputerName();
                            await UOF.IAdminMaster.LoginLog(adminData.AdminId, adminData.Name, ipAddressName);
                            return Json("success");
                        }
                    }
                    else
                    {
                        return Json("Username or Password provided is incorrect");

                    }
                    //}
                    //else
                    //{
                    //    return Json("OTP not match!");
                    //}
                }
                else
                {
                    return Json("Username or Password is not provided!");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }

        }
        private async void GenerateTicket(AdminMasterDTO adminData, bool IsPersistent = true)
        {
            CustomPrincipal serializeModel = new CustomPrincipal();
            serializeModel.UserId = adminData.AdminId;
            serializeModel.Name = adminData.Name;
            serializeModel.MobileNo = adminData.MobileNo;
            serializeModel.Email = adminData.Email;

            serializeModel.Roles = adminData.Roles;
            serializeModel.ProfilePic = adminData.ProfilePic;
            //serializeModel.CtypeList = adminData.CtypeListChoose;

            //serializeModel.ProfilePicDomain = adminData.ProfilePicDomain;
            string userData = JsonConvert.SerializeObject(serializeModel);

            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.UserData, userData),
                            new Claim(ClaimTypes.Email, adminData.Email),
                            new Claim(ClaimTypes.Role, adminData.Roles.ToString()),
                        };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties
            {
                // IsPersistent = IsPersistent,
                AllowRefresh = true,
                // ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
            });
            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity))
            //SetLoginDeviceHistory(Convert.ToInt32(member.MemberId), 3); //3 for member
        }
        [HttpPost]
        [Route("~/admin/LoginMatchOtp")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginMatchOtp([FromBody] LoginDTO model)
        {
            try
            {
                // LoginDTO model = JsonConvert.DeserializeObject<LoginDTO>(data.ToString());

                if (model.Email.Trim() != "" && model.Password.Trim() != "")
                {

                    AdminMasterDTO adminData = await UOF.IAdminMaster.AuthenticateAdmin(model.Email, model.Password);
                    if (adminData != null)
                    {
                        //if (adminData.IsVerified != true)
                        //{
                        //    return Json("The admin account is not verified. Please verifiy you account.");
                        //}
                        //if (adminData.IsActive != true)
                        //{
                        //    return Json("The admin account is de-activated. Please contact website administrator.");

                        //}

                        // GenerateTicket(adminData, model.IsRemember);
                        var otp = "123456";// Utility.GenerateOtp().ToUpper();
                        Session.SetString("otp", otp);

                        LoginDTO data = new LoginDTO();
                        data.Mobile = adminData.MobileNo;
                        data.Email = adminData.Email;
                        data.Name = adminData.Name;
                        //SendVerificationOTP(data, otp);


                        return Json("success");
                    }
                    else
                    {
                        return Json("The Email or Password provided is incorrect");

                    }
                }
                else
                {
                    return Json("The Email or Password is not provided!");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }

        }
        private void SendVerificationOTP(LoginDTO data, string otp)
        {
            ////sms otp
            if (!string.IsNullOrEmpty(data.Mobile))
            {
                string msg = "Dear " + data.Name + ",%3aEnter OTP " + otp + " to verify. %3aUniversity " + WebConfigSetting.univName + "%3a MSSPLV";
                string url = "http://103.16.101.52:8080/bulksms/bulksms?username=mscs-mgkvp&password=mgkvp123&type=0&dlr=1&destination=" + data.Mobile + "&source=MSSPLV&message=" + msg + "&entityid=1601100000000009298&tempid=1207163982498792745";

                url = url.Replace(" ", "%20").Replace(",", "%2C");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
            }
            try
            {
                ///////////////mail otp
                if (!string.IsNullOrEmpty(data.Email))
                {
                    string tmpl = "Dear" + data.Name
                                    + ", <br />"
                                    + "<br />"
                                    + "Your " + WebConfigSetting.univName + " OTP is <strong>" + otp + "</strong>"
                                    + "<br/>";
                    mailClient.SendMail(data.Email, WebConfigSetting.univName + " verification OTP details", tmpl);
                }
            }
            catch (Exception e)
            {


            }

        }
    }
}
