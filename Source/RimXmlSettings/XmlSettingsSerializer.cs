using RimXmlSettings.Elements;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace RimXmlSettings
{
    public static class XmlSettingsSerializer
    {
        public static void Serialize(this XmlSettings settings, TextWriter textWriter)
        {
            try
            {
                var doc = new XmlDocument();
                var rootElement = doc.CreateElement("RimXmlSettings");
                doc.AppendChild(rootElement);

                var settingsVersionNode = doc.CreateElement("SettingsVersion");
                settingsVersionNode.InnerText = settings.XmlSettingsVersion.ToString();
                doc.AppendChild(settingsVersionNode);

                var modSettingsNode = doc.CreateElement("ModSettings");
                foreach (var modKey in settings.ModSettings.Keys)
                {
                    var modSettings = settings.ModSettings[modKey];
                    var modNode = doc.CreateElement("Mod");

                    var keyAttr = doc.CreateAttribute("Key");
                    keyAttr.Value = modKey;
                    modNode.Attributes.Append(keyAttr);

                    var versionNode = doc.CreateElement("Version");
                    versionNode.InnerText = modSettings.ModVersion.ToString();
                    modNode.AppendChild(versionNode);

                    var propertiesNode = doc.CreateElement("Properties");
                    foreach (var propertyKey in modSettings.Properties.Keys)
                    {
                        AddPropertyNode(doc, propertiesNode, modSettings.Properties[propertyKey], propertyKey);
                    }
                    modNode.AppendChild(propertiesNode);

                    modSettingsNode.AppendChild(modNode);
                }
                doc.AppendChild(modSettingsNode);

                using (var xmlWriter = new XmlTextWriter(textWriter))
                {
                    doc.WriteTo(xmlWriter);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Coulnd't serialize XmlSettings", ex);
            }
        }

        public static XmlSettings Deserialize(TextReader textReader)
        {
            try
            {
                XmlSettings xmlSettings = new XmlSettings();
                var doc = new XmlDocument();
                doc.Load(textReader);
                var rootElement = doc.DocumentElement;

                // Version
                // Mod Settings
                //      Version
                //      Properties : SettingsProperty
                //          SpecificProperty : ex. ToggleProperty
                if (rootElement.Name != "RimXmlSettings")
                    throw new Exception("Invalid root element");

                if (!rootElement.HasChildNodes)
                    throw new Exception("RimXmlSettings has no children");

                var versionNode = rootElement["SettingsVersion"];
                xmlSettings.XmlSettingsVersion = new Version(versionNode.InnerText);

                var modSettingsNode = rootElement["ModSettings"];
                foreach (XmlElement modNode in modSettingsNode.ChildNodes)
                {
                    var modSettings = new XmlModSettings();

                    if (modNode.Name != "Mod")
                        throw new Exception("Invalid node under ModSettings");
                    var key = modNode.GetAttribute("Key");

                    var propertiesNode = modNode["Properties"];
                    foreach (XmlElement item in propertiesNode.ChildNodes)
                    {
                        modSettings.Properties.Add(item.GetAttribute("Key"), ReadPropertyFromNode(item));
                    }

                    xmlSettings.ModSettings.Add(key, modSettings);
                }

                return xmlSettings;
            }
            catch (Exception ex)
            {
                throw new Exception("Coulnd't deserialize XmlSettings", ex);
            }
        }

        private static void AddPropertyNode(XmlDocument doc, XmlElement parentNode, SettingsProperty property, string key)
        {
            var propertyNode = doc.CreateElement("Property");

            var keyAttr = doc.CreateAttribute("Key");
            keyAttr.Value = key;
            propertyNode.Attributes.Append(keyAttr);

            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty;
            foreach (var prop in property.GetType().GetProperties(flags))
            {
                var propNode = doc.CreateElement(prop.Name);

                propNode.InnerText = prop.GetValue(property, new object[] { }).ToString();

                propertyNode.AppendChild(propNode);
            }

            parentNode.AppendChild(propertyNode);
        }

        private static SettingsProperty ReadPropertyFromNode(XmlElement xmlElement)
        {


            return null;
        }
    }
}
