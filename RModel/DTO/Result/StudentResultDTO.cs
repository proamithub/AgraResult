using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO.Result
{
    public class StudentResultDTO
    {

        public long RecId { get; set; }
        public long StudentId { get; set; }
        public string? EntryType { get; set; }
        public int? SemesterYear { get; set; }
        public string? Session { get; set; }
        public string? RollNo { get; set; }
        public int? MarksObt { get; set; }
        public int? MaxMarks { get; set; }
        public int? MinMarks { get; set; }
        public int? TotalCredit { get; set; }
        public int? TotalEarnCredit { get; set; }
        public decimal? TotalPoint { get; set; }
        public decimal? TotalGradePoint { get; set; }
        public decimal? SGPA { get; set; }
        public string? Result { get; set; }
        public string? Division { get; set; }
        public string? FinalResult { get; set; }
        public string? FinalGrade { get; set; }
        public string? Remarks { get; set; }
        public DateTime? ResultDeclDate { get; set; }
        public string? MarksObt_1 { get; set; }
        public int? MarksMin_1 { get; set; }
        public int? MarksMax_1 { get; set; }
        public int? TotalCredit_1 { get; set; }
        public int? EarnCredit_1 { get; set; }
        public decimal? TotalGradePoint_1 { get; set; }
        public decimal? Sgpa_1 { get; set; }
        public decimal? Cgpa_1 { get; set; }
        public string? Result_1 { get; set; }
        public int? MarksObt_2 { get; set; }
        public int? MarksMin_2 { get; set; }
        public int? MarksMax_2 { get; set; }
        public int? TotalCredit_2 { get; set; }
        public int? EarnCredit_2 { get; set; }
        public decimal? TotalGradePoint_2 { get; set; }
        public decimal? Sgpa_2 { get; set; }
        public decimal? Cgpa_2 { get; set; }
        public string? Result_2 { get; set; }
        public int? MarksObt_3 { get; set; }
        public int? MarksMin_3 { get; set; }
        public int? MarksMax_3 { get; set; }
        public int? TotalCredit_3 { get; set; }
        public int? EarnCredit_3 { get; set; }
        public decimal? TotalGradePoint_3 { get; set; }
        public decimal? Sgpa_3 { get; set; }
        public decimal? Cgpa_3 { get; set; }
        public string? Result_3 { get; set; }
        public int? MarksObt_4 { get; set; }
        public int? MarksMin_4 { get; set; }
        public int? MarksMax_4 { get; set; }
        public int? TotalCredit_4 { get; set; }
        public int? EarnCredit_4 { get; set; }
        public decimal? TotalGradePoint_4 { get; set; }
        public decimal? Sgpa_4 { get; set; }
        public decimal? Cgpa_4 { get; set; }
        public string? Result_4 { get; set; }
        public int? MarksObt_5 { get; set; }
        public int? MarksMin_5 { get; set; }
        public int? MarksMax_5 { get; set; }
        public int? TotalCredit_5 { get; set; }
        public int? EarnCredit_5 { get; set; }
        public decimal? TotalGradePoint_5 { get; set; }
        public decimal? Sgpa_5 { get; set; }
        public decimal? Cgpa_5 { get; set; }
        public string? Result_5 { get; set; }
        public int? MarksObt_6 { get; set; }
        public int? MarksMin_6 { get; set; }
        public int? MarksMax_6 { get; set; }
        public int? TotalCredit_6 { get; set; }
        public int? EarnCredit_6 { get; set; }
        public decimal? TotalGradePoint_6 { get; set; }
        public decimal? Sgpa_6 { get; set; }
        public decimal? Cgpa_6 { get; set; }
        public string? Result_6 { get; set; }
        public int? MarksObt_7 { get; set; }
        public int? MarksMin_7 { get; set; }
        public int? MarksMax_7 { get; set; }
        public int? TotalCredit_7 { get; set; }
        public int? EarnCredit_7 { get; set; }
        public decimal? TotalGradePoint_7 { get; set; }
        public decimal? Sgpa_7 { get; set; }
        public decimal? Cgpa_7 { get; set; }
        public string? Result_7 { get; set; }
        public int? MarksObt_8 { get; set; }
        public int? MarksMin_8 { get; set; }
        public int? MarksMax_8 { get; set; }
        public int? TotalCredit_8 { get; set; }
        public string? EarnCredit_8 { get; set; }
        public decimal? TotalGradePoint_8 { get; set; }
        public decimal? Sgpa_8 { get; set; }
        public decimal? Cgpa_8 { get; set; }
        public string? Result_8 { get; set; }
        public int? MarksObt_9 { get; set; }
        public int? MarksMin_9 { get; set; }
        public int? MarksMax_9 { get; set; }
        public int? TotalCredit_9 { get; set; }
        public int? EarnCredit_9 { get; set; }
        public decimal? TotalGradePoint_9 { get; set; }
        public decimal? Sgpa_9 { get; set; }
        public decimal? Cgpa_9 { get; set; }
        public int? Result_9 { get; set; }
        public int? MarksObt_10 { get; set; }
        public int? MarksMin_10 { get; set; }
        public int? MarksMax_10 { get; set; }
        public int? TotalCredit_10 { get; set; }
        public int? EarnCredit_10 { get; set; }
        public decimal? TotalGradePoint_10 { get; set; }
        public decimal? Sgpa_10 { get; set; }
        public decimal? Cgpa_10 { get; set; }
        public string? Result_10 { get; set; }
        public int? TotalObtMarks { get; set; }
        public int? TotalMaxMarks { get; set; }
        public int? TotalMinMarks { get; set; }
        public decimal? CGPA { get; set; }
        public int? RGMarksObt { get; set; }
        public int? RGMinMarks { get; set; }
        public int? RGMaxMarks { get; set; }
        public string? RGResult { get; set; }
        public string? ExamName { get; set; }
        public string? ExamSession { get; set; }
        public string? HeldIn { get; set; }
        public long? MarkSheetSlNo { get; set; }
        public int? SubMarkSheetSlNo { get; set; }
        public DateTime? MarkSheetPrintDate { get; set; }
        public int? PrintLotNo { get; set; }
        public int? ReadyForResult { get; set; }
        public bool? IsWebPublishsed { get; set; }
        public string? WebPublishBy { get; set; }
        public DateTime? WebPublishDate { get; set; }
        public bool? IsSync { get; set; }
        public string? SyncBy { get; set; }
        public DateTime? SyncDate { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? MigratedTableRecId { get; set; }
        public string? MigratedTableName { get; set; }
    }

}
