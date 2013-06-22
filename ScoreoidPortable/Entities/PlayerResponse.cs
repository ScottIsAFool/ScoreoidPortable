using Newtonsoft.Json;

namespace ScoreoidPortable.Entities
{
    internal class PlayerResponse
    {
        [JsonProperty("Player")]
        public Player Player { get; set; }
    }
}