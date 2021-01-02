using K.PEF.Common;

namespace K.PEF.Modules.Main
{
    public class MainConfigManager : ConfigManager
    {
        public string Version => ReadAppSetting(nameof(Version));
    }
}
