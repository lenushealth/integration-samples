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

        [Display(Description = "Request access to specific patient health data types")]
        public class Model : IValidatableObject
        {
            [EmailAddress]
            [Display(Name = "Patient Email Address", Description = "(Optional) This is the email address to which the agency request invitation email is sent", Prompt = "patient@lenushealth.com")]
            public string? EmailAddress { get; set; }

            [Phone]
            [Display(Name = "Patient Mobile Number", Description = "(Optional) This is the mobile number to which the agency request invitation SMS is sent", Prompt = "+44111222333444")]
            public string? MobileNumber { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Requested health data scopes", Description = "Specify the read and/or write scopes you want the user to consent access to, e.g. read.step_count, read.heart_rate", Prompt = "read.step_count")]
            [Required]
            public string? Scopes { get; set; }

            [BindProperty]
            [Display(Name = "Organisation Id", Description = "(Optional) If a known organisation reference is supplied then the consent request will be made on behalf of the organisation, otherwise consent is requested only for the individual agent", Prompt = "00000000-0000-0000-0000-000000000000")]
            public Guid? OrganisationId { get; set; }

            [ScaffoldColumn(false)]
            public IEnumerable<string> ScopesList => Scopes?.Split(Environment.NewLine) ?? Enumerable.Empty<string>();

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (string.IsNullOrWhiteSpace(EmailAddress) && string.IsNullOrWhiteSpace(MobileNumber))
                {
                    yield return new ValidationResult($"One of email or mobile phone number must be supplied", new[] { nameof(EmailAddress), nameof(MobileNumber) });
                }
            }
        }

        [BindProperty]
        [UIHint("Form")]
        public Model Form { get; set; } = new Model();

        [ScaffoldColumn(false)]
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
