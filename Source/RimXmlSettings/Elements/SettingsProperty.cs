namespace RimXmlSettings.Elements
{
    public abstract class SettingsProperty
    {
        public SettingsProperty(string key)
        {
            Key = key;
        }

        public string Key { get; internal set; }
    }
}
