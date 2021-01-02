using K.PEF.Common;

namespace K.PEF.Modules.Tools
{
    public class ToolsConfigManager : ConfigManager
    {
        public string Version => ReadAppSetting(nameof(Version));
    }
}
