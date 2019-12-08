using System;
using System.Collections.Generic;

namespace RimXmlSettings.Elements
{
    public interface IXmlSettings
    {
        Version XmlSettingsVersion { get; set; }

        Version GetModVersion(string modKey);
        void SetModVersion(string modKey, Version version); 
        string GetValue(string modKey, string propertyKey);
        void SetValue(string modKey, string propertyKey, string value);
    }
}