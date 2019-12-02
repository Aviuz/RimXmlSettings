using RimXmlSettings.Elements;
using System;
using System.IO;
using Verse;

namespace RimXmlSettings
{
    public static class XmlSettingsManager
    {
        public static readonly string UserSettingsLocation = Path.Combine(GenFilePaths.ConfigFolderPath, "XmlModSettings.xml");

        public static XmlSettings LoadUserSettings()
        {
            // TODO Implement LoadUserSettings()
            //if (File.Exists(prefsFilePath))
            //{
            //    try
            //    {
            //        Model = DirectXmlLoader.ItemFromXmlFile<XmlModSettingsModel>(prefsFilePath, true);
            //    }
            //    catch (Exception ex)
            //    {
            //        Log.Message($"RimXmlSettings occured error, loading default settings: {ex.ToString()}");
            //        Model = new XmlModSettingsModel();
            //    }
            //}

            throw new NotImplementedException();
        }

        public static void SaveUserSettings(XmlSettings settings)
        {
            // TODO Implement SaveUserSettings(XmlSettings settings)
            //try
            //{
            //    var xDocument = new XDocument();
            //    var content = DirectXmlSaver.XElementFromObject(Model, typeof(XmlModSettingsModel));
            //    xDocument.Add(content);
            //    xDocument.Save(prefsFilePath);
            //}
            //catch (Exception ex)
            //{
            //    GenUI.ErrorDialog("ProblemSavingFile".Translate(prefsFilePath, ex.ToString()));
            //    Log.Error("Exception saving prefs: " + ex);
            //}

            throw new NotImplementedException();
        }

        public static XmlSettings LoadDefaultSettings()
        {
            // TODO Implement LoadDefaultSettings()
            //var settingsXmls = new List<LoadableXmlAsset>();

            //foreach (ModContentPack modContentPack in LoadedModManager.RunningMods)
            //{
            //    settingsXmls.AddRange(DirectXmlLoader.XmlAssetsInModFolder(modContentPack, "XmlSettings/")
            //        .ToList());
            //}

            //foreach (var asset in settingsXmls)
            //{
            //    var modDefaults = asset.ChangeType<XmlModDefaultValues>();

            //    Settings.Default.InitializeFromXml(asset.mod, asset);

            //    Settings.Default.SetToggleValue(asset.mod.Identifier, null, null);
            //}

            throw new NotImplementedException();
        }
    }
}
