namespace MyBp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class HealthSample
    {
        public class HealthSampleDateRange
        {
            public HealthSampleDateRange()
            {
                
            }

            public HealthSampleDateRange(DateTimeOffset lowerBound, DateTimeOffset upperBound)
            {
                this.LowerBound = lowerBound;
                this.UpperBound = upperBound;
            }

            [Required]
            [DataType(DataType.DateTime)]
            public DateTimeOffset LowerBound { get; set; }
            
            [Required]
            [DataType(DataType.DateTime)]
            public DateTimeOffset UpperBound { get; set; }
        }

        public class HealthSampleDateRangeNotRequired : HealthSampleDateRange
        {
            [DataType(DataType.DateTime)]
            public new DateTimeOffset? LowerBound { get; set; }

            [DataType(DataType.DateTime)]
            public new DateTimeOffset? UpperBound { get; set; }
        }

        public string Device { get; set; }
        public string ClientAssignedId { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public decimal QuantityValue { get;set; }
        public string CategoryValue { get; set; }
        public IEnumerable<HealthSample> CorrelationObjects { get; set; } = new List<HealthSample>();

        public HealthSampleDateRange DateRange { get; set; }
    }
}