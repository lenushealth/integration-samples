using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lenus.Samples.ClinicianOrg.Services
{

    public interface IAgencyInviteService
    {
        Task SendInvite(string? emailAddress, string? mobileNumber, IEnumerable<string> scopes, string? organisationId, CancellationToken cancellationToken);
    }
}
