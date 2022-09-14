using Lenus.Samples.ClientCredentialsFlow.Configuration;
using Lenus.Samples.ClientCredentialsFlow.Services.Agency.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Lenus.Samples.ClientCredentialsFlow.Pages
{
    public class IndexModel : PageModel
    {
        [Display(Description = "Request access to specific patient health data types")]
        public class Model : IValidatableObject
        {
            [EmailAddress]
            [Display(Name = "Patient Email Address", Description = "(Optional) This is the email address to which the agency request invitation email is sent", Prompt = "patient@lenushealth.com")]
            public string EmailAddress { get; set; } = String.Empty;

            [Phone]
            [Display(Name = "Patient Mobile Number", Description = "(Optional) This is the mobile number to which the agency request invitation SMS is sent", Prompt = "+44111222333444")]
            public string MobileNumber { get; set; } = String.Empty;

            [DataType(DataType.MultilineText)]
            [Display(Name = "Requested health data scopes", Description = "Specify the read and/or write scopes you want the user to consent access to, e.g. read.step_count, read.heart_rate", Prompt = "read.step_count")]
            [Required]
            public string Scopes { get; set; } = String.Empty;

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

        private readonly IAgencyInviteService agencyInviteService;
        private readonly IOptions<AgencyOptions> agencyOptions;
        private readonly ILogger<IndexModel> logger;

        public IndexModel(IAgencyInviteService agencyInviteService, IOptions<AgencyOptions> agencyOptions, ILogger<IndexModel> logger)
        {
            this.agencyInviteService = agencyInviteService;
            this.agencyOptions = agencyOptions;
            this.logger = logger;
        }
        public void OnGet()
        {
           
        }

        public async Task OnPostAsync(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var request = new AgencyInviteRequest(Form.EmailAddress, Form.MobileNumber, Form.ScopesList, agencyOptions.Value.OrganisationId, agencyOptions.Value.ClientNotifyPath, agencyOptions.Value.BrowserRedirectPath);
                var response = await agencyInviteService.SendInvite(request, cancellationToken);
                if(response.IsSuccessStatusCode)
                {
                    Submitted = true;
                }
                else
                {
                    logger.LogError($"Send agency invite returned not successful status code: {response.ReasonPhrase}");
                }
            }
        }
    }
}
