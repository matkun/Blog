namespace PageTypeTreeFilter.Framework
{
    public class Settings : ISettings
    {
        public string GlobalSettingsRoles
        {
            get { return Properties.Settings.Default.GlobalSettingsRoles; }
        }
    }
}
