using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimXmlSettings
{
    internal class ToggleProperty
    {
        public string Key;
        public bool Value;
    }

    internal class ModSettings
    {
        public string ModKey;

        public Version ModVersion = new Version("0.0.0");

        public List<ToggleProperty> ToggleProperties = new List<ToggleProperty>();
    }

    internal class XmlModSettingsModel
    {
        public Version XmlSettingsVersion = new Version("0.0.0");

        public List<ModSettings> Mods = new List<ModSettings>();
    }
}