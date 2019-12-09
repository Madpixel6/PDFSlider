using System.Configuration;

namespace PDFSlider.Services
{
    public static class CfgService
    {
        private static Configuration configFile;
        static CfgService()
        {
            configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
        public static bool ReadPropertyParseBool(string key)
        {
            return bool.Parse(ConfigurationManager.AppSettings[key]);
        }

        public static int ReadPropertyParseInt(string key)
        {
            return int.Parse(ConfigurationManager.AppSettings[key]);
        }
        public static string ReadProperty(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void SaveProperty(string key, object value)
        {
            var settings = configFile.AppSettings.Settings;
            if (configFile != null && settings != null)
            {
                if (settings[key] == null)
                    settings.Add(key, value.ToString());
                else
                    settings[key].Value = value.ToString();
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
        }
    }
}
