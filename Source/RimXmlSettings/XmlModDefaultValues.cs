using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RimXmlSettings.XmlModSettingsModel;

namespace RimXmlSettings
{
    internal class XmlModDefaultValues
    {
        public string ModKey { get; set; }

        public Version ModVersion { get; set; }

        public List<ToggleProperty> ToggleProperties = new List<ToggleProperty>();
    }
}
