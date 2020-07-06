using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Tinderro.API.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System;

namespace Tinderro.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        
        // ta klasa oswieza ostatnie logowanie uzytkownika



        // ActionExecutingContext - parametr gdy cos chcemy zrobic PODCZAS wykonywania akcji
        // ActionExecutionDelegate - parametr ktory uruchamia kod po wykonaniu akcji
        // UWAGA trzeba to dodac to StartUp w serwisach services.AddScoped<LogUserActivity>();
        // UWAGA trzeba to dodac do Controllersa jako atrybut [ServiceFilter(typeof(LogUserActivity))]
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // wykorzystujemy tylko 2 parametr next i robimy cos po wykonaniu czegos wczesniej xd
            var resultContext = await next();

            // zeby nie bylo bledu to trzeba dodac using Microsoft.Extensions.DependencyInjection;
            // pobieramy UserRepository
            var repositorium = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();

            // pobiera id zalogowanego uzytkownika
            var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await repositorium.GetUser(userId);
            user.LastActive = DateTime.Now;

            await repositorium.SaveAll();
        }
    }
}