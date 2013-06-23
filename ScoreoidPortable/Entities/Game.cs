using System;
using Newtonsoft.Json;
using PropertyChanged;
using ScoreoidPortable.Attributes;

namespace ScoreoidPortable.Entities
{
    [ImplementPropertyChanged]
    public class Game
    {
        [JsonProperty("user_id"), Description("user_id")]
        public string UserId { get; set; }

        [JsonProperty("name"), Description("name")]
        public string Name { get; set; }

        [JsonProperty("short_description"), Description("short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty("description"), Description("description")]
        public string Description { get; set; }

        [JsonProperty("game_type"), Description("game_type")]
        public string GameType { get; set; }

        [JsonProperty("version"), Description("version")]
        public string Version { get; set; }

        [JsonProperty("levels"), Description("levels")]
        public int Levels { get; set; }

        [JsonProperty("platform"), Description("platform")]
        public string Platform { get; set; }

        [JsonProperty("play_url"), Description("play_url")]
        public string PlayUrl { get; set; }

        [JsonProperty("website_url"), Description("website_url")]
        public string WebsiteUrl { get; set; }

        [JsonProperty("players_count"), Description("players_count")]
        public int PlayersCount { get; set; }

        [JsonProperty("scores_count"), Description("scores_count")]
        public int ScoresCount { get; set; }

        [JsonProperty("status"), Description("status")]
        public int Status { get; set; }

        [JsonProperty("created"), Description("created")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updated"), Description("updated")]
        public DateTime UpdatedDate { get; set; }
    }
}