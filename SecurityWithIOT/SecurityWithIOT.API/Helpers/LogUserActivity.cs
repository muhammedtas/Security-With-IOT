using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using SecurityWithIOT.API.Model.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SecurityWithIOT.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) // 134
        {

            var resultContext = await next();

            var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var repo = resultContext.HttpContext.RequestServices.GetService<IUser>(); // Dependencyinjection eklendi.
            var user = await repo.GetAsync(userId);
            user.LastEnterance = DateTime.Now;
            await repo.SaveAsync();
        }
    }
}