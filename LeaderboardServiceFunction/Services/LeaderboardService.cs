using System.Collections.Generic;
using System.Threading.Tasks;
using LeaderboardServiceFunction.Models;
using Microsoft.Azure.Cosmos;

namespace LeaderboardServiceFunction.Services
{

    public class LeaderboardService : ILeaderboardService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public LeaderboardService(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            _container = _cosmosClient.GetContainer("leaderboard-db", "leaderboard-container");
        }

        public async Task<bool> UploadDocumentAsync(MyDocument document)
        {
            try
            {
                await _container.CreateItemAsync(document, new PartitionKey(document.Username));
                return true;
            }
            catch (CosmosException _ex)
            {
                // Handle exceptions (e.g., log the error)
                return false;
            }
        }

        public async Task<IEnumerable<MyDocument>> GetLeaderboardEntriesAsync()
        {
            var query = "SELECT * FROM c";
            var queryDefinition = new QueryDefinition(query);
            var feedIterator = _container.GetItemQueryIterator<MyDocument>(queryDefinition);

            List<MyDocument> results = new List<MyDocument>();
            while (feedIterator.HasMoreResults)
            {
                var response = await feedIterator.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }
    }
}