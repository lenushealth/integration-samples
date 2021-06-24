﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace Clinician.Models
{
    public class AgencySubjectQueryParameters
    {
        [FromQuery(Name = "subject")]
        public Guid Subject { get;set; }
        [FromQuery(Name = "from")]
        public DateTimeOffset From { get; set; } = DateTimeOffset.UtcNow.Date;
        [FromQuery(Name = "to")]
        public DateTimeOffset To { get; set; } = DateTimeOffset.UtcNow.Date.AddDays(1).AddSeconds(-1);
        [FromQuery(Name = "take")]
        public int Take { get; set; } = 100;
    }
}