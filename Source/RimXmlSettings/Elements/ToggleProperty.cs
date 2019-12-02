namespace RimXmlSettings.Elements
{
    public class ToggleProperty : SettingsProperty
    {
        public ToggleProperty(string key) : base(key) { }

        public bool Value { get; set; }
    }
}
