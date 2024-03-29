﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.Json;

namespace SCUMServerListener
{
    static internal class Configuration
    {
        private const string SettingsFileName = "Configuration\\appsettings.json";

        public static AppSettings Load() {
            IConfigurationRoot configuration = null;

            try
            {
                configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(SettingsFileName)
                    .Build();
            }
            catch (Exception ex)
            {
                if (ex is not FileNotFoundException)
                {
                    throw new Exception("Something went wrong");
                }
            }
            return configuration?.Get<AppSettings>() ?? new AppSettings();
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
            catch (Exception ex)
            {
                if (ex is UnauthorizedAccessException or DirectoryNotFoundException)
                {
                    // display error
                }
                return false;
            }

            return true;
        }
    }
}
