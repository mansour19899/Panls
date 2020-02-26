using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panls
{
    class ManageFileFolder
    {
        public static string destinationFolder = "D:\\ImagesOfProducts";
        public ManageFileFolder()
        {

        }
        public string copy(string path)
        {
            string sourcePath = path;
            string sourceFile = sourcePath;
            string targetPath = destinationFolder;
            DateTime date = DateTime.Now;
            string id = date.Year.ToString() + date.Month.ToString().NumtoStr(2) + date.Day.ToString().NumtoStr(2) + date.Hour.ToString().NumtoStr(2) + date.Minute.ToString().NumtoStr(2) + date.Second.ToString().NumtoStr(2);
            var newName = id + "." + sourcePath.Split('.').LastOrDefault();


            string destFile = System.IO.Path.Combine(targetPath, newName);
            System.IO.File.Copy(sourceFile, destFile, true);
            return destFile;
        }
    }
}
