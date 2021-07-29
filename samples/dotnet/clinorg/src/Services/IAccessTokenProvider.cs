using System.Threading;
using System.Threading.Tasks;

namespace Lenus.Samples.ClinicianOrg.Services
{
    public interface IAccessTokenProvider
    {
        Task<string?> GetAccessToken(CancellationToken cancellationToken = default);
    }
}
