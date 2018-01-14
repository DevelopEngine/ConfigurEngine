using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ConfigurEngine
{
    public static class HostBuilderExtensions
    {
        internal static IWebHostBuilder ConfigureAppConfiguration(this IWebHostBuilder builder, string[] args, string variablePrefix = null) =>
            builder.ConfigureAppConfiguration((ctx, config) =>
            {
                var env = ctx.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                      .AddConfigFile("config");

                if (args != null && args.Any())
                {
                    config.AddCommandLine(args);
                }
                if (variablePrefix == null) {
                    config.AddEnvironmentVariables();
                } else {
                    config.AddEnvironmentVariables(variablePrefix);
                }
            });

        private static IConfigurationBuilder AddConfigFile(this IConfigurationBuilder config, string fileName)
        {
            fileName = fileName.Replace(".json", string.Empty).Replace(".yml", string.Empty);
            config.AddJsonFile($"{fileName}.json", optional: true, reloadOnChange: true);
            config.AddYamlFile($"{fileName}.yml", optional: true, reloadOnChange: true);
            return config;
        }
    }
}