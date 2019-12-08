using RimXmlSettings.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace RimXmlSettings
{
    [StaticConstructorOnStartup]
    public static class Initializer
    {
        static Initializer()
        {
#if DEBUG
            var settings = RimXml.Settings as XmlSettingsProxy;
            Log.Message($"[RimXmlSettings] Loaded Settings:");
            Log.Message($"RimXmlVersion: {settings.UserSettings.XmlSettingsVersion.ToString()}");
            Log.Message($"Mods in user-settings: {string.Join(", ", settings.UserSettings.ModSettings.Keys.ToArray())}");
            foreach (var modKey in settings.UserSettings.ModSettings.Keys)
            {
                Log.Message($"\tMod {modKey}:");
                foreach (var propertyKey in settings.UserSettings.ModSettings[modKey].Properties.Keys)
                {
                    var toggleProp = settings.UserSettings.ModSettings[modKey].Properties[propertyKey] as SettingsProperty;

                    Log.Message($"\t\t{propertyKey} - {toggleProp.Value}");
                }
            }
            Log.Message($"Loaded mods: {string.Join(", ", settings.DefaultSettings.ModSettings.Keys.ToArray())}");
            foreach (var modKey in settings.DefaultSettings.ModSettings.Keys)
            {
                Log.Message($"\tMod {modKey}:");
                foreach (var propertyKey in settings.DefaultSettings.ModSettings[modKey].Properties.Keys)
                {
                    var toggleProp = settings.DefaultSettings.ModSettings[modKey].Properties[propertyKey] as SettingsProperty;

                    Log.Message($"\t\t{propertyKey} - {toggleProp.Value}");
                }
            }

            RimXml.Settings.SetModVersion("Testeroooo", new Version("1.6.9"));
            RimXml.Settings.SetValue("Testeroooo", "Mordadelooo", "True");
            RimXml.Save();
#endif
        }
    }
}
