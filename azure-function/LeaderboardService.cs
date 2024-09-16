using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LB.Function
{
    public class LeaderboardService
    {
        private readonly ILogger<LeaderboardService> _logger;

        public LeaderboardService(ILogger<LeaderboardService> logger)
        {
            _logger = logger;
        }

        [Function("LeaderboardService")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
