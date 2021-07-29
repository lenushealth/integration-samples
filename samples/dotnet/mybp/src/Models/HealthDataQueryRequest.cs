namespace MyBp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class HealthDataQueryRequest
    {
        [Required]
        [UIHint("CorrelationTypes")]
        public IEnumerable<string> Types { get; set; }
        
        [Required]
        public HealthSample.HealthSampleDateRange RangeOfStartDate { get; set; }

        public HealthSample.HealthSampleDateRangeNotRequired RangeOfEndDate { get; set; }

        public HealthSample.HealthSampleDateRangeNotRequired RangeOfCreationDate { get; set; }

        public HealthDataQueryRequest()
        {
            
        }

        public HealthDataQueryRequest(HealthSample.HealthSampleDateRange dateRange, params string[] types)
        {
            this.Types = types;
            this.RangeOfStartDate = dateRange;
        }

        public HealthDataQueryRequest(DateTimeOffset lowerBound, DateTimeOffset upperBound, params string[] types) : this(new HealthSample.HealthSampleDateRange(lowerBound, upperBound), types)
        {
            
        }
    }
}
