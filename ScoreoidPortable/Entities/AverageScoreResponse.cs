using Newtonsoft.Json;

namespace ScoreoidPortable.Entities
{
    internal class AverageScoreResponse
    {
        [JsonProperty("average_score")]
        public double AverageScore { get; set; }
    }
}