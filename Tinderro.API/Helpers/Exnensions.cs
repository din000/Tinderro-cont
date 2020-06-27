using System;
using Microsoft.AspNetCore.Http;

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

        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Orgin", "*");
        }
    }
}