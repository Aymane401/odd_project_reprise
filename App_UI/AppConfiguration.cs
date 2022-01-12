using Microsoft.Extensions.Configuration;
using System;

namespace App_UI
{
    public static class AppConfiguration
    {

        private static IConfiguration Configuration;


        public static string GetValue(string key)
        {
            if (Configuration == null)
            {
                initConfig();
            }

            return Configuration[key];
        }

        private static void initConfig()
        {
            
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json",
                optional: true,
                reloadOnChange: true);

            var devEnvVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var isDevelopment = string.IsNullOrEmpty(devEnvVariable) ||
                                    devEnvVariable.ToLower() == "development";

            if (isDevelopment)
            {
                builder.AddUserSecrets("e0a86c66-f550-437b-a2c9-3260f364ea05", true);
            }

            Configuration = builder.Build();
        }
    }
}
