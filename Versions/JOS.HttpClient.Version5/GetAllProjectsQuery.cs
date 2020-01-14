﻿using HttpClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOSHttpClient.Version5
{
    public class GetAllProjectsQuery : IGetAllProjectsQuery
    {
        private readonly GitHubClientFactory _gitHubClientFactory;

        public GetAllProjectsQuery(GitHubClientFactory gitHubClientFactory)
        {
            _gitHubClientFactory = gitHubClientFactory ?? throw new ArgumentNullException(nameof(gitHubClientFactory));
        }

        public async Task<IReadOnlyCollection<Project>> Execute()
        {
            var gitHubClient = _gitHubClientFactory.Create();
            var response = await gitHubClient.GetRepositories().ConfigureAwait(false);
            return response.Select(x => new Project(x.Name, x.Url, x.Stars)).ToArray();
        }
    }
}
