using System;

namespace Tinderro.API.Helpers
{
    public static class Exnensions
    {
        public static int CalculateAge(this DateTime datetime) // datetime bedzie przesylany z automappera z helpers
        {
            var age = DateTime.Today.Year - datetime.Year;

            if (datetime.AddYears(age) > DateTime.Today)
                age --;

            return age;
        }
    }
}