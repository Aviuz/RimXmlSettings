using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RimXmlSettings.Test
{
    [TestClass]
    public class SettingsTest
    {
        public static readonly string prefsFilePath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                , "XmlModSettings.xml"
            );

        [TestMethod]
        public void SerializeSettings()
        {
            var settings = new Settings(prefsFilePath);

            settings.Load();
            settings.Save();
        }

        [TestMethod]
        public void AddTwoBoolPropertiesAndCheckThem()
        {
            var settings = new Settings(prefsFilePath);

            settings["value1"] = false;
            settings["value2"] = true;

            Assert.AreEqual(false, settings["value1"]);
            Assert.AreEqual(true, settings["value2"]);

            settings.Save();

            settings = new Settings(prefsFilePath);
            settings.Load();

            Assert.AreEqual(false, settings["value1"]);
            Assert.AreEqual(true, settings["value2"]);
        }
    }
}
