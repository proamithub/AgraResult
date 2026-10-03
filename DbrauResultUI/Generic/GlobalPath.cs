namespace DbrauResultUI.Generic
{
    public class GlobalPath
    {
        private static string TempFile { get { return "~/Assets/Temp/"; } }
        public static string DownloadReport { get { return TempFile + "DownloadReport/"; } }
        public static string DownloadReportPDF { get { return TempFile + "DownloadReportPDF/"; } }
        public static string ImportExcel { get { return TempFile + "ImportExcel/"; } }
		public static string ImportMarksExcel { get { return TempFile + "ImportMarksExcel/"; } }
		private static string TempFile1 { get { return "~/Assets/"; } }
        public static string UploadedDocuments { get { return TempFile1 + "UploadedDocuments/"; } }
    }
}
