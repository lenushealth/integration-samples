using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lenus.Samples.ClinicianOrg.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lenus.Samples.ClinicianOrg.Pages.Invite
{
    public class IndexModel : PageModel
    {
        private readonly IAgencyInviteService agencyInviteService;

        public class Model
        {
            [DataType(DataType.EmailAddress)]
            public string? EmailAddress { get; set; }
            
            [DataType(DataType.PhoneNumber)]
            public string? MobileNumber { get; set; }

            [DataType(DataType.MultilineText)]
            public string? Scopes { get; set; }

            [DataType(DataType.Text)]
            public string? OrganisationId { get; set; }

            public IEnumerable<string> ScopesList => Scopes?.Split(Environment.NewLine) ?? Enumerable.Empty<string>();
        }

        [BindProperty]
        public Model Form { get; set; } = new Model();

        public bool Submitted { get; set; } = false;

        public IndexModel(IAgencyInviteService agencyInviteService)
        {
            this.agencyInviteService = agencyInviteService;
        }

        public void OnGet()
        {
        }

        public async Task OnPost(CancellationToken cancellationToken)
        {
            if(ModelState.IsValid)
            {
                await agencyInviteService.SendInvite(Form.EmailAddress, Form.MobileNumber, Form.ScopesList, Form.OrganisationId, cancellationToken);
                Submitted = true;
            }
        }
    }
}
