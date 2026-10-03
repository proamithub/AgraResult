using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RModel.CustomAttribute
{
    public class ValidatePhotoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string[] AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            var file = value as IFormFile;

            if (file == null)
            {
                return true;
            }
            else if (!AllowedFileExtensions.Contains(file.FileName.ToLower().Substring(file.FileName.ToLower().LastIndexOf('.'))))
            {
                ErrorMessage = "Please upload Your image of type: " + string.Join(", ", AllowedFileExtensions);
                return false;
            }
            //else if (file.ContentLength > MaxContentLength)
            //{
            //    ErrorMessage = "Your image is too large, maximum allowed size is : " + (MaxContentLength / 1024).ToString() + "MB";
            //    return false;
            //}
            else
                return true;
        }
    }
}
