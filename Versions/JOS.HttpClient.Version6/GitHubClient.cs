﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using HttpClient.Common;

using Newtonsoft.Json;

namespace JOSHttpClient.Version6
{
    public class GitHubClient : IGitHubClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly JsonSerializer _jsonSerializer;

        public GitHubClient(System.Net.Http.HttpClient httpClient, JsonSerializer jsonSerializer)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
        }

        public async Task<IReadOnlyCollection<GitHubRepositoryDto>> GetRepositories()
        {
            var request = CreateRequest();
            var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);

            using (var responseStream = await result.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(responseStream))
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    return _jsonSerializer.Deserialize<List<GitHubRepositoryDto>>(jsonTextReader);
                }
            }
        }

        private static HttpRequestMessage CreateRequest()
        {
            return new HttpRequestMessage(HttpMethod.Get, GitHubConstants.RepositoriesPath);
        }
    }
}
