using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimXmlSettings
{
    internal class XmlModSettingsModel 
    {
        internal class  ToggleProperty
        {
            public string Key;
            public bool Value;
        }

        public Version XmlSettingsVersion = new Version("0.0.0");

        public Version ModVersion = new Version("0.0.0");

        public ICollection<ToggleProperty> ToggleProperties = new List<ToggleProperty>();
    }
}
