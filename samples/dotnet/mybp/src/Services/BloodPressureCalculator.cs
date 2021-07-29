namespace MyBp.Services
{
    using System;
    
    //public static class BloodPressureCalculator
    //{
    //    public static int ConvertToMmHg(decimal value)
    //    {
    //        return (int)Math.Round((value * 0.00750061683m), 0);
    //    }

    //    public static decimal ConvertToPascals(decimal value)
    //    {
    //        return value / 0.00750061683m;
    //    }

    //    public static BloodPressureUnitCategory CategoriseSystolicValue(decimal valueInPascals)
    //    {
    //        var systolic = ConvertToMmHg(valueInPascals);
    //        if (systolic < 90)
    //        {
    //            return BloodPressureUnitCategory.Low;
    //        }

    //        if (systolic > 140)
    //        {
    //            return BloodPressureUnitCategory.High;
    //        }

    //        if (systolic > 120)
    //        {
    //            return BloodPressureUnitCategory.PreHigh;
    //        }

    //        return BloodPressureUnitCategory.Normal;
    //    }

    //    public static BloodPressureUnitCategory CategoriseDiastolicValue(decimal valueInPascals)
    //    {
    //        var diastolic = ConvertToMmHg(valueInPascals);
    //        if (diastolic < 60)
    //        {
    //            return BloodPressureUnitCategory.Low;
    //        }

    //        if (diastolic > 90)
    //        {
    //            return BloodPressureUnitCategory.High;
    //        }

    //        if (diastolic > 80)
    //        {
    //            return BloodPressureUnitCategory.PreHigh;
    //        }

    //        return BloodPressureUnitCategory.Normal;
    //    }

    //    public static (BloodPressureUnitCategory SystolicCategory, BloodPressureUnitCategory DiastolicCategory) CategoriseBloodPressureReading(decimal systolicInPascals, decimal diastolicInPascals)
    //    {
    //        return (CategoriseSystolicValue(systolicInPascals), CategoriseDiastolicValue(diastolicInPascals));
    //    }
    //}
}
