﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using HttpClient.Common;

using Newtonsoft.Json;

namespace JOSHttpClient.Version8
{
    public class GitHubClient : IGitHubClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly ProjectDeserializer _projectDeserializer;

        public GitHubClient(System.Net.Http.HttpClient httpClient, ProjectDeserializer projectDeserializer)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _projectDeserializer = projectDeserializer ?? throw new ArgumentNullException(nameof(projectDeserializer));
        }

        public async Task<IReadOnlyCollection<GitHubRepositoryDto>> GetRepositories()
        {
            var request = CreateRequest();
            var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            using (var streamReader = new StreamReader(await result.Content.ReadAsStreamAsync()))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return _projectDeserializer.Deserialize(jsonTextReader);
            }
        }

        private static HttpRequestMessage CreateRequest()
        {
            return new HttpRequestMessage(HttpMethod.Get, GitHubConstants.RepositoriesPath);
        }
    }
}
