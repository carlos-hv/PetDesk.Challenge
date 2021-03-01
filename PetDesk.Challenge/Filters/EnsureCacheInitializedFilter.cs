using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetDesk.Challenge.Services.Caching;

namespace PetDesk.Challenge.WebApi.Filters
{
    internal class EnsureCacheInitializedFilter : IActionFilter
    {
        private readonly FlightDataCache _cache;

        public EnsureCacheInitializedFilter(FlightDataCache cache)
        {
            _cache = cache;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_cache.IsInitialized)
                context.Result = new ConflictObjectResult("API cache is initializing, please wait");
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
