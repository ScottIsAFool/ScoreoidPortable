using Newtonsoft.Json;

namespace ScoreoidPortable.Entities
{
    internal class ScoreResponse
    {
        [JsonProperty("Score")]
        public Score Scores { get; set; }
    }
}