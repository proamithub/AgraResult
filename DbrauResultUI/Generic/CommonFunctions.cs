
using NReco.PdfGenerator;
using System.Text;
using System.Text.RegularExpressions;


namespace DbrauResultUI.Generic
{
    public static class CommonFunctions
    {
        #region Remove SpecialCharacter
        public static string RemoveSpecialChars(string str)
        {
            // Create  a string array and add the special characters you want to remove
            string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]", "-" };
            //Iterate the number of times based on the String array length.
            for (int i = 0; i < chars.Length; i++)
            {
                //Check if the Given string contains the special Characters.
                if (str.Contains(chars[i]))
                {
                    //Here I replaced with emtpy string,
                    str = str.Replace(chars[i], " ");
                }
            }
            return Regex.Replace(str, @"\s+", " ");
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
        public static bool IsNullOrEmpty<T>(T[] array) where T : class
        {
            if (array == null || array.Length == 0)
                return true;
            else
                return array.All(item => item == null);
        }


        public static string ConvertBlankToNull(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.ToUpper() == "NULL")
            {
                return null;
            }
            else
            {
                return input.Trim(); // Optionally trim the string if you want to remove leading/trailing spaces
            }
        }


        public static object NullObject(object input)
        {
            if (input.ToString().ToUpper() == "NULL")
            {
                return null;
            }
            else
            {
                return input; // Optionally trim the string if you want to remove leading/trailing spaces
            }
        }


        #region Create Directory
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        #endregion

        public static void  ConvertAndSaveHtmlToPdf(string _filepath, string _fileName, string _content, string fileOrientation = "Portrait", int pageMargineTop = 0, int pageMargineBottom = 0, int pageMargineLeft = 0, int pageMargineRight = 0, int pageWidth = 210, int pageHeight = 297, int isPrintSize = 210)
        {
            _filepath = _filepath.Replace("~", "");
            _filepath = _filepath.Replace("\\", "/");
           
            CreateDirectory(_filepath);
            var margins = new PageMargins
            {
                Top = pageMargineTop,
                Bottom = pageMargineBottom,
                Left = pageMargineLeft,
                Right = pageMargineRight

            };
            _filepath += _fileName;
            HtmlToPdfConverter _htmlToPdf = new HtmlToPdfConverter
            {
                Orientation = fileOrientation == "Portrait" ? PageOrientation.Portrait : PageOrientation.Landscape,
                Margins = margins,
                CustomWkHtmlArgs = "--load-media-error-handling ignore",
                Zoom = 1.4f
            };
            if (fileOrientation == "Landscape")
            {
                _htmlToPdf.PageWidth = default;
                _htmlToPdf.PageHeight = default;
            }
            else
            {
                _htmlToPdf.PageWidth = pageWidth;
                _htmlToPdf.PageHeight = pageHeight;
            }
            var pdfBytes = _htmlToPdf.GeneratePdf(_content.ToString());

            if (System.IO.File.Exists(_filepath))
            {
                System.IO.File.Delete(_filepath);
            }
            System.IO.FileStream file = System.IO.File.Create(_filepath);
            file.Write(pdfBytes, 0, pdfBytes.Length);
            file.Close();
         
        }

      
    }
}