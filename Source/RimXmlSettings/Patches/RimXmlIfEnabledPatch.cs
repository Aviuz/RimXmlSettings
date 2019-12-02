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
            if (XmlSettings.GetProperty<ToggleProperty>(modKey, settingKey)?.Value == true)
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
