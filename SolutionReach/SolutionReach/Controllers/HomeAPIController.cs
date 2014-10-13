using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SolutionReach.Controllers.API
{
    public class HomeAPIController : Controller
    {
        /// <summary>
        /// Save uploaded files to the /Content/Files directory
        /// </summary>
        /// <param name="fileInput"></param>
        /// <returns></returns>
        public JsonResult FileUpload(HttpPostedFileBase fileInput)
        {
            bool success = true;
            string message = "";
            string path = "";
            string fileName = "";

            //If the file exists, save it to /Content/Files with the same name
            if (fileInput != null && fileInput.ContentLength > 0)
            {
                fileName = Path.GetFileName(fileInput.FileName);
                path = Path.Combine(Server.MapPath("~/Content/Files"), fileName);
                try
                {
                    fileInput.SaveAs(path);
                }
                catch
                {
                    success = false;
                    message = "Unable to save file.";
                }
            }
            else
            {
                success = false;
                message = "No file was selected for upload.";
            }

            return new JsonResult()
            {
                Data = new
                {
                    success = success,
                    message = message,
                    filename = fileName,
                    path = path
                }
            };
        }

        /// <summary>
        /// Pass in a list of serialized paths to files you want zipped, and it will save the zip in the same directory
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public JsonResult ZipFiles(string files, string zipName)
        {
            bool success = true;
            string message = "";
            string path = "";

            List<string> fileList = new JavaScriptSerializer().Deserialize<List<string>>(files);
            string fileName = new JavaScriptSerializer().Deserialize<string>(zipName);
            fileName = fileName.EndsWith(".zip") ? fileName : string.Format("{0}.zip", fileName);

            using (var zip = new ZipFile())
            {
                //Add all the files directly into the root of the zip file
                zip.AddFiles(fileList, "");
                try
                {
                    path = string.Format("{0}{1}", Server.MapPath("~/Content/Files/"), fileName);
                    zip.Save(path);
                }
                catch
                {
                    success = false;
                    message = "Unable to zip selected files.";
                }
            }

            return new JsonResult()
            {
                Data = new
                {
                    success = success,
                    message = message,
                    filename = fileName,
                    path = path
                }
            };
        }
    }
}
