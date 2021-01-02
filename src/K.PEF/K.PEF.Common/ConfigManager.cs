using K.PEF.Common.Infrastructures;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace K.PEF.Common
{
    public class ConfigManager : IConfigManager
    {
        public virtual string ReadAppSetting(string key)
        {
            return ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
            {
                ExeConfigFilename = string.Concat(Assembly.GetCallingAssembly().Location, ".config")
            }, ConfigurationUserLevel.None).AppSettings.Settings[key].Value;
        }

        public virtual void SaveAppSetting(string key, string value)
        {
            var config = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
            {
                ExeConfigFilename = string.Concat(Assembly.GetCallingAssembly().Location, ".config")
            }, ConfigurationUserLevel.None);

            if (config.AppSettings.Settings.AllKeys.Contains(key))
            {
                var result = config.AppSettings.Settings[key].Value;
                if (result != value)
                {
                    config.AppSettings.Settings[key].Value = value;
                    config.Save(ConfigurationSaveMode.Modified);
                }
            }
            else
            {
                config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Modified);
            }
        }

        public string ReadConnectionString(string key) => ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
        {
            ExeConfigFilename = string.Concat(Assembly.GetCallingAssembly().Location, ".config")
        }, ConfigurationUserLevel.None).ConnectionStrings.ConnectionStrings[key].ConnectionString;

        public string ReadAllText(string path) => File.ReadAllText(path);
    }
}
