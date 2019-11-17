using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Verse;

namespace RimXmlSettings
{
    public class Settings
    {
        private string prefsFilePath;
        private static Settings defaultSettings;

        public Settings(string prefsFilePath)
        {
            this.prefsFilePath = prefsFilePath;
            Model = new XmlModSettingsModel();
        }

        public static Settings Default
        {
            get
            {
                if (defaultSettings == null)
                    defaultSettings = new Settings(Path.Combine(GenFilePaths.ConfigFolderPath, "XmlModSettings.xml"));

                return defaultSettings;
            }
        }

        private RimXmlSettings.ModSettings this[string key]
        {
            get
            {
                if (key == null)
                    throw new NullReferenceException();

                var modSettings = Model.Mods.SingleOrDefault(m=>m.ModKey == key);
                
                if(modSettings == null)
                {
                    modSettings = new RimXmlSettings.ModSettings();
                    Model.Mods.Add(modSettings);
                }

                return modSettings;
            }
        }

        public bool? GetToggleValue(string modName, string key)
        {
            if (!this[modName].ToggleProperties.Any(p => p.Key == key))
                return null;
            else
                return this[modName].ToggleProperties.Single(p => p.Key == key).Value;
        }

        public void SetToggleValue(string modName, string key, bool? value)
        {
            if (value.HasValue)
            {
                if (!this[modName].ToggleProperties.Any(p => p.Key == key))
                {
                    this[modName].ToggleProperties.Add(new RimXmlSettings.ToggleProperty() { Key = key, Value = value.Value });
                }
                else
                {

                    this[modName].ToggleProperties.Single(p => p.Key == key).Value = value.Value;
                }
            }
            else
            {
                var item = this[modName].ToggleProperties.SingleOrDefault(p => p.Key == key);
                if (item != null)
                    this[modName].ToggleProperties.Remove(item);
            }
        }

        private XmlModSettingsModel Model { get; set; }

        public void Load()
        {
            if (File.Exists(prefsFilePath))
            {
                try
                {
                    Model = DirectXmlLoader.ItemFromXmlFile<XmlModSettingsModel>(prefsFilePath, true);
                }
                catch (Exception ex)
                {
                    Log.Message($"RimXmlSettings occured error, loading default settings: {ex.ToString()}");
                    Model = new XmlModSettingsModel();
                }
            }
        }

        public void Save()
        {
            try
            {
                var xDocument = new XDocument();
                var content = DirectXmlSaver.XElementFromObject(Model, typeof(XmlModSettingsModel));
                xDocument.Add(content);
                xDocument.Save(prefsFilePath);
            }
            catch (Exception ex)
            {
                GenUI.ErrorDialog("ProblemSavingFile".Translate(prefsFilePath, ex.ToString()));
                Log.Error("Exception saving prefs: " + ex);
            }
        }
    }
}
