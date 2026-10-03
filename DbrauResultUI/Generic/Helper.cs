using ClosedXML.Excel;
using System.Data;


namespace DbrauResultUI.Generic
{
    public class Helper
    {
        private System.Collections.Specialized.NameValueCollection Headers = new System.Collections.Specialized.NameValueCollection();
        public void AddHeader(string name, string value = "")
        {
            Headers.Add(name, value);
        }
        #region Write Json File in Blob / S3 / Local
        public static void WriteFile(string fileName, string contentBody, string subDirectory)
        {
            try
            {
                string dirFolder = AppSettings.GetDestinationFilePath() + subDirectory.Trim('~').Replace("/", "\\");


                //Create Directory If not exists
                CommonFunctions.CreateDirectory(dirFolder);


                string dirFile = System.IO.Path.Combine(dirFolder, fileName);


                CommonFunctions.WriteTextFile(dirFile, contentBody);

            }
            catch
            {


            }
        }
        #endregion


        //#region Write Image file in Blob / S3 / Local
        //public static bool WriteBase64(string fileName, string contentBody, string subDirectory)
        //{
        //    try
        //    {
        //        string fileSaveInS3 = AppSettings.GetDestinationFilePath();


        //        if (fileSaveInS3.ToUpper() == "S3")
        //        {
        //            //Amazon_S3.WriteObject(fileName, contentBody, subDirectory.Trim('~'));
        //        }
        //        else if (fileSaveInS3.ToUpper() == "BLOB")
        //        {
        //            //AzureContainer.WriteBinaryObject(fileName, contentBody, subDirectory.Trim('~'));
        //        }
        //        else
        //        {
        //            string dirFolder = AppSettings.GetDestinationFilePath() + subDirectory.Trim('~').Replace("/", "\\");


        //            //Create Directory If not exists
        //            CommonFunctions.CreateDirectory(dirFolder);


        //            string dirFile = System.IO.Path.Combine(dirFolder, fileName);


        //            GlobalImageHelper.ConvertBase64ToImg(contentBody, dirFile);
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //#endregion


        #region Generate Excel Report


        public void GenerateExcelReport(string _filepath, string _sheetName, DataTable _dt)
        {
            var aCode = 65;


            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(_sheetName);


                int new_row = 0;


                if (_dt == null || _dt.Rows.Count == 0)
                {
                    _dt = new DataTable();
                    _dt.Columns.Add(new DataColumn("Result", typeof(System.String)));
                    _dt.Rows.Add("Records not found.");
                }
                else
                {
                    for (int i = 0; i < Headers.Keys.Count; i++)
                    {
                        var wsHeaderRange = ws.Range(string.Format("A{0}:{1}{0}", (i + 1), Char.ConvertFromUtf32(aCode + 4)));
                        wsHeaderRange.Merge();
                        wsHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsHeaderRange.Style.Font.SetFontSize(14);


                        if (!string.IsNullOrEmpty(Headers[Headers.Keys[i]]))
                            wsHeaderRange.Value = (Headers.Keys[i] + " : " + Headers[Headers.Keys[i]]).Trim();
                        else
                            wsHeaderRange.Value = Headers.Keys[i];
                    }


                    if (Headers.Keys.Count > 0)
                    {
                        new_row = Headers.Keys.Count + 1;


                        ws.Row(new_row).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ws.Row(new_row).Style.Border.RightBorder = XLBorderStyleValues.None;
                        ws.Row(new_row).Style.Border.LeftBorder = XLBorderStyleValues.None;
                    }
                }
                ws.Cell(new_row + 1, 1).InsertTable(_dt);


                //Adjust widths of Columns.
                try
                {
                    //////wb.Worksheet(1).Columns().AdjustToContents();
                   wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;
                }
                catch (Exception )
                {

                    throw;
                }
                wb.SaveAs(_filepath);
            }
        }


        public void GenerateExcelReport<T>(string _filepath, string _sheetName, IEnumerable<T> _list)
        {
            var aCode = 65;


            using (XLWorkbook wb = new XLWorkbook())
            {
                if (_list == null || _list.Count() == 0)
                {
                    DataTable _dt = new DataTable();
                    _dt.Columns.Add(new DataColumn("Result", typeof(System.String)));
                    _dt.Rows.Add("Records not found.");
                    wb.Worksheets.Add(_sheetName).Cell(1, 1).InsertTable(_dt);
                }
                else
                {
                    var ws = wb.Worksheets.Add(_sheetName);


                    for (int i = 0; i < Headers.Keys.Count; i++)
                    {
                        var wsHeaderRange = ws.Range(string.Format("A{0}:{1}{0}", (i + 1), Char.ConvertFromUtf32(aCode + 2)));
                        wsHeaderRange.Merge();
                        wsHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsHeaderRange.Style.Font.SetFontSize(14);


                        if (!string.IsNullOrEmpty(Headers[Headers.Keys[i]]))
                            wsHeaderRange.Value = (Headers.Keys[i] + " : " + Headers[Headers.Keys[i]]).Trim();
                        else
                            wsHeaderRange.Value = Headers.Keys[i];
                    }


                    int new_row = 0;
                    if (Headers.Keys.Count > 0)
                    {
                        new_row = Headers.Keys.Count + 1;


                        ws.Row(new_row).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ws.Row(new_row).Style.Border.RightBorder = XLBorderStyleValues.None;
                        ws.Row(new_row).Style.Border.LeftBorder = XLBorderStyleValues.None;
                    }
                    ws.Cell(new_row + 1, 1).InsertTable(_list);
                }


                //Adjust widths of Columns.
                wb.Worksheet(1).Columns().AdjustToContents();
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                wb.SaveAs(_filepath);
            }
        }
        #endregion
    }
}
