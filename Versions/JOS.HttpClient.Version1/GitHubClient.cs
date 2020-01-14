﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using HttpClient.Common;

using Newtonsoft.Json;

namespace JOSHttpClient.Version1
{
    public class GitHubClient : IGitHubClient
    {
        public async Task<IReadOnlyCollection<GitHubRepositoryDto>> GetRepositories()
        {
            using (var httpClient = new System.Net.Http.HttpClient { BaseAddress = new Uri(GitHubConstants.ApiBaseUrl) })
            {
                var result = await httpClient.GetStringAsync(GitHubConstants.RepositoriesPath).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<GitHubRepositoryDto>>(result);
            }
        }
    }
}
