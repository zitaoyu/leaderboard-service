using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.CosmosDB;

namespace LeaderboardServiceFunction.Models
{
    public class MultiResponse
    {
        [CosmosDBOutput("leaderboard-db", "leaderboard-container",
            Connection = "CosmosDbConnectionSetting", CreateIfNotExists = true)]
        public MyDocument? Document { get; set; }

        public HttpResponseData? HttpResponse { get; set; }
    }
}