using RimXmlSettings.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimXmlSettings
{
    public static class RimXml
    {
        private static XmlSettingsProxy proxySingleton;

        public static IXmlSettings Settings
        {
            get
            {
                if(proxySingleton == null)
                    Load();

                return proxySingleton;
            }
        }

        public static void Load()
        {
            var userSettings = XmlSettingsManager.LoadUserSettings();
            var defaultSettings = XmlSettingsManager.LoadDefaultSettings();
            proxySingleton = new XmlSettingsProxy(userSettings, defaultSettings);
        }

        public static void Save()
        {
            if (proxySingleton == null)
                Load();

            XmlSettingsManager.SaveUserSettings(proxySingleton.UserSettings);
        }
    }
}
