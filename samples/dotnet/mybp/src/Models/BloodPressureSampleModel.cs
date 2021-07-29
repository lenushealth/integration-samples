namespace MyBp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BloodPressureSampleSubmitModel
    {
        [DisplayFormat(DataFormatString = "{0:G}")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTimeOffset From { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:N}")]
        [Required]
        [Range(0, 500)]
        public int Systolic { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:N}")]
        [Required]
        [Range(0, 500)]
        public int Diastolic { get; set; }
    }

    public class BloodPressureSampleModel
    {
        [DisplayFormat(DataFormatString = "{0:G}")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTimeOffset From { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:N}")]
        [Required]
        public decimal Systolic { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:N}")]
        [Required]
        public decimal Diastolic { get; set; }
    }
}
