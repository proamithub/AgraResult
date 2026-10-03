
using DbrauResultUI.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QRCoder;
using RModel;
using RModel.DTO;
using RRepo;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;


namespace DbrauResultUI.Areas.Admin.ViewComponents
{
    public class CourseManageViewComponent : ViewComponent
    {
        IUnitOfWork uow;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected ISession Session => httpContextAccessor.HttpContext.Session;
        public CourseManageViewComponent(IUnitOfWork _uow, IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
            uow = _uow;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var ctlist = await uow.IAdminMaster.GetCourseTypeList("Type");
                List<SelectListItem> Select_Listct = new List<SelectListItem>();
                foreach (var r in ctlist)
                {
                    SelectListItem obj = new SelectListItem()
                    {
                        Value = r.CourseType.ToString(),
                        Text = r.CourseType,
                        //Selected = model.CtySelectedList.Where(me => me.CTypeID == r.CTypeID).Count() > 0 ? true : false
                    };

                    Select_Listct.Add(obj);
                }
                ViewBag.CtMaster = Select_Listct;
                var TemplateList = await uow.IAdminMaster.GetTemplateList("ALL");
                List<SelectListItem> TemplateListselect = new List<SelectListItem>();
                foreach (var r in TemplateList)
                {
                    SelectListItem obj = new SelectListItem()
                    {
                        Value = r.TName.ToString(),
                        Text = r.TName,
                        //Selected = model.CtySelectedList.Where(me => me.CTypeID == r.CTypeID).Count() > 0 ? true : false
                    };

                    TemplateListselect.Add(obj);
                }
                ViewBag.TemplateList = TemplateListselect;
                return View("~/areas/admin/views/components/_partialcourse.cshtml");



            }
            catch (Exception ex)
            {

            }

            return View("~/areas/admin/views/components/_partialcourse.cshtml");

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