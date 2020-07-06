using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error"); // "Application-Error" ma sie zgadzac z nazwa Application-Error o linijke wyzej
            response.Headers.Add("Access-Control-Allow-Orgin", "*");
        }

        public static void AddPagination(this HttpResponse response, int currentPage, int itemPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemPerPage, totalItems, totalPages);

            // trzeba zrobic male literki, "camel formation" czy cos takiego
            // te 2 linijki to robia XD i trzeba jeszcze przekazac to do JsonConvert.Serialize.... (tam nizej :D)
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // "Pagination" - to my wysmyslamy jakis klucz/naglowek
            // JsonConvert.SerializeObject to parsikujemy na stringi z obiektu
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination"); // nazwy Pagination musza sie zgadzac (ta linijka i linijka wyzej)
        }
    }
}