using RimXmlSettings.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Verse;

namespace RimXmlSettings
{
    public static class XmlSettingsManager
    {
        public static readonly string UserSettingsLocation = Path.Combine(GenFilePaths.ConfigFolderPath, "XmlModSettings.xml");

        public static XmlSettings LoadUserSettings()
        {
            if (File.Exists(UserSettingsLocation))
            {
                try
                {
                    using (var fileReader = File.OpenText(UserSettingsLocation))
                    {
                        return XmlSettingsSerializer.Deserialize(fileReader);
                    }
                }
                catch (Exception ex)
                {
                    Log.Message($"RimXmlSettings occured error, loading default settings: {ex.ToString()}");
                    return DefaultUserSettings();
                }
            }
            else
            {
                return DefaultUserSettings();
            }
        }

        private static XmlSettings DefaultUserSettings()
        {
            return new XmlSettings();

        }

        public static void SaveUserSettings(XmlSettings settings)
        {
            try
            {
                using (var fileStream = File.OpenWrite(UserSettingsLocation))
                using (var textWriter = new StreamWriter(fileStream))
                {
                    settings.Serialize(textWriter);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception saving prefs: " + ex);
            }
        }

        public static XmlSettings LoadDefaultSettings()
        {
            var xmlSettings = new XmlSettings();

            var settingsXmls = new List<LoadableXmlAsset>();

            foreach (ModContentPack modContentPack in LoadedModManager.RunningMods)
            {
                settingsXmls.AddRange(DirectXmlLoader.XmlAssetsInModFolder(modContentPack, "XmlSettings/")
                    .ToList());
            }

            foreach (var asset in settingsXmls)
            {
                string modName;
                XmlModSettings modSettings;

                modName = asset.mod.Identifier;
                using (var reader = File.OpenText(asset.FullFilePath))
                {
                    modSettings = XmlSettingsSerializer.DeserializeModSettings(reader);
                }
                xmlSettings.ModSettings[modName] = modSettings;
            }

            return xmlSettings;
        }
    }
}
