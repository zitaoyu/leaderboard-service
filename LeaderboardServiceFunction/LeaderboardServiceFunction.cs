using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using LeaderboardServiceFunction.Models;
using LeaderboardServiceFunction.Services;

namespace LeaderboardServiceFunction.Functions
{
    public class LeaderboardFunction
    {
        private readonly ILeaderboardService _leaderboardService;
        private readonly ILogger<LeaderboardFunction> _logger;

        public LeaderboardFunction(ILeaderboardService leaderboardService, ILogger<LeaderboardFunction> logger)
        {
            _leaderboardService = leaderboardService;
            _logger = logger;
        }

        // POST Function for uploading data
        [Function("upload")]
        public async Task<HttpResponseData> UploadScore(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var requestBody = await req.ReadAsStringAsync();
            var newDocument = JsonSerializer.Deserialize<MyDocument>(requestBody);

            if (newDocument == null)
            {
                var errorResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await errorResponse.WriteStringAsync("{\"error\": \"Invalid input data.\"}");
                return errorResponse;
            }

            var result = await _leaderboardService.UploadDocumentAsync(newDocument);

            var response = req.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
            await response.WriteStringAsync(result ? "Upload successful" : "Failed to upload data");

            return response;
        }

        // GET Function for retrieving leaderboard entries
        [Function("getScores")]
        public async Task<HttpResponseData> GetScores(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var documents = await _leaderboardService.GetLeaderboardEntriesAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            await response.WriteStringAsync(JsonSerializer.Serialize(documents));

            return response;
        }
    }
}
