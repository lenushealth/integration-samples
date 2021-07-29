using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MyBp.Controllers
{
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using System.Threading.Tasks;

    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        public async Task Login()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                await this.HttpContext.ChallengeAsync(new AuthenticationProperties()
                {
                    RedirectUri = this.Url.Action("Index")
                });
            }
        }

        [Route("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme );
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }

    }
}
