using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    /// <summary>
    /// Microsoft Authentication Service to generate OAuth token.
    /// </summary>
    public class MicrosoftAuthService : IMicrosoftAuthService
    {
        private IConfidentialClientApplication _client;
        private string[] _scopes;

        public MicrosoftAuthService(IConfidentialClientApplication clientApplication, string[] scopes)
        {
            _client = clientApplication;
            _scopes = scopes;
        }

        /// <summary>
        /// Update HttpRequestMessage with credentials
        /// </summary>
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var token = await GetTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        /// <summary>
        /// Acquire Token 
        /// </summary>
        public async Task<string> GetTokenAsync()
        {
            var authResult = await _client.AcquireTokenForClient(_scopes).ExecuteAsync();
            return authResult.AccessToken;
        }
    }
}
