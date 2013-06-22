using Newtonsoft.Json;

namespace ScoreoidPortable.Entities
{
    internal class PlayerRankResponse
    {
        [JsonProperty("rank")]
        public int Rank { get; set; }
    }
}