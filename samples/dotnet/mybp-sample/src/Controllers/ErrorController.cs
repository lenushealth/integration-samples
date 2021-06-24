using Microsoft.AspNetCore.Mvc;

namespace MyBp.Controllers
{
    using Models;

    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            if (this.HttpContext.Items.TryGetValue("_problem", out var problemObject) &&
                problemObject is ProblemDetails problem)
            {
                return this.View("ProblemDetails", problem);
            }

            return this.View("Error", new ErrorViewModel());
        }
    }
}
