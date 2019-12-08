using System;

namespace RimXmlSettings.Elements
{
    public class XmlSettingsProxy : IXmlSettings
    {
        public XmlSettingsProxy(PersistentXmlSettings userSettings, PersistentXmlSettings defaultSettings)
        {
            UserSettings = userSettings;
            DefaultSettings = defaultSettings;
        }

        public PersistentXmlSettings UserSettings { get; }

        public PersistentXmlSettings DefaultSettings { get; }

        public Version XmlSettingsVersion
        {
            get => UserSettings.XmlSettingsVersion;
            set => UserSettings.XmlSettingsVersion = value;
        }

        public Version GetModVersion(string modKey)
        {
            return DefaultSettings.GetModVersion(modKey);
        }

        public void SetModVersion(string modKey, Version version)
        {
            UserSettings.SetModVersion(modKey, version);
        }

        public string GetValue(string modKey, string propertyKey)
        {
            string result = UserSettings.GetValue(modKey, propertyKey);

            if (result != null)
            {
                return result;
            }
            else
            {
                return DefaultSettings.GetValue(modKey, propertyKey);
            }
        }

        public void SetValue(string modKey, string propertyKey, string value)
        {
            // Initialize mod settings
            if (UserSettings.GetModVersion(modKey) == null)
                UserSettings.SetModVersion(modKey, DefaultSettings.GetModVersion(modKey));

            UserSettings.SetValue(modKey, propertyKey, value);
        }
    }
}
