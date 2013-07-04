﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using Newtonsoft.Json;
using PropertyChanged;
using ScoreoidPortable.Attributes;

namespace ScoreoidPortable.Entities
{
    /// <summary>
    /// The Score class
    /// </summary>
    [ImplementPropertyChanged]
    public class Score
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [JsonProperty("id"), Description("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        /// <value>
        /// The player id.
        /// </value>
        [JsonProperty("player_id"), Description("player_id")]
        public string PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the difficulty.
        /// </summary>
        /// <value>
        /// The difficulty.
        /// </value>
        [JsonProperty("difficulty"), Description("difficulty")]
        public int Difficulty { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>
        /// The platform.
        /// </value>
        [JsonProperty("platform"), Description("platform")]
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the leaderboard.
        /// </summary>
        /// <value>
        /// The leaderboard.
        /// </value>
        [JsonProperty("leaderboard"), Description("leaderboard")]
        public object Leaderboard { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonProperty("data"), Description("data")]
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        [JsonProperty("score"), Description("score")]
        public string TheScore { get; set; }

        /// <summary>
        /// Gets or sets the game id.
        /// </summary>
        /// <value>
        /// The game id.
        /// </value>
        [JsonProperty("game_id"), Description("game_id")]
        public string GameId { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [JsonProperty("created"), Description("created")]
        public DateTime CreatedDate { get; set; }
    }
}

