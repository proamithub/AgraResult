using Castle.Core.Internal;
using DbrauResultUI.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RDATA.Entities;
using RModel;
using RModel.DTO;
using RModel.DTO.Result;
using RModel.Enum;
using RRepo;
using RRepo.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DbrauResultUI.Areas.Admin.Controllers
{
    //[Area("Admin")]
    public class DashboardController : BaseController
    {
        int pageSize;
        private IWebHostEnvironment environment;
        public string ImgCloudPath = "";
        private IMailClient _mailClient;
        public DashboardController(IHttpContextAccessor _httpContextAccessor, IConfiguration _config,
             IWebHostEnvironment _environment, IUnitOfWork uow, IMailClient mailClient) : base(_httpContextAccessor, _config, uow)
        {
            environment = _environment;
            string _pageSize = "15";//Convert.ToString(WebConfigSetting.PageSize);
            int PS;
            bool result = Int32.TryParse(_pageSize, out PS);
            pageSize = (result == true) ? PS : 15;
            _mailClient = mailClient;
        }
        [Route("~/admin/dashboard")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StudentMasterDTO> adminData = new List<StudentMasterDTO>();
            //DashboardCount model = await UOF.IAdminMaster.GetHelpDeskCount((int)CurrentUser.UserId);
            return View(adminData);
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(string RollNumber = "")
        //{
        //    bool IsAdmin = false;
        //    if (CurrentUser.Roles.Contains("Admin"))
        //    {
        //        IsAdmin = true;
        //    }
        //    if (!string.IsNullOrEmpty(RollNumber))
        //    { 

        //        List<StudentMasterDTO> adminData = await UOF.IAdminMaster.CheckResult("result", RollNumber.Trim(), IsAdmin);

        //        if (adminData.Count() > 0)
        //        {
        //            return View(adminData);
        //        }
        //    }
        //    return View();
        //}
        //public async Task<IActionResult> CountIndex()
        //{
        //    DashboardCount model = await UOF.IAdminMaster.GetHelpDeskCount((int)CurrentUser.UserId);
        //    return PartialView("_CounterPage", model);
        //}

        //[Route("~/admin/dashboard/checkresultdata")]
        public async Task<IActionResult> CheckResultData(string RollNumber = "")
        {
            bool IsAdmin = false;
            if (CurrentUser.Roles.Contains("Admin") || CurrentUser.Roles.Contains("AdmResult"))
            {
                IsAdmin = true;
            }
            List<StudentMasterDTO> adminData = new List<StudentMasterDTO>();
            //return ViewComponent("PageAdmin", new { RollNumber, IsAdmin });
            if (!string.IsNullOrEmpty(RollNumber))
            {

                adminData = await UOF.IAdminMaster.CheckResult("", RollNumber.Trim(), IsAdmin);

                if (adminData.Count() > 0)
                {
                    // return View("~/areas/admin/views/components/_pageadmin.cshtml", adminData);
                    return PartialView("_PageData", adminData);
                }
            }
            return PartialView("_PageData", adminData);
        }

        public async Task<IActionResult> GetMarksData(string roll, string sem_no, int course_id, string sessionname, string exam_type)
        {
            bool IsAdmin = false;
            if (CurrentUser.Roles.Contains("Admin") || CurrentUser.Roles.Contains("AdmResult"))
            {
                IsAdmin = true;
            }
            List<STUDENT_MARKS_AMDTO> adminData = new List<STUDENT_MARKS_AMDTO>();
            //return ViewComponent("PageAdmin", new { RollNumber, IsAdmin });
            if (!string.IsNullOrEmpty(roll))
            {

                adminData = await UOF.IAdminMaster.MarkAdminDetails("", roll, sem_no, course_id, sessionname, exam_type);

                if (adminData.Count() > 0)
                {
                    // return View("~/areas/admin/views/components/_pageadmin.cshtml", adminData);
                    return PartialView("_MarksData", adminData);
                }
            }
            return PartialView("_MarksData", adminData);
        }

        [HttpGet]
        public async Task<IActionResult> CourseMaster()
        {
            try
            {

                var s= await UOF.IAdminMaster.GetSessionList("SessionList");
                ViewBag.SessionList = s;
                List<CourseMasterDTO_AM> adminData = await UOF.IAdminMaster.CourseMasterAdmin(false,s.First().SessionName);
                return View(adminData);
            }
            catch (Exception)
            {

            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CourseMaster(bool WithCountH = false,string SessionName = "")
        {
            try
            {
                List<CourseMasterDTO_AM> adminData = await UOF.IAdminMaster.CourseMasterAdmin(WithCountH,SessionName);
                ViewBag.SessionList = await UOF.IAdminMaster.GetSessionList("SessionList");
                ViewBag.IsCount = WithCountH;
                ViewBag.SessionName = SessionName;
                return View(adminData);
            }
            catch (Exception)
            {

            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> CSPMaster()
        {
            try
            {
                List<CSP_MASTER_AMDTO> adminData = await UOF.IAdminMaster.CSPMasterList();

                return View(adminData);
            }
            catch (Exception)
            {

            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CourseStatusUpdate([FromForm] string EntryIDs, string IsActive)
        {
            try
            {

                FormResponse data = new FormResponse();

                data = await UOF.IAdminMaster.CourseStatusUpdate(EntryIDs, IsActive);

                if (data.ResponseCode == 1 && data.ResponseMessage == "Yes")
                {
                    return Json(new { res = "ok" });
                }
            }
            catch (Exception e)
            {

            }
            return Json("0");
        }

        [HttpPost]
        public async Task<IActionResult> CourseStatusReset([FromForm] string EntryIDs, string IsActive)
        {
            try
            {

                FormResponse data = new FormResponse();

                data = await UOF.IAdminMaster.CourseStatusReset(EntryIDs, IsActive);

                if (data.ResponseCode == 1 && data.ResponseMessage == "Yes")
                {
                    return Json(new { res = "ok" });
                }
            }
            catch (Exception e)
            {

            }
            return Json("0");
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromForm] CourseMasterDTO_AM Model)
        {
            try
            {

                FormResponse data = new FormResponse();

                data = await UOF.IAdminMaster.AddCourse_AM(Model);


                if (data.ResponseCode == 1 && data.ResponseMessage == "Yes")
                {
                    return Json(new { res = "ok" });
                }
            }
            catch (Exception e)
            {

            }
            return Json("0");
        }
        public async Task<IActionResult> EditCourse(int EntryID = 0)
        {

            if (EntryID > 0)
            {

                CourseMasterDTO_AM adminData = await UOF.IAdminMaster.EditCourse("", EntryID);

                if (adminData.EntryID > 0)
                {
                    var ctlist = await UOF.IAdminMaster.GetCourseTypeList("Type");
                    List<SelectListItem> Select_Listct = new List<SelectListItem>();
                    foreach (var r in ctlist)
                    {
                        SelectListItem obj = new SelectListItem()
                        {
                            Value = r.CourseType.ToString(),
                            Text = r.CourseType,
                            Selected = adminData.CtypeMapList.Where(me => me.CTYPEID.ToString() == r.CtypeID).Count() > 0 ? true : false
                        };

                        Select_Listct.Add(obj);
                    }
                    ViewBag.CtMaster = Select_Listct;
                    // return View("~/areas/admin/views/components/_pageadmin.cshtml", adminData);

                    var TemplateList = await UOF.IAdminMaster.GetTemplateList("ALL");
                    List<SelectListItem> TemplateListselect = new List<SelectListItem>();
                    foreach (var r in TemplateList)
                    {
                        SelectListItem obj = new SelectListItem()
                        {
                            Value = r.TName.ToString(),
                            Text = r.TName,
                            Selected = adminData.TemplateName == r.TName ? true : false
                        };

                        TemplateListselect.Add(obj);
                    }
                    ViewBag.TemplateList = TemplateListselect;

                    return PartialView("_EditCourse", adminData);
                }
            }
            return PartialView("_EditCourse");
        }
        [HttpPost]
        public async Task<IActionResult> EditCourseUpdate([FromForm] CourseMasterDTO_AM Model)
        {
            try
            {

                FormResponse data = new FormResponse();

                data = await UOF.IAdminMaster.UpdateCourse_AM(Model);


                if (data.ResponseCode == 1 && data.ResponseMessage == "Yes")
                {
                    return Json(new { res = "ok" });
                }
            }
            catch (Exception e)
            {

            }
            return Json("0");
        }
        [HttpGet]
        public async Task<IActionResult> StudentList()
        {
            try
            {
                bool IsAdmin = false;
                if (CurrentUser.Roles.Contains("Admin"))
                {
                    IsAdmin = true;
                }
                StudentList_Admin data = new StudentList_Admin();
                data.SessionList = await UOF.IAdminMaster.GetSessionList("SessionList");
                data.CourseTypeList = await UOF.IAdminMaster.GetCourseTypeList("Type");
                data.CourseList = new List<CourseMasterDTO_AM>();
                 data.StudentList = new List<StudentMasterDTO>();
                data.ExamTypeList = new List<ExamTyperDTO_AM>();
                return View(data);
            }
            catch (Exception e)
            {

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StudentList(string SessionName = "", string CourseType = "", bool IsPG = false, int CourseId = 0, string ExamTypeName = "", string CommandNameSrh = null, string RollN = "", int IsLiveStatus = 2)
        {
            try
            {
                bool IsAdmin = false;
                if (CurrentUser.Roles.Contains("Admin"))
                {
                    IsAdmin = true;
                }
                StudentList_Admin data = new StudentList_Admin();
                data.SessionList = await UOF.IAdminMaster.GetSessionList("SessionList");
                data.CourseTypeList = await UOF.IAdminMaster.GetCourseTypeList("Type");
                data.CourseList = await UOF.IAdminMaster.GetCourseList("AllLiveAdminList", CourseType, IsPG,SessionName);

                data.ExamTypeList = await UOF.IAdminMaster.GetExamTypeList_ADMIN("ExamTypeList", CourseId);
                data.StudentList = new List<StudentMasterDTO>();
                if (CommandNameSrh == "Search")
                {
                    data.StudentList = await UOF.IAdminMaster.StudentList_AM("", SessionName, CourseType, IsPG, CourseId, ExamTypeName, RollN, IsLiveStatus, IsAdmin);
                }
                data.CourseType = CourseType;
                data.CourseID = CourseId;
                data.SessionName = SessionName;
                data.ExamTypeName = ExamTypeName;
                data.RollN = RollN;
                data.IsPG = IsPG;
                data.IsLiveStatus = IsLiveStatus;
                return View(data);
            }
            catch (Exception e)
            {

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StudentStatusUpdate([FromForm] string EntryIDs, string IsLiveStatus, String? HeldIn,DateTime? Resultdate,string? SessionName)
        {
            try
            {

                FormResponse data = new FormResponse();

                data = await UOF.IAdminMaster.StudentStatusUpdate(EntryIDs, IsLiveStatus, HeldIn, Resultdate, CurrentUser.UserId, SessionName);

                if (data.ResponseCode == 1 && data.ResponseMessage == "Yes")
                {
                    return Json(new { res = "ok" });
                }
            }
            catch (Exception e)
            {

            }
            return Json("0");
        }

      
        public async Task<IActionResult> ResultViewedReport(string SessionName = "2025-26")
        {
            try
            {
                bool IsAdmin = false;
                if (CurrentUser.Roles.Contains("Admin"))
                {
                    IsAdmin = true;
                }
                StudentList_Admin data = new StudentList_Admin();
               data.ResultViewed = await UOF.IAdminMaster.GetResultViewed("report", SessionName);
                var s = await UOF.IAdminMaster.GetSessionList("SessionList");
                if (s.Count() == 0)
                {

                    SessionDTO ses = new SessionDTO { SessionName = "2025-26" };
                    s.Add(ses);
                }
                ViewBag.SessionList = s;
                ViewBag.SessionName = SessionName;
                return View(data);
            }
            catch (Exception e)
            {

            }
            return View();
        }


        [Route("~/admin/ledger")]
        [HttpGet]
        public async Task<IActionResult> Ledger()
        {
            List<Ledger_AMDTO> adminData = new List<Ledger_AMDTO>();
            adminData = await UOF.IAdminMaster.GetLedgerList("");
            return View(adminData);
        }
        [Route("~/admin/lindex")]
        [HttpGet]
        public async Task<IActionResult> LIndex()
        {
            List<StudentMasterDTO> adminData = new List<StudentMasterDTO>();
            //DashboardCount model = await UOF.IAdminMaster.GetHelpDeskCount((int)CurrentUser.UserId);
            return View(adminData);
        }

        [Route("~/admin/lindex")]
        [HttpPost]
        public async Task<IActionResult> LIndex(Ledger_AMDTO model, string Message)
        {
            try
            {

                if (!string.IsNullOrEmpty(model.InvoiceNo) && model.Amount > 0)
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        List<TblLedgerFile_AM> docfile = new List<TblLedgerFile_AM>();
                        int j = 1;
                        foreach (IFormFile file in Request.Form.Files)
                        {

                            TblLedgerFile_AM d = new TblLedgerFile_AM();
                            var FullPath = "";

                            if ((file != null) && (file.Length > 0) && !string.IsNullOrEmpty(file.FileName))
                            {

                                var filename = ContentDispositionHeaderValue
                                                .Parse(file.ContentDisposition)
                                                .FileName
                                                .Trim('"');
                                var name = ContentDispositionHeaderValue
                                                .Parse(file.ContentDisposition)
                                                .Name
                                                .Trim('"');
                                d.Name = filename;
                                var ext = filename.Substring(filename.LastIndexOf('.'));
                                string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                                string myfile = time + j.ToString() + ext;
                                var path = "D:/DOCUMENTS/HELPDESK_FILE/LEDGER/";
                                FullPath = path + myfile;
                                var uploads = Path.Combine(path);

                                var filePath = Path.Combine(uploads, myfile);
                                string strpath = Path.GetExtension(file.FileName);
                                if (strpath == ".jpg" || strpath == ".jpeg" || strpath == ".pdf" || strpath == ".png")
                                {

                                    if (!Directory.Exists(uploads))
                                    {
                                        Directory.CreateDirectory(uploads);
                                    }

                                    if (System.IO.File.Exists(filePath) == true)
                                    {
                                        System.IO.File.Delete(filePath);
                                    }

                                    using (FileStream fs = System.IO.File.Create(filePath))
                                    {
                                        file.CopyTo(fs);
                                    }
                                    d.FilePath = FullPath.Replace("D:", "");
                                }
                                //  size += file.Length;
                                j++;

                            }

                            docfile.Add(d);



                        }
                        model.FileList = docfile;
                    }
                    model.IpAddress = await Geoloc();
                    model.CreatedBy = (int)CurrentUser.UserId;
                    FormResponse adminData = await UOF.IAdminMaster.SaveLedger(model);
                    if (adminData.ResponseCode == 1)
                    {
                        ViewBag.msg = "ok";
                        return View();
                    }
                }

                return View();
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public async Task<string> Geoloc()
        {
            try
            {
                string _ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                if (_ipAddress.Contains("::1"))
                    _ipAddress = "127.0.0.1";

                return _ipAddress;
            }
            catch (Exception e)
            {

            }
            return string.Empty;
        }
        [HttpPost]
        public async Task<IActionResult> DownloadResultSummary([FromBody] ResultReprtDTO model)
        {


            FileDirectory.GenerateFilePath(
                             destFilePath: AppSettings.GetDestinationFilePath(),
                             GlobalPath: GlobalPath.DownloadReport,
                             FolderPath: string.Empty,
                             FileName: "Download" + model.Flag,
                             FileExtension: "xlsx",
                             FullFilePath: out string fullfilepath,
                             AbsoulteFilePath: out string absoultefilepath,
            ReturnFileName: out string returnfilename);

            DataTable dt1 = await UOF.IAdminMaster.Download_Result_Summary(model);

            //Generate Excel Report
            new Helper().GenerateExcelReport(
                            _filepath: fullfilepath,
                            _sheetName: "Download" + model.Flag,
                            _dt: dt1);
            return Json(new { filename = returnfilename, filepath = absoultefilepath });

        }
        public async Task<IActionResult> DownloadPending([FromForm] PendingReprtDTO model)
        {
            FileDirectory.GenerateFilePath(
                            destFilePath: AppSettings.GetDestinationFilePath(),
                            GlobalPath: GlobalPath.DownloadReport,
                            FolderPath: string.Empty,
                            FileName: "DownloadPending",
                            FileExtension: "xlsx",
                            FullFilePath: out string fullfilepath,
                            AbsoulteFilePath: out string absoultefilepath,
           ReturnFileName: out string returnfilename);

            DataTable dt1 = await UOF.IAdminMaster.DownloadPending(model);

            //Generate Excel Report
            new Helper().GenerateExcelReport(
                            _filepath: fullfilepath,
                            _sheetName: "DownloadPending",
                            _dt: dt1);
            return Json(new { filename = returnfilename, filepath = absoultefilepath });
        }

    }
}
