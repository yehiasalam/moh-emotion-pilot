using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
  
namespace AffdexMe
{
    class FilePath
    {
        static public String GetClassifierDataFolder()
        {
            return "C:\\Program Files\\Affectiva\\Affdex SDK\\data";
        }

        static public String GetAffdexLicense()
        {
            //E:\Work\Samples\affdexme-win-master
            //"C:\\Users\\mostafa_osama\\Desktop\\sdk_os2os_7_up%40hotmail.com.license"
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\EmotionAnalysis\\App_License.license";
            return File.ReadAllText(fileName);
        }
    }
}
