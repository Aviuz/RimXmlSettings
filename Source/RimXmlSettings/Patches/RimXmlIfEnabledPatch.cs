using RimXmlSettings.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Verse;

namespace RimXmlSettings.Patches
{
    public class RimXmlIfEnabledPatch : PatchOperation
    {
        private PatchOperation enabled;

        private PatchOperation disabled;

        private string modKey;

        private string settingKey;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            bool value;
            string valueString = RimXml.Settings.GetValue(modKey, settingKey);

            if (valueString == null)
            {
                Log.Error($"There is no key {modKey} in settings");
                return false;
            }

            if (!bool.TryParse(valueString, out value))
            {
                Log.Error($"Property {modKey} couldn't be parsed. Available values: {bool.TrueString}, {bool.FalseString}");
                return false;
            }

            if (value == true)
            {
                if (this.enabled != null)
                {
                    return this.enabled.Apply(xml);
                }
            }
            else if (this.disabled != null)
            {
                return this.disabled.Apply(xml);
            }
            return false;
        }
    }
}
