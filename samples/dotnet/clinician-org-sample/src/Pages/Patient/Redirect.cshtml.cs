using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lenus.Samples.ClinicianOrg.Pages.Patient
{
    public class RedirectModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Subject { get; set; } = "N/A";

        [BindProperty(SupportsGet = true)]
        public string? State { get; set; } = "N/A";

        public void OnGet()
        {
        }
    }
}
