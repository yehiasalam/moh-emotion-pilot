using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoHEmotionPilot
{
    class FilePath
    {
        static public String GetClassifierDataFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "moh-emotion-pilot", "affectiva-classifier-data");

        }

        static public String GetAffdexLicense()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "moh-emotion-pilot", "affdex.license"); ;

            return File.ReadAllText(path);
        }
    }
}
