using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lenus.Samples.ClinicianOrg.Services
{
    public interface IOrganisationMembershipService
    {
        Task<IEnumerable<LenusOrganisationalMembership>> Retrieve(CancellationToken cancellationToken = default);
    }
}
