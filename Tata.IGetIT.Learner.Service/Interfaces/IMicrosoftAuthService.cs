using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    /// <summary>
    /// Microsoft Authentication Service to generate OAuth token.
    /// </summary>
    public interface IMicrosoftAuthService
    {
        /// <summary>
        /// Update HttpRequestMessage with OAuth token.
        /// </summary>
        Task AuthenticateRequestAsync(HttpRequestMessage request);

        /// <summary>
        /// Get Token 
        /// </summary>
        Task<string> GetTokenAsync();
    }
}
