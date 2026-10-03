
using DbrauResultUI.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using RModel;
using RModel.DTO;
using RRepo;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace DbrauResultUI.ViewComponents
{
    public class ResultViewComponent : ViewComponent
    {
        IUnitOfWork uow;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected ISession Session => httpContextAccessor.HttpContext.Session;
        public ResultViewComponent(IUnitOfWork _uow, IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
            uow = _uow;
        }

        public async Task<IViewComponentResult> InvokeAsync(string data)
        {
            try
            {

                var s = Session.GetObject<SearchDTO>("resultdata");
                var roll = AESEncription.Base64Decode(data);
                if (roll == s.RollNumber)
                {
                    if (string.IsNullOrEmpty(s.FName))
                    {
                        var datet = Convert.ToDateTime(s.DOBstr, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                        s.DOB = datet;
                    }
                    StudentMasterDTO adminData = await uow.IAdminMaster.ResultStudentData("Result", s);
                    if (roll == adminData.ROLL_NO)
                    {
                        //adminData.MarksList = await uow.IAdminMaster.ResultMarksStudentData(s);
                        // adminData.ResultList = await uow.IAdminMaster.ResultDataStudentData(s);
                        //adminData.CSPList = await uow.IAdminMaster.CSVDataStudentData(adminData);
                        adminData.EncrptedRoll = data;
                        adminData.SessionName = s.SessionName;
                        adminData.HELD_IN = !string.IsNullOrEmpty(s.HELD_IN)?s.HELD_IN:adminData.HELD_IN;

                        var data1 = adminData.CourseName + "$" + s.RollNumber + "$" + s.CourseType + "$" + s.ExamTypeName + "$" + s.SessionName + "$" + s.HELD_IN;
                        data1 = AESEncription.Base64Encode(data1);
                        var UriPayload = "https://result2024.agrauniv.online/resultpreview/" + data1;
                        QRCodeGenerator _qrCode = new QRCodeGenerator();
                        QRCodeData _qrCodeData = _qrCode.CreateQrCode(UriPayload, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(_qrCodeData);
                        Bitmap qrCodeImage = qrCode.GetGraphic(20);
                        adminData.qcore = BitmapToBytesCode(qrCodeImage);

                        switch (adminData.TemplateName)
                        {
                            case "T1":
                                return View("/views/components/_Result_t1.cshtml", adminData);
                            case "Nep_G":
                                return View("/views/components/_Result_Nep_G.cshtml", adminData);
                            case "Nep_PG":
                                return View("/views/components/_Result_Nep_PG.cshtml", adminData);
                            case "PG_CBCS":
                                return View("/views/components/_Result_PG_CBCS.cshtml", adminData);
                            case "PG_MLIS":
                                return View("/views/components/_Result_PG_MLIS.cshtml", adminData);
                            case "MEDICAL":
                                return View("/views/components/_Result_MEDICAL.cshtml", adminData);
                            case "Edu_I":
                                return View("/views/components/_Result_Edu1.cshtml", adminData);
                            case "LAW":
                                return View("/views/components/_Result_Law.cshtml", adminData);
                            case "VOC":
                                return View("/views/components/_Result_Voc.cshtml", adminData);

                            case "OLD_VOC":
                                return View("/views/components/_Result_OLD_VOC.cshtml", adminData);

                            case "BPES":
                                return View("/views/components/_Result_BPES.cshtml", adminData);
                            case "MPES":
                                return View("/views/components/_Result_MPES.cshtml", adminData);
                            case "Edu_II":
                                return View("/views/components/_Result_Edu2.cshtml", adminData);
                            case "Edu_IV":
                                return View("/views/components/_Result_Edu4.cshtml", adminData);
                            case "Edu_FORESTRY":
                                return View("/views/components/_Result_Forestry.cshtml", adminData);
                                
                            case "BCAVI":
                                return View("/views/components/_Result_BCAVI.cshtml", adminData);
                            case "BCOMECOM":
                                return View("/views/components/_Result_BCOMECOM.cshtml", adminData);
                            case "MBBS":
                                return View("/views/components/_Result_MBBS.cshtml", adminData);
                            case "MBBS_OLD":
                                return View("/views/components/_Result_MBBS_OLD.cshtml", adminData);
                            case "Bsc_AG_1":
                                return View("/views/components/_Result_Bsc_AG_1.cshtml", adminData);
                            case "Bsc_AG_3":
                                return View("/views/components/_Result_Bsc_AG_3.cshtml", adminData);
                                
                            case "Bsc_AG_7":
                                return View("/views/components/_Result_Bsc_AG_7.cshtml", adminData);
                            case "Bsc_AG_8":
                                return View("/views/components/_Result_Bsc_AG_8.cshtml", adminData);
                            case "PHARMA":
                                return View("/views/components/_Result_PHARMA.cshtml", adminData);
                            case "VocAnnual":
                                return View("/views/components/_Result_VocAnnual.cshtml", adminData);
                            case "BPEDIV":
                                return View("/views/components/_Result_BPEDIV.cshtml", adminData);
                            case "Diploma":
                                return View("/views/components/_Result_Diploma.cshtml", adminData);
                            case "AnnualCom":
                                return View("/views/components/_Result_AnnualCom.cshtml", adminData);
                            case "AnnualUG":
                                return View("/views/components/_Result_AnnualUG.cshtml", adminData);
                            case "DPL_II":
                                return View("/views/components/_Result_DPL_II.cshtml", adminData);
                            case "BioTech_Annual":
                                return View("/views/components/_Result_BioTech_Annual.cshtml", adminData);
                            case "Msc_AG":
                                return View("/views/components/_Result_Msc_AG.cshtml", adminData);
                            case "AnnualPG":
                                return View("/views/components/_Result_AnnualPG.cshtml", adminData);
                            case "BIOTECH_VOC":
                                return View("/views/components/_Result_Biotech_VOC.cshtml", adminData);
                            case "PHARMA_PG":
                                return View("/views/components/_Result_PHARMA_PG.cshtml", adminData);
                            case "Nursing":
                                return View("/views/components/_Result_Nursing.cshtml", adminData);
                            case "BSC_AG_PLAIN":
                                return View("/views/components/_Result_BSC_AG_PLAIN.cshtml", adminData);
                            case "Msc_AG_IV":
                                return View("/views/components/_Result_Msc_AG_IV.cshtml", adminData);
                            case "AnnualBioTech_I_II":
                                return View("/views/components/_Result_AnnualBioTech_I_II.cshtml", adminData);
                            case "BDS":
                                return View("/views/components/_Result_BDS.cshtml", adminData);
                            case "PHARMA_D":
                                return View("/views/components/_Result_PHARMA_D.cshtml", adminData);
                                
                        }


                    }
                }

            }
            catch (Exception ex)
            {

            }

            return View("/views/components/_Result_t1.cshtml");

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
    }
}