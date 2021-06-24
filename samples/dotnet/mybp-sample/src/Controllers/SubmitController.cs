using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBp.Controllers
{
    using Client;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Authorize(Policy = "Submit")]
    public class SubmitController : Controller
    {
        private readonly IHealthDataClient client;

        public SubmitController(IHealthDataClient client)
        {
            this.client = client;
        }

        public IActionResult Index()
        {
            var model = new BloodPressureSampleSubmitModel()
            {
                From = DateTimeOffset.UtcNow
            };
            return View(model);
        }

        private HealthSample CreateBloodPressureSample(DateTimeOffset date, BloodPressureUnit systolic, BloodPressureUnit diastolic)
        {
            var dateRange = new HealthSample.HealthSampleDateRange(date, date);
            var sample = new HealthSample()
            {
                DateRange = dateRange,
                Type = "blood_pressure",
                ClientAssignedId = Guid.NewGuid().ToString(),
                CorrelationObjects = new List<HealthSample>()
                {
                    new HealthSample()
                    {
                        DateRange = dateRange,
                        Type = "blood_pressure_systolic",
                        QuantityValue = systolic,
                        ClientAssignedId = Guid.NewGuid().ToString()
                    },
                    new HealthSample()
                    {
                        DateRange = dateRange,
                        Type = "blood_pressure_diastolic",
                        QuantityValue = diastolic,
                        ClientAssignedId = Guid.NewGuid().ToString()
                    }
                }
            };
            return sample;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(BloodPressureSampleSubmitModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var sample = CreateBloodPressureSample(model.From, model.Systolic, model.Diastolic);

            try
            {
                await client.SubmitBloodPressureMeasurementAsync(sample);
                return RedirectToAction("Index", "Query");
            }
            catch (HealthDataClientException exception)
            {
                return this.View("ProblemDetails", exception.ProblemDetails);
            }
        }
    }
}
