﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lenus.Samples.ClinicianOrg.Services
{

    public interface IAgencyInviteService
    {
        Task SendInvite(string? emailAddress, string? mobileNumber, IEnumerable<string> scopes, Guid? organisationId, CancellationToken cancellationToken);
    }
}
