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
            Settings.Default.Load();
        }


        struct ModWithSettings
        {
            public ModContentPack mod;
            public LoadableXmlAsset xmlAsset;
        }

        private static void LoadAllDefaultSettings()
        {
            List<ModWithSettings> settingsXmls = new List<ModWithSettings>();

            foreach (ModContentPack modContentPack in LoadedModManager.RunningMods)
            {
                settingsXmls.AddRange( DirectXmlLoader.XmlAssetsInModFolder(modContentPack, "XmlSettings/")
                    .Select(asset => new ModWithSettings() { mod = modContentPack, xmlAsset = asset }).ToList<ModWithSettings>());
            }

            foreach(var assset in settingsXmls)
            {
                var modDefaults = assset.xmlAsset.ChangeType<XmlModDefaultValues>();

                Settings.Default.SetToggleValue[assset.mod.Identifier]
            }
        }
    }
}
