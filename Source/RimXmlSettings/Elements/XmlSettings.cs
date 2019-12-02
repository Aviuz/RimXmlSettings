using System;
using System.Collections.Generic;

namespace RimXmlSettings.Elements
{
    public class XmlSettings
    {
        private static XmlSettings userSettings;
        private static XmlSettings defaultSettings;

        public static XmlSettings UserSettings
        {
            get
            {
                if (userSettings == null)
                    userSettings = XmlSettingsManager.LoadUserSettings();
                return userSettings;
            }
        }

        public static XmlSettings DefaultSettings
        {
            get
            {
                if (defaultSettings == null)
                    defaultSettings = XmlSettingsManager.LoadDefaultSettings();
                return defaultSettings;
            }
        }

        public Version XmlSettingsVersion { get; set; }

        public Dictionary<string, XmlModSettings> ModSettings { get; set; } = new Dictionary<string, XmlModSettings>();

        public T Property<T>(string modKey, string propertyKey) where T : SettingsProperty
        {
            if (!ModSettings.ContainsKey(modKey) || !ModSettings[modKey].Properties.ContainsKey(propertyKey))
            {
                return null;
            }

            var property = ModSettings[modKey].Properties[propertyKey];

            if (!(property is T))
            {
                throw new Exception($"Cannot cast property {propertyKey} ({typeof(T).Name}.) to {property.GetType().Name}");
            }

            return (T)property;
        }

        public static T GetProperty<T>(string modKey, string propertyKey) where T : SettingsProperty
        {
            T property = UserSettings.Property<T>(modKey, propertyKey);

            if (property != null)
                return property;
            else
                return DefaultSettings.Property<T>(modKey, propertyKey);
        }

        public static void Load()
        {
            userSettings = XmlSettingsManager.LoadUserSettings();
            defaultSettings = XmlSettingsManager.LoadDefaultSettings();
        }

        public static void Save()
        {
            XmlSettingsManager.SaveUserSettings(UserSettings);
        }
    }
}
