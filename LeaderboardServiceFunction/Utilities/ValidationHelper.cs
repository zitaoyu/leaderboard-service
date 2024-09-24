using LeaderboardServiceFunction.Models;

namespace LeaderboardServiceFunction.Utilities
{
    public static class ValidationHelper
    {
        public static bool IsValidDocument(MyDocument? document)
        {
            if (document == null ||
                string.IsNullOrEmpty(document.Username) ||
                document.Score < 0 ||
                document.CompleteTimeInSec < 0 ||
                string.IsNullOrEmpty(document.FormattedTime))
            {
                return false;
            }
            return true;
        }
    }
}
