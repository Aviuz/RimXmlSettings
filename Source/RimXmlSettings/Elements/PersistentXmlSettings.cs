using System;
using System.Collections.Generic;

namespace RimXmlSettings.Elements
{
    public class PersistentXmlSettings : IXmlSettings
    {
        public Version XmlSettingsVersion { get; set; } = new Version("0.0.0");

        public Dictionary<string, XmlModSettings> ModSettings { get; set; } = new Dictionary<string, XmlModSettings>();

        public Version GetModVersion(string modKey)
        {
            if (ModSettings.ContainsKey(modKey))
            {
                return ModSettings[modKey].ModVersion;
            }
            else
            {
                return null;
            }
        }

        public void SetModVersion(string modKey, Version version)
        {
            if (ModSettings.ContainsKey(modKey))
            {
                ModSettings[modKey].ModVersion = version;
            }
            else
            {
                ModSettings.Add(modKey, new XmlModSettings() { ModVersion = version });
            }
        }

        public string GetValue(string modKey, string propertyKey)
        {
            if (ModSettings.ContainsKey(modKey) && ModSettings[modKey].Properties.ContainsKey(propertyKey))
            {
                return ModSettings[modKey].Properties[propertyKey].Value;
            }
            else
            {
                return null;
            }
        }

        public void SetValue(string modKey, string propertyKey, string value)
        {
            if (!ModSettings.ContainsKey(modKey))
            {
                throw new Exception("Before assigning value, mod need to be initialized");
            }

            if (ModSettings[modKey].Properties.ContainsKey(propertyKey))
            {
                ModSettings[modKey].Properties[propertyKey].Value = value;
            }
            else
            {
                ModSettings[modKey].Properties.Add(propertyKey, new SettingsProperty(value));
            }
        }
    }
}
