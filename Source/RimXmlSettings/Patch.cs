using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Verse;

namespace RimXmlSettings
{
    public class SettingsPatch : PatchOperation
    {
        private PatchOperation enabled;

        private PatchOperation disabled;

        private string settingKey;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            if (SettingsManager.Settings[settingKey] == true)
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
