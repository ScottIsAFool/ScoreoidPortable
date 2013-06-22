using System;
using Newtonsoft.Json;

namespace ScoreoidPortable.Entities
{
    public class Game
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("game_type")]
        public string GameType { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("levels")]
        public int Levels { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("play_url")]
        public string PlayUrl { get; set; }

        [JsonProperty("website_url")]
        public string WebsiteUrl { get; set; }

        [JsonProperty("players_count")]
        public int PlayersCount { get; set; }

        [JsonProperty("scores_count")]
        public int ScoresCount { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("created")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updated")]
        public DateTime UpdatedDate { get; set; }
    }
}