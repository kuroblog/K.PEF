using K.PEF.Common;

namespace K.PEF.Shell
{
    public class ShellConfigManager : ConfigManager
    {
        public int Width => int.TryParse(ReadAppSetting(nameof(Width)), out int width) ? width : 1280;

        public int Height => int.TryParse(ReadAppSetting(nameof(Height)), out int height) ? height : 1024;

        //public string LogFolder => ReadAppSetting(LogFolder);
    }
}
