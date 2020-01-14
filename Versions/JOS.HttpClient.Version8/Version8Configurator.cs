﻿using System;

using HttpClient.Common;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace JOSHttpClient.Version8
{
    public static class Version8Configurator
    {
        public static void AddVersion8(this IServiceCollection services)
        {
            services.AddSingleton<GetAllProjectsQuery>();
            services.AddHttpClient<GitHubClient>(
                "GitHubClient.Version8",
                x => { x.BaseAddress = new Uri(GitHubConstants.ApiBaseUrl); });
            services.AddSingleton<GitHubClientFactory>();
            services.AddSingleton<JsonSerializer>();
            services.AddSingleton<ProjectDeserializer>();
        }
    }
}