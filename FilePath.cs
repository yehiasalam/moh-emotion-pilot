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
            return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "lib", "affectiva-classifier-data");

        }

        static public String GetAffdexLicense()
        {
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "affdex.license"); ;

            return File.ReadAllText(path);
        }
    }
}
