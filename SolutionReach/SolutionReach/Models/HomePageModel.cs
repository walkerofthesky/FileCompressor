using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SolutionReach.Models
{
    public class HomePageModel
    {
        //Input
        public FileInfo FileToCompress { get; set; }

        //Output
        public List<FileInfo> UnzippedFiles { get; set; }
        public List<FileInfo> ZippedFiles { get; set; }
 

        public void Init()
        {
            List<FileInfo> allFiles = new List<FileInfo>();

            string directoryPath = HttpContext.Current.Server.MapPath("~/Content/Files");            
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            if (directory.Exists)
            {
                //1. Get all the files in the /Content/Files directory
                allFiles = directory.GetFiles().ToList();
                //2. Separate them by zipped and unzipped files
                UnzippedFiles = allFiles.Where(zf => zf.Extension != ".zip").ToList();
                ZippedFiles = allFiles.Where(zf => zf.Extension == ".zip").ToList();
            }            
        }
    }
}