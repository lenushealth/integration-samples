using System.Collections.Generic;

namespace MyBp.Models
{
    public class HealthSamplesDto
    {
        public int TotalAvailable { get; set; }

        public int Count { get; set; }

        public IList<HealthSample> Datas { get; set; }
    }
}
