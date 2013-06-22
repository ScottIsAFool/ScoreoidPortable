using Newtonsoft.Json;

namespace ScoreoidPortable.Entities
{
    internal class ScoreCountResponse
    {
        [JsonProperty("scores")]
        public double ScoreCount { get; set; }
    }
}