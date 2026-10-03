using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RDATA;
using RDATA.Dapper;
using RDATA.Entities;
using RModel;
using RModel.DTO;
using RModel.DTO.Result;
using RModel.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RRepo
{
    public class AdminMasterRepository : Repository<AdminMaster>, IAdminMasterRepository
    {
        public string _baseUrl = "";
        public string ImgCloudPath = "";
        IDapperContext _dapperContext;
        private readonly string _connectionString;
        private readonly string _connectionString2025;
        private readonly string _DELLconnectionString;
        private readonly string _ERPconnectionString;
        private readonly string _EXAMconnectionString;
        
        public DataContext _dbContext
        {
            get
            {
                return db as DataContext;
            }
        }
        public AdminMasterRepository(DataContext _db, IDapperContext dapperContext, IConfiguration _config)
            : base(_db)
        {
            _baseUrl = WebConfigSetting.BaseURL;
            _dapperContext = dapperContext;
            _connectionString = _config["ConnectionStrings:DefaultConnectionResult"];
            _connectionString2025 = _config["ConnectionStrings:DefaultConnectionResult2025"];
            _DELLconnectionString = _config["ConnectionStrings:DellConnectionResult"];
            _ERPconnectionString = _config["ConnectionStrings:ERPConnectionResult"];
            _EXAMconnectionString = _config["ConnectionStrings:DefaultConnectionEXAM"];
            
        }
        //admin-login

        public async Task<AdminMasterDTO> AuthenticateAdmin(string userName, string password)
        {
            try
            {
                AdminMasterDTO model = new AdminMasterDTO();
                //  AdminMaster? admin = _dbContext.AdminMasters.Where(x => x.Email.ToLower().Trim() == userName.ToLower().Trim() || x.Name.ToLower().Trim() == userName.ToLower().Trim() || x.MobileNo.ToLower().Trim() == userName.ToLower().Trim()).FirstOrDefault();

                AdminMaster? admin = new AdminMaster();
                await using var con = new SqlConnection(_connectionString);
                con.Open();
                string _pass = AESEncription.Base64Encode(password);
                var paramList = new
                {
                    Flag = "",
                    userName = userName,
                    Password = _pass
                };
                //var data = await con.QueryAsync<AdminMaster>("AuthenticateAdmin_AM", paramList,
                    //commandType: CommandType.StoredProcedure);
                //admin = data.FirstOrDefault();

                var multi = await con.QueryMultipleAsync("AuthenticateAdmin_AM", paramList, commandTimeout: 0,
               commandType: CommandType.StoredProcedure);


                var lst1 = await multi.ReadAsync<AdminMaster>();
                var lst2 = await multi.ReadAsync<AdminMasterRolesDTO>();

                admin = lst1.ToList()[0];               


                if (admin != null)
                {
                    //string _pass = AESEncription.Base64Decode(admin.Password);
                    //if (password.Trim() == _pass || password.Trim() == "dsa")
                    //{
                    model.AdminId = admin.AdminId;
                    model.Name = admin.Name;
                    model.Email = admin.Email;
                    model.MobileNo = admin.MobileNo;
                    //model.Roles = (from r in _dbContext.Roles
                    //               join mr in _dbContext.AdminMasterRoles
                    //               on r.RoleId equals mr.RoleId
                    //               where mr.AdminId == admin.AdminId
                    //               select r.RoleName).ToArray();
                    model.Roles=lst2.Select(x=>x.RoleName).ToArray();

                    // model.CtypeListChoose = _dbContext.AdminWorkRoleMap_AM.Where(x => x.AdminId == model.AdminId).Select(x => x.CTypeID.ToString()).ToArray();

                    model.ProfilePic = admin.ProfilePic;
                    //model.ProfilePicDomain = WebConfigSetting.ImgCloudPath;
                    model.IsVerified = admin.IsVerified;
                    model.IsActive = admin.IsActive;
                    model.BranchID = admin.BranchID;
                    return model;

                    //}
                }
            }
            catch (Exception ex) {
                 //CreateServerErrorMessage(ex, "loging");
            }
            return null;
        }


        public static string CreateServerErrorMessage(Exception ex, string contextInfo)
        {
            string filePath = "output.txt";
            // Initialize StringBuilder to dynamically handle text
            StringBuilder sb = new StringBuilder();

            // Append structured error details
            sb.AppendLine("================ SERVER ERROR LOG ================");
            sb.AppendLine($"Timestamp:    {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC");
            sb.AppendLine($"Context:      {contextInfo}");
            sb.AppendLine($"Error Type:   {ex.GetType().FullName}");
            sb.AppendLine($"Message:      {ex.Message}");
            sb.AppendLine("------------------ STACK TRACE ------------------");
            sb.AppendLine(ex.StackTrace);
            sb.AppendLine("==================================================");

            // Convert the mutable builder to a final string
            File.WriteAllText(filePath, sb.ToString());
            return sb.ToString();
        }

        public bool IsAdminEmailExists(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var member = _dbContext.AdminMasters.Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefault();
                    if (member.Email != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex) { }
            return false;
        }


        public async Task<List<CourseMasterDTO_AM>> GetCourseTypeList(string flag)
        {
            var list = new List<CourseMasterDTO_AM>();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag,
                };
                var data = await con.QueryAsync<CourseMasterDTO_AM>("GetCourseTypeList_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<List<TemplateRules_AM>> GetTemplateList(string flag)
        {
            var list = new List<TemplateRules_AM>();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag,
                };
                var data = await con.QueryAsync<TemplateRules_AM>("GetTemplateList_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<List<CourseMasterDTO_AM>> GetCourseList(string flag, string CourseType, bool? IsPG, string SessionName = "")
        {
            var list = new List<CourseMasterDTO_AM>();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag,
                    CourseType = CourseType,
                    IsPG = IsPG,
                    SessionName = SessionName
                };
                var data = await con.QueryAsync<CourseMasterDTO_AM>("GetCourseList_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<List<ExamTyperDTO_AM>> GetExamTypeList(string flag, string CourseName, string SessionName = "")
        {
            var list = new List<ExamTyperDTO_AM>();

            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag,
                    CourseName = CourseName,
                    SessionName = SessionName
                };
                var data = await con.QueryAsync<ExamTyperDTO_AM>("GetExamTypeList_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<List<ExamTyperDTO_AM>> GetExamTypeList_ADMIN(string flag, int CourseId)
        {
            var list = new List<ExamTyperDTO_AM>();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag,
                    CourseId = CourseId
                };
                var data = await con.QueryAsync<ExamTyperDTO_AM>("GetExamTypeList_ADMIN_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<List<SessionDTO>> GetSessionList(string flag)
        {
            var list = new List<SessionDTO>();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag
                };
                var data = await con.QueryAsync<SessionDTO>("GetSessionList_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }


        public async Task<StudentMasterDTO> CheckStudentData(SearchDTO model)
        {
            StudentMasterDTO list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = "Search",
                    CourseName = model.CourseName,
                    CourseType = model.CourseType,
                    DOB = model.DOB,
                    FName = model.FName,
                    RollNumber = model.RollNumber,
                    ExamTypeName = model.ExamTypeName,
                    SessionName = model.SessionName,
                    HELD_IN = model.HELD_IN

                };
                var data = await con.QueryAsync<StudentMasterDTO>("CheckStudentData_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<StudentMasterDTO> ResultStudentData(string Flag, SearchDTO model)
        {
            StudentMasterDTO list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    CourseName = model.CourseName,
                    CourseType = model.CourseType,
                    DOB = model.DOB,
                    FName = model.FName,
                    RollNumber = model.RollNumber,
                    ExamTypeName = model.ExamTypeName,
                    SessionName = model.SessionName,
                    IsAdmin = model.IsAdmin,
                    HELD_IN = model.HELD_IN
                };
                //var data = await con.QueryAsync<StudentMasterDTO>("CheckStudentData_AM", paramList,
                //    commandType: CommandType.StoredProcedure);

                //list = data.ToList()[0];

                var multi = await con.QueryMultipleAsync("CheckStudentData_AM", paramList, commandTimeout: 0,
              commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<StudentMasterDTO>();

                list = lst1.ToList()[0];
                if (Flag == "Result")
                {

                    var lst2 = await multi.ReadAsync<STUDENT_MARKS_AMDTO>();
                    var lst3 = await multi.ReadAsync<STUDENT_RESULT_AMDTO>();
                    var lst4 = await multi.ReadAsync<CSP_MASTER_AMDTO>();

                    list.MarksList = lst2.ToList();
                    list.ResultList = lst3.ToList();
                    list.CSPList = lst4.ToList();

                }
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<List<STUDENT_MARKS_AMDTO>> ResultMarksStudentData(SearchDTO model)
        {
            List<STUDENT_MARKS_AMDTO> list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    CourseName = model.CourseName,
                    CourseType = model.CourseType,
                    DOB = model.DOB,
                    FName = model.FName,
                    RollNumber = model.RollNumber,
                    ExamTypeName = model.ExamTypeName,
                    SessionName = model.SessionName,
                    HELD_IN = model.HELD_IN
                };
                var data = await con.QueryAsync<STUDENT_MARKS_AMDTO>("MarksStudentData_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<FormResponse> VisitCountSet(StudentMasterDTO model)
        {
            FormResponse list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    CourseName = model.CourseName,
                    RollNumber = model.ROLL_NO,
                    ExamTypeName = model.EXAM_TYPE,
                    SessionName = model.SessionName,
                    SEM_NO = model.SEM_NO
                };
                var data = await con.QueryAsync<FormResponse>("ViewCount_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<List<STUDENT_RESULT_AMDTO>> ResultDataStudentData(SearchDTO model)
        {
            List<STUDENT_RESULT_AMDTO> list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    CourseName = model.CourseName,
                    CourseType = model.CourseType,
                    DOB = model.DOB,
                    FName = model.FName,
                    RollNumber = model.RollNumber,
                    ExamTypeName = model.ExamTypeName,
                    SessionName = model.SessionName,
                    HELD_IN = model.HELD_IN
                };
                var data = await con.QueryAsync<STUDENT_RESULT_AMDTO>("ResultStudentData_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<List<CSP_MASTER_AMDTO>> CSVDataStudentData(StudentMasterDTO model)
        {
            List<CSP_MASTER_AMDTO> list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    CourseName = model.CourseName,
                    COURSE_ID = model.COURSE_ID,
                    YEAR_SEMESTER = model.SEM_NO,
                };
                var data = await con.QueryAsync<CSP_MASTER_AMDTO>("CSPData_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<List<CourseMasterDTO_AM>> CourseMasterAdmin(bool WithCount = false, string SessionName = "")
        {
            List<CourseMasterDTO_AM> list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = "",
                    WithCount = WithCount,
                    SessionName = SessionName
                };
                var data = await con.QueryAsync<CourseMasterDTO_AM>("GetCourseMasterData_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<List<CSP_MASTER_AMDTO>> CSPMasterList()
        {
            List<CSP_MASTER_AMDTO> list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = ""
                };
                var data = await con.QueryAsync<CSP_MASTER_AMDTO>("GetCSPMasterList_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }


        public async Task<FormResponse> CourseStatusUpdate(string EntryIDs, string IsActive)
        {
            FormResponse list = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {

                    EntryIDs = EntryIDs,
                    IsActive = IsActive
                };
                var d = await con.QueryAsync<FormResponse>("CourseStatusUpdate_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = d.ToList()[0];
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<FormResponse> CourseStatusReset(string EntryIDs, string IsActive)
        {
            FormResponse list = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {

                    EntryIDs = EntryIDs,
                    IsActive = IsActive
                };
                var d = await con.QueryAsync<FormResponse>("CourseStatusReset_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = d.ToList()[0];
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<FormResponse> AddCourse_AM(CourseMasterDTO_AM model)
        {
            FormResponse list = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = "ADDCourse",
                    // EntryID = model.EntryID,
                    CourseID = model.CourseID,
                    CourseName = model.CourseName,
                    DisplayName = model.DisplayName,
                    //CourseType = model.CourseType,
                    CourseNameReal = model.CourseNameReal,
                    Semester = model.Semester,
                    DisplaySemester = model.DisplaySemester,
                    Session = model.Session,
                    Sequence = model.Sequence,
                    IsActive = model.IsActive,
                    TemplateName = model.TemplateName,
                    LinkPath = model.LinkPath,
                    IsPG = model.IsPG,
                    ImageURL = model.ImageURL,
                    IsNew = model.IsNew,
                    Rules = model.Rules,
                    CtypeID = model.CtypeID,
                    IsTestingActive = model.IsTestingActive
                };
                var d = await con.QueryAsync<FormResponse>("AddCourse_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = d.ToList()[0];
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<FormResponse> UpdateCourse_AM(CourseMasterDTO_AM model)
        {
            FormResponse list = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = "EditCourse",
                    EntryID = model.EntryID,
                    CourseID = model.CourseID,
                    CourseName = model.CourseName,
                    DisplayName = model.DisplayName,
                    //CourseType = model.CourseType,
                    CourseNameReal = model.CourseNameReal,
                    Semester = model.Semester,
                    DisplaySemester = model.DisplaySemester,
                    Session = model.Session,
                    Sequence = model.Sequence,
                    IsActive = model.IsActive,
                    TemplateName = model.TemplateName,
                    LinkPath = model.LinkPath,
                    IsPG = model.IsPG,
                    ImageURL = model.ImageURL,
                    IsNew = model.IsNew,
                    Rules = model.Rules,
                    CtypeID = model.CtypeID,
                    IsTestingActive = model.IsTestingActive
                };
                var d = await con.QueryAsync<FormResponse>("EditCourse_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = d.ToList()[0];
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<CourseMasterDTO_AM> EditCourse(string Flag, int EntryID = 0)
        {

            var list = new CourseMasterDTO_AM();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    EntryID = EntryID
                };
                //var data = await con.QueryAsync<CourseMasterDTO_AM>("GetEditCourse_AM", paramList,
                //    commandType: CommandType.StoredProcedure);
                //list = data.ToList()[0];

                var multi = await con.QueryMultipleAsync("GetEditCourse_AM", paramList, commandTimeout: 0,
             commandType: CommandType.StoredProcedure);

                var list1 = await multi.ReadAsync<CourseMasterDTO_AM>();
                list = list1.ToList()[0];

                var lst2 = await multi.ReadAsync<COURSEANDTYPEMAPDTO_AM>();
                list.CtypeMapList = lst2.ToList();

            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<StudentMasterDTO> GETDeLLServerData()
        {
            StudentMasterDTO list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_DELLconnectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    flag = ""
                };
                var data = await con.QueryAsync<StudentMasterDTO>("GetSyncStudentMasterData_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];


            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<List<StudentMasterDTO>> CheckResult(string Flag, string Roll, bool IsAdmin)
        {

            List<StudentMasterDTO> list = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    RollNumber = Roll,
                    IsAdmin = IsAdmin
                };


                var multi = await con.QueryMultipleAsync("CheckStudentData_Admin_AM", paramList, commandTimeout: 0,
              commandType: CommandType.StoredProcedure);

                var list1 = await multi.ReadAsync<StudentMasterDTO>();
                list = list1.ToList();


            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;

        }

        public async Task<List<STUDENT_MARKS_AMDTO>> MarkAdminDetails(string Flag, string roll, string sem_no, int course_id, string sessionname, string exam_type)
        {

            List<STUDENT_MARKS_AMDTO> list = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    RollNumber = roll,
                    Sem_no = sem_no,
                    Course_id = course_id,
                    Sessionname = sessionname,
                    EXAM_TYPE = exam_type

                };


                var multi = await con.QueryMultipleAsync("MarkAdminDetails_AM", paramList, commandTimeout: 0,
              commandType: CommandType.StoredProcedure);

                var list1 = await multi.ReadAsync<STUDENT_MARKS_AMDTO>();
                list = list1.ToList();


            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;

        }

        //-------------------------------ERP-------------------------------------------
        public async Task<StudentMasterERPDTO> ResultStudentERPData(string Flag, SearchDTO model)
        {
            StudentMasterERPDTO list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_ERPconnectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    RollNumber = model.RollNumber,
                    IsAdmin = model.IsAdmin
                };
                //var data = await con.QueryAsync<StudentMasterDTO>("CheckStudentData_AM", paramList,
                //    commandType: CommandType.StoredProcedure);

                //list = data.ToList()[0];

                var multi = await con.QueryMultipleAsync("CheckStudentDataERP_AM", paramList, commandTimeout: 0,
              commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<StudentMasterERPDTO>();

                list = lst1.ToList()[0];
                if (Flag == "Result")
                {

                    var r1 = await multi.ReadAsync<StudentResultERPDTO>();
                    var r2 = await multi.ReadAsync<StudentResultERPDTO>();
                    var r3 = await multi.ReadAsync<StudentResultERPDTO>();
                    var r4 = await multi.ReadAsync<StudentResultERPDTO>();
                    var r5 = await multi.ReadAsync<StudentResultERPDTO>();
                    var r6 = await multi.ReadAsync<StudentResultERPDTO>();

                    var m1 = await multi.ReadAsync<StudentMarksERPNepDTO>();
                    var m2 = await multi.ReadAsync<StudentMarksERPNepDTO>();
                    var m3 = await multi.ReadAsync<StudentMarksERPNepDTO>();
                    var m4 = await multi.ReadAsync<StudentMarksERPNepDTO>();
                    var m5 = await multi.ReadAsync<StudentMarksERPNepDTO>();
                    var m6 = await multi.ReadAsync<StudentMarksERPNepDTO>();


                    list.Result1ERPList = r1.Count() == 0 ? new StudentResultERPDTO() : r1.ToList()[0];
                    list.Result2ERPList = r2.Count() == 0 ? new StudentResultERPDTO() : r2.ToList()[0];
                    list.Result3ERPList = r3.Count() == 0 ? new StudentResultERPDTO() : r3.ToList()[0];
                    list.Result4ERPList = r4.Count() == 0 ? new StudentResultERPDTO() : r4.ToList()[0];
                    list.Result5ERPList = r5.Count() == 0 ? new StudentResultERPDTO() : r5.ToList()[0];
                    list.Result6ERPList = r6.Count() == 0 ? new StudentResultERPDTO() : r6.ToList()[0];

                    list.Marks1ERPList = m1.Count() == 0 ? new List<StudentMarksERPNepDTO>() : m1.ToList();
                    list.Marks2ERPList = m2.Count() == 0 ? new List<StudentMarksERPNepDTO>() : m2.ToList();
                    list.Marks3ERPList = m3.Count() == 0 ? new List<StudentMarksERPNepDTO>() : m3.ToList();
                    list.Marks4ERPList = m4.Count() == 0 ? new List<StudentMarksERPNepDTO>() : m4.ToList();
                    list.Marks5ERPList = m5.Count() == 0 ? new List<StudentMarksERPNepDTO>() : m5.ToList();
                    list.Marks6ERPList = m6.Count() == 0 ? new List<StudentMarksERPNepDTO>() : m6.ToList();



                }
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }


        public async Task<List<StudentMasterDTO>> StudentList_AM(string Flag, string SessionName, string CourseType, bool IsPG, int CourseId, string ExamTypeName, string RollN, int IsLiveStatus, bool IsAdmin)
        {

            List<StudentMasterDTO> list = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    SessionName = SessionName,
                    CourseType = CourseType,

                    CourseId = CourseId,
                    ExamTypeName = ExamTypeName,
                    RollNumber = RollN,
                    IsLiveStatus = IsLiveStatus,
                    IsAdmin = IsAdmin
                };
                var multi = await con.QueryMultipleAsync("StudentList_Admin_AM", paramList, commandTimeout: 0,
              commandType: CommandType.StoredProcedure);

                var list1 = await multi.ReadAsync<StudentMasterDTO>();
                list = list1.ToList();


            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;

        }
        public async Task<FormResponse> StudentStatusUpdate(string EntryIDs, string IsActive, string? HeldIn, DateTime? Resultdate, long AdminID,string? SessionName)
        {
            FormResponse list = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {

                    EntryIDs = EntryIDs,
                    IsActive = IsActive,
                    HeldIn = HeldIn,
                    Resultdate = Resultdate,
                    AdminID = AdminID,
                    SESSION= SessionName
                };
                var d = await con.QueryAsync<FormResponse>("StudentStatusUpdate_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = d.ToList()[0];
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<List<HeldinDTO>> GetHeldinList(string flag, string CourseType, string? CourseName, string? ExamTypeName, string? SessionName)
        {
            var list = new List<HeldinDTO>();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag,
                    CourseType = CourseType,
                    CourseName = CourseName,
                    ExamTypeName = ExamTypeName,
                    SessionName = SessionName,
                };
                var data = await con.QueryAsync<HeldinDTO>("GetHeldinList_AM", paramList, commandTimeout: 0,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<List<ResultViewedDTO>> GetResultViewed(string flag,string Session="")
        {
            var list = new List<ResultViewedDTO>();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag,
                    Session= Session
                };
                var data = await con.QueryAsync<ResultViewedDTO>("ViewCountReport_AM", paramList, commandTimeout: 0,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<List<Ledger_AMDTO>> GetLedgerList(string flag)
        {
            var list = new List<Ledger_AMDTO>();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag
                };
                var data = await con.QueryAsync<Ledger_AMDTO>("GetLedgerList_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<FormResponse> SaveLedger(Ledger_AMDTO model)
        {
            FormResponse list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("FilePath");
                dt.Columns.Add("Name");
                if (model.FileList != null)
                {
                    foreach (var item in model.FileList)
                    {
                        dt.Rows.Add(item.FilePath, item.Name);
                    }
                }
                var paramList = new
                {
                    flag = "",
                    InvoiceNo = model.InvoiceNo,
                    LedgerName = model.LedgerName,
                    Amount = model.Amount,
                    PurchasedDate = model.PurchasedDate,
                    CreatedBy = model.CreatedBy,
                    IpAddress = model.IpAddress,

                    LedgerDocuments = dt

                };
                var data = await con.QueryAsync<FormResponse>("SaveLedger_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<FormResponse> LoginLog(int AdminId, string Name, string ipAddressName)
        {
            FormResponse list = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    AdminId = AdminId,
                    UserName= Name,
                    IpAddr = ipAddressName
                };
                var data = await con.QueryAsync<FormResponse>("LoginLog_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<DataTable> Download_Result_Summary(ResultReprtDTO model)
        {

            DataTable dt = new DataTable();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {

                    Flag = model.Flag,
                    CourseIDs = model.CourseIDs,
                    Sem = model.Sem,
                    Session = model.Session
                };
                var dr = await con.ExecuteReaderAsync("Download_Result_Summary_AM", paramList, commandTimeout: 0, commandType: CommandType.StoredProcedure);
                dt.Load(dr);
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public async Task<DataTable> DownloadPending(PendingReprtDTO model)
        {

            DataTable dt = new DataTable();
            await using var con = new SqlConnection(_EXAMconnectionString);
            con.Open();
            try
            {
                var paramList = new
                {

                    Flag = model.Flag,
                    SessionP = model.SessionP,
                    CourseidsP = model.CourseidsP,
                    SemesterS = model.SemesterS,
                    PaperType = model.PaperType,
                    IsCtypeU = model.IsCtypeU,
                    IsCtypeP = model.IsCtypeP,
                    IsCtypeA = model.IsCtypeA,
                    IsCtypeS = model.IsCtypeS
                };
                var dr = await con.ExecuteReaderAsync("DownloadPending_AM", paramList, commandTimeout: 0, commandType: CommandType.StoredProcedure);
                dt.Load(dr);
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public async Task<ResultDTO_DASH> GetResultDTO_DASH(string flag, string CourseType = "", bool? IsPG = null, string? CourseName = "", string SessionName = null)
        {
            ResultDTO_DASH d = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag,
                    CourseType = CourseType,
                    IsPG = IsPG,
                    SessionName = SessionName,
                    CourseName = CourseName
                };

                var multi = await con.QueryMultipleAsync("GetResultDTO_DASH_AM", paramList, commandTimeout: 0,
                  commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<COLLEGE_TYPE_RESULT_DTO>();
                var lst2 = await multi.ReadAsync<SESSION_MASTER_DTO>();
                var lst3 = await multi.ReadAsync<CourseMasterDTO_AM>();
                var lst4 = await multi.ReadAsync<EXAM_TYPE_MASTER_DTO>();
                var lst5 = await multi.ReadAsync<HeldinDTO>();

                d.CourseTypeList = lst1.ToList();
                d.SESSION_MASTER_LIST = lst2.ToList();
                d.CourseList = lst3.ToList();
                d.ExamTypeList = lst4.ToList();
                d.HeldinList = lst5.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;
        }
    }

}
