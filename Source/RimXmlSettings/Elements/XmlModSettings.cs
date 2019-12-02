using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RimXmlSettings.Elements
{
    public class XmlModSettings
    {
        public Version ModVersion { get; set; }

        public Dictionary<string, SettingsProperty> Properties { get; set; } = new Dictionary<string, SettingsProperty>();

        public T GetProperty<T>(string propertyKey) where T : SettingsProperty
        {
            if (!Properties.ContainsKey(propertyKey))
            {
                return null;
            }

            var property = Properties[propertyKey];

            if (!(property is T))
            {
                throw new Exception($"Cannot cast property {propertyKey} ({typeof(T).Name}.) to {property.GetType().Name}");
            }

            return (T)property;
        }
    }
}
