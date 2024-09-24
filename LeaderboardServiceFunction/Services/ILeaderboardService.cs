using System.Collections.Generic;
using System.Threading.Tasks;
using LeaderboardServiceFunction.Models;

namespace LeaderboardServiceFunction.Services
{

    public interface ILeaderboardService
    {
        Task<bool> UploadDocumentAsync(MyDocument document);
        Task<IEnumerable<MyDocument>> GetLeaderboardEntriesAsync();
    }
}