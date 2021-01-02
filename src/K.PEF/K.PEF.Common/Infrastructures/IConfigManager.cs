namespace K.PEF.Common.Infrastructures
{
    public interface IConfigManager
    {
        string ReadAppSetting(string key);

        void SaveAppSetting(string key, string value);

        string ReadConnectionString(string key);

        string ReadAllText(string path);
    }
}
