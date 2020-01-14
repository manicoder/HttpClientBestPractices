﻿using System;

using HttpClient.Common;

using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace JOSHttpClient.Version7
{
    public static class Version7Configurator
    {
        public static void AddVersion7(this IServiceCollection services)
        {
            services.AddSingleton<GetAllProjectsQuery>();
            services.AddHttpClient<GitHubClient>("GitHubClient.Version7", x => { x.BaseAddress = new Uri(GitHubConstants.ApiBaseUrl); });
            services.AddSingleton<GitHubClientFactory>();
            services.AddSingleton<JsonSerializer>();
        }
    }
}
