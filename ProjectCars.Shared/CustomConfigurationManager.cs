using System;
using System.Configuration;

// Author: Stuart Leeks
// https://gist.github.com/stuartleeks/e356956ebf5f1e38718c
namespace ProjectCars.Shared
{

    public static class CustomConfigurationManager
    {
        private static CustomAppSettings _appSettings = new CustomAppSettings();
        public static CustomAppSettings AppSettings { get { return _appSettings; } }
    }
    public class CustomAppSettings
    {
        const string AppSettingsPrefix = "APPSETTING_";
        public string AppPrefix { get; set; }

        public string Get(string key)
        {
            return this[key];
        }

        public string this[string key]
        {
            get
            {
                string fullKey;
                if (!string.IsNullOrEmpty(AppPrefix))
                {
                    fullKey = AppPrefix + "_" + key;
                }
                else
                {
                    fullKey = key;
                }
                string value = Environment.GetEnvironmentVariable(AppSettingsPrefix + fullKey)
                                ?? Environment.GetEnvironmentVariable(fullKey, EnvironmentVariableTarget.User)
                                ?? ConfigurationManager.AppSettings[fullKey]
                                ?? ConfigurationManager.AppSettings[key];

                return value;
            }
        }
    }

}
