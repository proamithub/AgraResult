using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using DbrauResultUI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace DbrauResultUI.Generic
{
    public class FileDirectory
    {
        #region File Delete
        public static void FileDelete(string filePath)
        {
            string fpath = Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"), filePath);
            if (File.Exists(fpath))
            {
                File.Delete(fpath);
            }
        }
        #endregion


        #region Create Directory
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        #endregion


        #region Delete Directory
        public static void DeleteDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.Delete(path);
            }
        }
        #endregion


        #region Copy File
        public static void CopyFile(string sourcePath, string destinationFile, bool overwrite = true)
        {
            // To copy a file to another location and
            // overwrite the destination file if overwrite = 'true'.
            File.Copy(sourcePath, destinationFile, overwrite);
        }
        #endregion


        #region Move File
        public static void MoveFile(string sourcePath, string destinationFile)
        {
            // Ensure that the destination file does not exist.
            if (File.Exists(destinationFile))
                File.Delete(destinationFile);


            // To copy a file to another location and
            // overwrite the destination file if it already exists.
            File.Move(sourcePath, destinationFile);
        }
        #endregion


        #region Write Text File
        public static void WriteTextFile(string path, string content, bool appendText = false)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            using (StreamWriter sw = new StreamWriter(path, appendText, Encoding.UTF8))
            {
                sw.WriteLine(content);
                sw.Flush();
            }
        }
        #endregion


        #region Check File is Locked
        public static bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }


            //file is not locked
            return false;
        }
        #endregion


        public static string UnixTime()
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp.ToString();
        }


        #region Create Directory & Return Full & Absolute FilePath


        /// <summary>
        ///
        /// </summary>
        /// <param name="Root Folder Path"></param>
        /// <param name="FolderPathname, if mutiple then Use / between two folder"></param>
        /// <param name="FileName to generate unique fileName as FileName_ with Datetime"></param>
        /// <param name="Extemnsion of File"></param>
        /// <param name="Return FullFile Path in Outpur Parameter"></param>
        /// <param name="Return AbsoluteFile Path in Outpur Parameter"></param>
        public static void GenerateFilePath(string destFilePath, string GlobalPath, string FolderPath, string FileName, string FileExtension, out string? FullFilePath, out string? AbsoulteFilePath, bool unixTimeFlag = true)
        {
            GlobalPath = GlobalPath.Trim('~');
            FullFilePath = null;
            AbsoulteFilePath = null;
            string absoluteFolderPath = (GlobalPath + "/" + FolderPath).Replace("\\", "/");
            string fullFolderPath = (destFilePath + absoluteFolderPath).Replace("\\", "/");


            CreateDirectory(fullFolderPath);


            if (unixTimeFlag)
            {
                FileName = FileName + "_" + UnixTime() + "." + FileExtension;
            }
            else
            {
                FileName = FileName + "." + FileExtension;
            }


            FullFilePath = fullFolderPath + "/" + FileName;
            AbsoulteFilePath = absoluteFolderPath + "/" + FileName;
        }


        public static void GenerateFilePath(string destFilePath, string GlobalPath, string FolderPath, string FileName, string FileExtension, out string? FullFilePath, out string? AbsoulteFilePath, out string? ReturnFileName, bool unixTimeFlag = true)
        {
            GlobalPath = GlobalPath.Trim('~');
            FullFilePath = null;
            AbsoulteFilePath = null;


            string absoluteFolderPath = GlobalPath;


            if (!string.IsNullOrWhiteSpace(FolderPath))
                absoluteFolderPath = (GlobalPath + "/" + FolderPath).Replace("\\", "/");

            string fullFolderPath = (destFilePath + absoluteFolderPath).Replace("\\", "/");


            CreateDirectory(fullFolderPath);


            if (unixTimeFlag)
            {
                FileName = FileName + "_" + UnixTime() + "." + FileExtension;
            }
            else
            {
                FileName = FileName + "." + FileExtension;
            }


            ReturnFileName = FileName;
            FullFilePath = fullFolderPath + "/" + FileName;
            AbsoulteFilePath = absoluteFolderPath + "/" + FileName;
        }
        #endregion

        #region Check Photo Exist On Live
        public static async Task<PhotoExist> CheckPhotoExistOnLive(string Photograph)
        {
            PhotoExist exist = new PhotoExist();
            
            if (!string.IsNullOrEmpty(Photograph))
            {
                var imagePath = Photograph.Replace("~", "");

                var filePath = AppSettings.GetPhotoDomainName() + imagePath;
                using (var client = new System.Net.Http.HttpClient())
                {
                    try
                    {
                        var response = await client.GetAsync(filePath);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            exist.IsPhoto = true;
                            exist.FullFilePath = filePath;

                        }
                        else
                        {
                            exist.IsPhoto = false;
                            exist.FullFilePath = "";

                        }
                    }
                    catch (Exception)
                    {

                        exist.IsPhoto = false;
                        exist.FullFilePath = "";
                    }
                   
                }
            }

            return exist;
        }
        

        #endregion
        public class PhotoExist
        {
            public string FullFilePath { get; set; }
            public bool IsPhoto { get; set; }
        }

        #region Convert Excel to DataTable
        public static DataTable ConvertExceltoDataTable(string filePath, int rowToReadFrom = 0)
        {
            //Create a new DataTable.
            DataTable dt = new DataTable();
            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                int rowIndex = 1;

                bool headerRow = true;
                //Loop through the Worksheet rows.
                foreach (IXLRow row in workSheet.Rows())
                {
                    if (rowIndex >= rowToReadFrom)
                    {
                        if (headerRow) //Use the first row to add columns to DataTable.
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                string header = CommonFunctions.RemoveSpecialChars(cell.Value.ToString()).Trim();
                                dt.Columns.Add(header.Replace(" ", "_").Trim());
                            }
                            headerRow = false;
                        }
                        else
                        {
                            //Add rows to DataTable.
                            dt.Rows.Add();
                            int i = 0;
                            for (int j = 1; j <= dt.Columns.Count; j++)
                            {
                                string val = row.Cell(j).Value.ToString();
                                if (val.ToUpper().Contains("NULL") || val.ToUpper().Contains("N/A"))
                                {
                                    val = string.Empty;
                                }
                                if (!string.IsNullOrWhiteSpace(val))
                                    dt.Rows[dt.Rows.Count - 1][i] = val.Trim();
                                else
                                    dt.Rows[dt.Rows.Count - 1][i] = val;

                                i++;
                            }
                        }
                    }
                    rowIndex++;
                }
            }
            return dt;
        }
        #endregion

        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}