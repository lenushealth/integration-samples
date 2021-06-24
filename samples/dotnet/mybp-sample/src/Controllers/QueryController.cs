using System.Collections.Generic;

namespace MyBp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using Client;
    using Models;

    [Authorize(Policy = "Query")]
    public class QueryController : Controller
    {
        private readonly IHealthDataClient client;

        public QueryController(IHealthDataClient client)
        {
            this.client = client;
        }

        public IActionResult Index()
        {
            var model = new HealthDataQueryRequest(DateTimeOffset.UtcNow.AddMonths(-2), DateTimeOffset.UtcNow, "blood_pressure", "blood_pressure_diastolic", "blood_pressure_systolic");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(HealthDataQueryRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(request);
            }

            try
            {
                var requestDateParams = new Dictionary<string, object>
                {
                    {"RangeOfStartDate", request.RangeOfStartDate},
                    {"RangeOfEndDate", request.RangeOfEndDate},
                    {"RangeOfCreationDate", request.RangeOfCreationDate}
                };

                var queryResult = await this.client.ExecuteQueryAsync(request.Types, requestDateParams);

                var model = queryResult.Datas.Select(s =>
                {
                    if (s?.CorrelationObjects?.Any() ?? false)
                    {
                        var reading = new BloodPressureSampleModel()
                        {
                            From = s.DateRange.LowerBound,
                            Diastolic = s.CorrelationObjects.SingleOrDefault(x => x.Type == "blood_pressure_diastolic")
                                .QuantityValue,
                            Systolic = s.CorrelationObjects.SingleOrDefault(x => x.Type == "blood_pressure_systolic")
                                .QuantityValue
                        };
                        return reading;
                    }

                    return null;
                }).Where(s => s != null).OrderByDescending(s => s.From).ToList();

                return View("QueryResults", model);
            }
            catch (HealthDataClientException exception)
            {
                return this.View("ProblemDetails", exception.ProblemDetails);
            }
        }
    }
}
