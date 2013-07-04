using System;
using Newtonsoft.Json;
using PropertyChanged;
using ScoreoidPortable.Attributes;

namespace ScoreoidPortable.Entities
{
    /// <summary>
    /// This holds the information about your game
    /// </summary>
    [ImplementPropertyChanged]
    public class Game
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        [JsonProperty("user_id"), Description("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name"), Description("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the short description.
        /// </summary>
        /// <value>
        /// The short description.
        /// </value>
        [JsonProperty("short_description"), Description("short_description")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonProperty("description"), Description("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of the game.
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        [JsonProperty("game_type"), Description("game_type")]
        public string GameType { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [JsonProperty("version"), Description("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the levels.
        /// </summary>
        /// <value>
        /// The levels.
        /// </value>
        [JsonProperty("levels"), Description("levels")]
        public int Levels { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>
        /// The platform.
        /// </value>
        [JsonProperty("platform"), Description("platform")]
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the play URL.
        /// </summary>
        /// <value>
        /// The play URL.
        /// </value>
        [JsonProperty("play_url"), Description("play_url")]
        public string PlayUrl { get; set; }

        /// <summary>
        /// Gets or sets the website URL.
        /// </summary>
        /// <value>
        /// The website URL.
        /// </value>
        [JsonProperty("website_url"), Description("website_url")]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the players count.
        /// </summary>
        /// <value>
        /// The players count.
        /// </value>
        [JsonProperty("players_count"), Description("players_count")]
        public int PlayersCount { get; set; }

        /// <summary>
        /// Gets or sets the scores count.
        /// </summary>
        /// <value>
        /// The scores count.
        /// </value>
        [JsonProperty("scores_count"), Description("scores_count")]
        public int ScoresCount { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty("status"), Description("status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [JsonProperty("created"), Description("created")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        [JsonProperty("updated"), Description("updated")]
        public DateTime UpdatedDate { get; set; }
    }
}