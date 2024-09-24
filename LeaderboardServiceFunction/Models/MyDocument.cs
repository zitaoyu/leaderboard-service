namespace LeaderboardServiceFunction.Models
{
    public class MyDocument
    {
        public string id { get; set; } = System.Guid.NewGuid().ToString(); // Auto-generate ID
        public string Username { get; set; } = string.Empty;
        public double Score { get; set; } // Changed to double for numerical values
        public int CompleteTimeInSec { get; set; }
        public string FormattedTime { get; set; } = string.Empty;
    }
}

