namespace MyBp.Models
{
    using System;

    public class HealthDataQueryResponse
    {
        public int NumberOfResults { get; set; }
        
        public DateTimeOffset ExpirationDate { get; set; }

        public string QueryKey { get; set; }

        public bool IsValid()
        {
            return DateTimeOffset.UtcNow < this.ExpirationDate;
        }
    }
}