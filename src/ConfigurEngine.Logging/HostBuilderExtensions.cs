using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace ConfigurEngine
{
    public static class HostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureAppLogging(this IWebHostBuilder builder, string configSection = "Logging", bool enableConsole = true, bool enableDebug = true) =>
            builder.ConfigureLogging((ctx, factory) =>
            {
                factory.AddConfiguration(ctx.Configuration.GetSection(configSection));
                if (enableConsole) {
                    factory.AddConsole();
                }
                if (enableDebug) {
                    factory.AddDebug();
                }
            });
    }
}
