﻿//-----------------------------------------------------------------------
// <copyright file="Github.cs" company="TemporalCohesion.co.uk">
//     Copyright [2010] [Stuart Grassie]
//
//     Licensed under the Apache License, Version 2.0 (the "License");
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.
// </copyright>
//----------------------------------------------------------------------

using System;

namespace csharp_github_api
{
    using Core;
    using Framework;
    using RestSharp;

    /// <summary>
    /// Access the Github.com API.
    /// </summary>
    public class Github
    {
        private readonly IAuthenticator _gitHubAuthenticator;
        private readonly IGitHubApiSettings _gitHubApiSettings;
        private string _baseUrl;

        private UserApi _userApi;

        /// <summary>
        /// Instantiates a new instance of the <see cref="Github"/> class.
        /// </summary>
        /// <param name="gitHubApiSettings">The settings provider which provides the initial configuration for the API.</param>
        public Github(IGitHubApiSettings gitHubApiSettings)
        {
            if(gitHubApiSettings == null) throw new ArgumentNullException("gitHubApiSettings", "The Github settings provider cannot be null.");
            _gitHubApiSettings = gitHubApiSettings;
            _baseUrl = gitHubApiSettings.BaseUrl;

            _gitHubAuthenticator = string.IsNullOrEmpty(gitHubApiSettings.Token) ? new GitHubAuthenticator(gitHubApiSettings.Username, gitHubApiSettings.Password, false) : new GitHubAuthenticator(gitHubApiSettings.Username, gitHubApiSettings.Token, true);
        }

        /// <summary>
        /// Instantiates a new instance of the <see cref="Github"/> class.
        /// </summary>
        /// <param name="baseUrl">The base url for GitHub's API.</param>
        /// <param name="username">The username to authenticate as.</param>
        /// <param name="apiKey">Indicates whether or not to use a Github.com API token. If <c>true</c>, then pass the token instead of the password.</param>
        public Github(string baseUrl, string username, string apiKey)
        {
            _gitHubApiSettings = new GitHubApiSettings
                                     {
                                         BaseUrl = baseUrl,
                                         Username = username,
                                         Token = apiKey,
                                         Password = string.Empty
                                     };
            _gitHubAuthenticator = new GitHubAuthenticator(username, apiKey, true);
        }

        /// <summary>
        /// Gets or sets the base URL for accessing GitHub's API.
        /// </summary>
        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        public UserApi User
        {
            get
            {
                return new UserApi(_baseUrl, _gitHubAuthenticator);
            }
        }
    }
}
