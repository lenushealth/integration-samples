using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Lenus.Samples.ClinicianOrg.Services.Impl
{
    public class AccessTokenProvider : IAccessTokenProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccessTokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string?> GetAccessToken(CancellationToken cancellationToken = default)
        {
            if(httpContextAccessor == null || httpContextAccessor.HttpContext == null)
            {
                return null;
            }

            return await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        }
    }
}
