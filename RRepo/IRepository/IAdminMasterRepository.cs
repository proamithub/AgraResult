using RModel.DTO;
using RDATA.Entities;
using System;
using System.Collections.Generic;
using RModel;
using System.Threading.Tasks;
using RModel.DTO.Result;

namespace RRepo
{
    public interface IAdminMasterRepository : IRepository<AdminMaster>
    {
        //admin-login
        Task<AdminMasterDTO> AuthenticateAdmin(string userName, string password);
        bool IsAdminEmailExists(string email);
        Task<List<CourseMasterDTO_AM>> GetCourseTypeList(string flag);
        Task<List<CourseMasterDTO_AM>> GetCourseList(string flag, string CourseType, bool? IsPG,string SessionName);
        Task<StudentMasterDTO> CheckStudentData(SearchDTO model);
        Task<StudentMasterDTO> ResultStudentData(string Flag,SearchDTO model);
        Task<List<STUDENT_MARKS_AMDTO>> ResultMarksStudentData(SearchDTO model);
        Task<List<STUDENT_RESULT_AMDTO>> ResultDataStudentData(SearchDTO model);
        Task<List<ExamTyperDTO_AM>> GetExamTypeList(string flag, string  CourseName, string SessionName);
        Task<FormResponse> VisitCountSet(StudentMasterDTO model);


        Task<StudentMasterDTO> GETDeLLServerData();
        Task<List<CSP_MASTER_AMDTO>> CSVDataStudentData(StudentMasterDTO model);
        Task<List<StudentMasterDTO>> CheckResult(string Flag, string Roll, bool IsAdmin);
        Task<List<CourseMasterDTO_AM>> CourseMasterAdmin(bool WithCount,string SessionName);
    }
}
