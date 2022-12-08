using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;

namespace SCUMServerListener
{
    static internal class Configuration
    {
        private const string SettingsFileName = "appsettings.json";

        public static AppSettings Load() {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(SettingsFileName)
                .Build();

            if (configuration is null) throw new System.NullReferenceException("Configuration was null");

            return configuration.Get<AppSettings>() ?? new AppSettings();
        }

        public static bool Save(AppSettings appSettings)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(appSettings, options);

            try
            {
                if (!File.Exists(SettingsFileName))
                {
                    File.Create(SettingsFileName).Close();
                }
                using var writer = new StreamWriter(SettingsFileName);
                writer.Write(json);
            }
            catch (System.Exception ex)
            {
                if (ex is System.UnauthorizedAccessException or System.IO.DirectoryNotFoundException)
                {
                    // display error
                }
                return false;
            }

            return true;
        }
    }
}
