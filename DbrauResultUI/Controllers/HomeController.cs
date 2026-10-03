using RDATA.Entities;
using RModel;
using RModel.DTO;
using RRepo;
using RRepo.Utilities;
using DbrauResultUI.Helpers;
using DbrauResultUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using Newtonsoft.Json;
using DbrauResultUI.Extensions;
using Microsoft.AspNetCore.Antiforgery;
using System.Globalization;
using Rotativa.AspNetCore.Options;
using Rotativa.AspNetCore;
using QRCoder;
using System.Drawing;
using RModel.DTO.Result;
using Castle.Core.Internal;

namespace DbrauResultUI.Controllers
{
    [CanonicalActionFilter]
    public class HomeController : BaseController
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        SmsClient sms = new SmsClient();
        int pageSize;
        int pageSizeJobs;

        private IMailClient _mailClient;
        public HomeController(IConfiguration _config, IHttpContextAccessor _httpContextAccessor, IUnitOfWork uow, IMailClient mailClient) : base(_httpContextAccessor, _config, uow)
        {
            httpContextAccessor = _httpContextAccessor;
            _mailClient = mailClient;
        }

        public async Task<IActionResult> Index()
        {
            //ResultDTO data = new ResultDTO();
            //data.CourseTypeList = await UOF.IAdminMaster.GetCourseTypeList("Type");
            //data.CourseList = await UOF.IAdminMaster.GetCourseList("Type", "", false,"");
            //data.CourseAllList = await UOF.IAdminMaster.GetCourseList("AllLiveList", "", null,"");
            //data.ExamTypeList = new List<ExamTyperDTO_AM>();
            ////await UOF.IAdminMaster.GetExamTypeList("ExamTypeList", "");
            //data.SessionList = await UOF.IAdminMaster.GetSessionList("SessionList");
            //data.HeldinList = new List<HeldinDTO>();
            ResultDTO_DASH data = await UOF.IAdminMaster.GetResultDTO_DASH("", "", false, "", "");
            data.CourseType = "Affiliated College";
            //data.CType = Ctype;
            return View(data);
        }
        [HttpPost]
        //public async Task<IActionResult> Index(string CourseType = "", bool IsPG = false, string CourseName = "",string ExamTypeName="",string SessionName="")
        //{
        public async Task<IActionResult> Index(string CourseType = "", bool? IsPG = false, string? CourseName = "", string ExamTypeName = "", string SessionName = "")
        {
            //ResultDTO data = new ResultDTO();


            //data.CourseTypeList = await UOF.IAdminMaster.GetCourseTypeList("Type");
            //data.CourseList = await UOF.IAdminMaster.GetCourseList("Type", CourseType, IsPG, SessionName);
            //data.CourseAllList = await UOF.IAdminMaster.GetCourseList("AllLiveList", CourseType, IsPG, SessionName);
            //data.ExamTypeList = await UOF.IAdminMaster.GetExamTypeList("ExamTypeList", CourseName, SessionName);
            //data.SessionList = await UOF.IAdminMaster.GetSessionList("SessionList");
            //if (!string.IsNullOrEmpty(ExamTypeName))
            //{
            //    data.HeldinList = await UOF.IAdminMaster.GetHeldinList("AlList", CourseType, CourseName, ExamTypeName, SessionName);
            //}
            //else {
            //    data.HeldinList = new List<HeldinDTO>();
            //}

            //data.CourseType = CourseType;
            //data.CourseName = CourseName; 
            //data.ExamTypeName = ExamTypeName; 
            //data.SessionName = SessionName;
            //data.IsPG = IsPG;
            ResultDTO_DASH data = await UOF.IAdminMaster.GetResultDTO_DASH("", CourseType, IsPG, CourseName, SessionName);


            data.CourseType = CourseType;
            //data.CourseID = data.CourseID;
            data.CourseName = CourseName;
            data.ExamTypeName = ExamTypeName;
            data.SessionName = SessionName;
            data.IsPG = IsPG;
            return View(data);
        }
        public IActionResult SignIn()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<ActionResult> SignOut()
        {
            if (CurrentUser != null)
            {
                if (CurrentUser.Roles.Contains("Student") == true)
                {
                    string[] Roles = CurrentUser != null ? CurrentUser.Roles : new string[] { "" };

                    await httpContextAccessor.HttpContext.SignOutAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string[] Roles = CurrentUser != null ? CurrentUser.Roles : new string[] { "" };

                    await httpContextAccessor.HttpContext.SignOutAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    return RedirectToAction("Login", "Account", new { Area = "Admin" });

                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SearchResult([FromBody] SearchDTO model)
        {
            try
            {
                Session.SetObject("resultdata", model);

                if (model.RollNumber.Trim() != "")
                {
                    if (string.IsNullOrEmpty(model.FName))
                    {
                        var datet = Convert.ToDateTime(model.DOBstr, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                        model.DOB = datet;
                    }

                    var adminData = await UOF.IAdminMaster.CheckStudentData(model);
                    if (adminData.ROLL_NO == model.RollNumber)
                    {
                        adminData.EncrptedRoll = AESEncription.Base64Encode(adminData.ROLL_NO);


                        return Json(new { data = adminData, res = "success" });
                    }

                }
                else
                {
                    return Json("0");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }
            return Json("Internal error!");
        }

        [HttpPost]
        public async Task<IActionResult> VisitCountSet([FromBody] StudentMasterDTO model)
        {
            try
            {
                if (model.ROLL_NO.Trim() != "")
                {
                    var FormResponse = await UOF.IAdminMaster.VisitCountSet(model);
                    if (FormResponse.ResponseCode == 1)
                    {
                        return Json(new { res = "success" });
                    }

                }
                else
                {
                    return Json("0");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }
            return Json("Internal error!");
        }


        [Route("~/result/{data}")]
        public async Task<IActionResult> Result(string data)
        {
            try
            {

                var s = Session.GetObject<SearchDTO>("resultdata");
                var roll = AESEncription.Base64Decode(data);
                if (roll == s.RollNumber)
                {
                    StudentMasterDTO adminData = new StudentMasterDTO();
                    adminData.ROLL_NO = s.RollNumber;
                    if (string.IsNullOrEmpty(s.FName))
                    {
                        var datet = Convert.ToDateTime(s.DOBstr, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                        adminData.DOB = datet;
                    }

                    //StudentMasterDTO adminData = await UOF.IAdminMaster.ResultStudentData("Search",s);
                    //if (roll == adminData.ROLL_NO)
                    //{
                    //adminData.MarksList = await UOF.IAdminMaster.ResultMarksStudentData(s);
                    //adminData.ResultList = await UOF.IAdminMaster.ResultDataStudentData(s);
                    //adminData.CSPList = await UOF.IAdminMaster.CSVDataStudentData(adminData);
                    adminData.EncrptedRoll = data;
                    return View(adminData);
                    //return new ViewAsPdf("Result", adminData)
                    //{
                    //    FileName = adminData.ROLL_NO + "_" + "Result.pdf",
                    //    //PageMargins = new Margins(10, 1, 0, 0),
                    //    PageOrientation = Orientation.Portrait,
                    //};
                    //}
                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Home");
        }

        [Route("~/resultpreview/{data}")]
        public async Task<IActionResult> ResultPreview(string data)
        {
            try
            {

                var roll = AESEncription.Base64Decode(data);
                var s = roll.Split("$");


                SearchDTO model = new SearchDTO();
                model.CourseName = s[0].ToString();
                model.RollNumber = s[1].ToString();
                model.CourseType = s[2].ToString();
                model.ExamTypeName = s[3].ToString();
                model.SessionName = s[4].ToString();
                model.HELD_IN = s[5].ToString();
                if (s.Length > 6)
                {
                    model.IsAdmin = s[6].ToString();
                }
                else
                {
                    model.IsAdmin = "0";
                }
              
                model.FName = "dsa";
                if (!string.IsNullOrEmpty(model.RollNumber))
                {
                    Session.SetObject("resultdata", model);
                    data = AESEncription.Base64Encode(model.RollNumber);
                    return RedirectToAction("Result", "Home", new { data = data });
                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Home");
        }
        //[Route("~/result/{data}")]
        public async Task<IActionResult> ResultDownload_t1(string data)
        {
            try
            {

                var s = Session.GetObject<SearchDTO>("resultdata");
                var roll = AESEncription.Base64Decode(data);
                if (roll == s.RollNumber)
                {
                    StudentMasterDTO adminData = await UOF.IAdminMaster.ResultStudentData("", s);
                    if (roll == adminData.ROLL_NO)
                    {
                        adminData.MarksList = await UOF.IAdminMaster.ResultMarksStudentData(s);
                        adminData.ResultList = await UOF.IAdminMaster.ResultDataStudentData(s);
                        adminData.CSPList = await UOF.IAdminMaster.CSVDataStudentData(adminData);
                        adminData.EncrptedRoll = data;

                        var data1 = adminData.CourseName + "$" + s.RollNumber + "$" + s.CourseType + "$" + s.ExamTypeName + "$" + s.SessionName + "$" + s.HELD_IN;
                        data1 = AESEncription.Base64Encode(data1);
                        var UriPayload = "https://result2024.agrauniv.online/resultpreview/" + data1;
                        QRCodeGenerator _qrCode = new QRCodeGenerator();
                        QRCodeData _qrCodeData = _qrCode.CreateQrCode(UriPayload, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(_qrCodeData);
                        Bitmap qrCodeImage = qrCode.GetGraphic(20);
                        adminData.qcore = BitmapToBytesCode(qrCodeImage);



                        return new ViewAsPdf("ResultDownload_t1", adminData)
                        {
                            FileName = adminData.ROLL_NO + "_" + "Result.pdf",
                            //PageMargins = new Margins(10, 1, 0, 0),
                            PageOrientation = Orientation.Portrait,
                        };
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Home");
        }
        [NonAction]
        private static Byte[] BitmapToBytesCode(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        public async Task<IActionResult> ResultDownload_t2(string data)
        {
            try
            {

                var s = Session.GetObject<SearchDTO>("resultdata");
                var roll = AESEncription.Base64Decode(data);
                if (roll == s.RollNumber)
                {
                    StudentMasterDTO adminData = await UOF.IAdminMaster.ResultStudentData("", s);
                    if (roll == adminData.ROLL_NO)
                    {
                        adminData.MarksList = await UOF.IAdminMaster.ResultMarksStudentData(s);
                        adminData.ResultList = await UOF.IAdminMaster.ResultDataStudentData(s); adminData.CSPList = await UOF.IAdminMaster.CSVDataStudentData(adminData);
                        //  return View(adminData);
                        return new ViewAsPdf("ResultDownload_t2", adminData)
                        {
                            FileName = adminData.ROLL_NO + "_" + "Result.pdf",
                            //PageMargins = new Margins(10, 1, 0, 0),
                            PageOrientation = Orientation.Portrait,
                        };
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ResultDownload_t3(string data)
        {
            try
            {

                var s = Session.GetObject<SearchDTO>("resultdata");
                var roll = AESEncription.Base64Decode(data);
                if (roll == s.RollNumber)
                {
                    StudentMasterDTO adminData = await UOF.IAdminMaster.ResultStudentData("", s);
                    if (roll == adminData.ROLL_NO)
                    {
                        adminData.MarksList = await UOF.IAdminMaster.ResultMarksStudentData(s);
                        adminData.ResultList = await UOF.IAdminMaster.ResultDataStudentData(s); adminData.CSPList = await UOF.IAdminMaster.CSVDataStudentData(adminData);
                        //  return View(adminData);
                        return new ViewAsPdf("ResultDownload_t3", adminData)
                        {
                            FileName = adminData.ROLL_NO + "_" + "Result.pdf",
                            //PageMargins = new Margins(10, 1, 0, 0),
                            PageOrientation = Orientation.Portrait,
                        };
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DataSync()
        {
            try
            {
                CommonResultDataDTO d = new CommonResultDataDTO();

                d.StudentMaster = await UOF.IAdminMaster.GETDeLLServerData();
                //if (!string.IsNullOrEmpty(adminData))
                //{
                //    adminData.MarksList = await UOF.IAdminMaster.ResultMarksStudentData(s);
                //}



            }
            catch (Exception ex)
            {

            }
            return Json("ok");
        }
        [HttpGet]

        public async Task<IActionResult> CheckResult(string RollNumber = "")
        {
            bool IsAdmin = false;
            if (CurrentUser.Roles.Contains("Admin"))
            {
                IsAdmin = true;
            }
            return ViewComponent("PageAdmin", new { RollNumber, IsAdmin });
        }

        
        [Route("~/result_consolidated_gradesheet/{RollNumber}")]
        public async Task<IActionResult> CONSOLIDATED_GRADESHEET(string RollNumber = "")
        {
            bool IsAdmin = false;
            var roll = AESEncription.Base64Encode(RollNumber);
            return RedirectToAction("CONSOLIDATED_GRADESHEET_DETAIL", "Home", new { ERollNumber = roll });            
        }
        
        [Route("~/result_consolidated_gradesheet_detail/{ERollNumber}")]
        public async Task<IActionResult> CONSOLIDATED_GRADESHEET_DETAIL(string ERollNumber = "")
        {
            bool IsAdmin = false;
            var RollNumber = AESEncription.Base64Decode(ERollNumber);
            SearchDTO s = new SearchDTO();
            s.RollNumber = RollNumber;

            //if (CurrentUser.Roles.Contains("Admin"))
            //{
            //    IsAdmin = true;
            //}
            if (!string.IsNullOrEmpty(RollNumber))
            {
                StudentMasterERPDTO adminData = await UOF.IAdminMaster.ResultStudentERPData("Result", s);
                if (RollNumber == adminData.RollNo1)
                {
                    var UriPayload = "https://result2024.agrauniv.online/result_consolidated_gradesheet_detail/" + ERollNumber;
                    QRCodeGenerator _qrCode = new QRCodeGenerator();
                    QRCodeData _qrCodeData = _qrCode.CreateQrCode(UriPayload, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(_qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20);
                    adminData.qcore = BitmapToBytesCode(qrCodeImage);

                    return View(adminData);
                }
            }
            return View();
        }
       

    }
}