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

        public Settings(string prefsFilePath)
        {
            this.prefsFilePath = prefsFilePath;
            Model = new XmlModSettingsModel();
        }

        public bool? this[string key]
        {
            get
            {
                if (!Model.ToggleProperties.Any(p => p.Key == key))
                    return null;
                else
                    return Model.ToggleProperties.Single(p => p.Key == key).Value;
            }
            set
            {
                if (value.HasValue)
                {
                    if (!Model.ToggleProperties.Any(p => p.Key == key))
                        Model.ToggleProperties.Add(new XmlModSettingsModel.ToggleProperty() { Key = key, Value = value.Value });
                    else
                        Model.ToggleProperties.Single(p => p.Key == key).Value = value.Value;
                }
                else
                {
                    var item = Model.ToggleProperties.SingleOrDefault(p => p.Key == key);
                    if (item != null)
                        Model.ToggleProperties.Remove(item);
                }
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
                    //throw ex;
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
