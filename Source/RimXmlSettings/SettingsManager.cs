using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimXmlSettings
{
    public static class SettingsManager
    {
        public static readonly string prefsFilePath = Path.Combine(GenFilePaths.ConfigFolderPath, "XmlModSettings.xml");

        static SettingsManager()
        {
            Settings = new Settings(prefsFilePath);
        }

        public static Settings Settings { get; private set; }
    }
}
