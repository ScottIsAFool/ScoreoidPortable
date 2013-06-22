﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using Newtonsoft.Json;

namespace ScoreoidPortable.Entities
{
    public class Player
    {
        [JsonProperty("username"), Description("username")]
        public string Username { get; set; }

        [JsonProperty("unique_id"), Description("unique_id")]
        public string UniqueId { get; set; }

        [JsonProperty("first_name"), Description("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name"), Description("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email"), Description("email")]
        public string Email { get; set; }

        [JsonProperty("bonus"), Description("bonus")]
        public int Bonus { get; set; }

        [JsonProperty("achievements"), Description("achievements")]
        public string Achievements { get; set; }

        [JsonProperty("gold"), Description("gold")]
        public int Gold { get; set; }

        [JsonProperty("money"), Description("money")]
        public int Money { get; set; }

        [JsonProperty("kills"), Description("kills")]
        public int Kills { get; set; }

        [JsonProperty("lives"), Description("lives")]
        public int Lives { get; set; }

        [JsonProperty("time_played"), Description("time_played")]
        public int TimePlayed { get; set; }

        [JsonProperty("unlocked_levels"), Description("unlocked_levels")]
        public string UnlockedLevels { get; set; }

        [JsonProperty("unlocked_items"), Description("unlocked_items")]
        public string UnlockedItems { get; set; }

        [JsonProperty("inventory"), Description("inventory")]
        public string Inventory { get; set; }

        [JsonProperty("last_level"), Description("last_level")]
        public string LastLevel { get; set; }

        [JsonProperty("current_level"), Description("current_level")]
        public string CurrentLevel { get; set; }

        [JsonProperty("current_time"), Description("current_time")]
        public int CurrentTime { get; set; }

        [JsonProperty("current_bonus"), Description("current_bonus")]
        public int CurrentBonus { get; set; }

        [JsonProperty("current_kills"), Description("current_kills")]
        public int CurrentKills { get; set; }

        [JsonProperty("current_achievements"), Description("current_achievements")]
        public string CurrentAchievements { get; set; }

        [JsonProperty("current_gold"), Description("current_gold")]
        public int CurrentGold { get; set; }

        [JsonProperty("current_unlocked_levels"), Description("current_unlocked_levels")]
        public int CurrentUnlockedLevels { get; set; }

        [JsonProperty("current_unlocked_items"), Description("current_unlocked_items")]
        public string CurrentUnlockedItems { get; set; }

        [JsonProperty("current_lives"), Description("current_lives")]
        public int CurrentLives { get; set; }

        [JsonProperty("xp"), Description("xp")]
        public string Xp { get; set; }

        [JsonProperty("energy"), Description("energy")]
        public string Energy { get; set; }

        [JsonProperty("boost"), Description("boost")]
        public string Boost { get; set; }

        [JsonProperty("latitude"), Description("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longtitude"), Description("longtitude")]
        public string Longtitude { get; set; }

        [JsonProperty("game_state"), Description("game_state")]
        public string GameState { get; set; }

        [JsonProperty("platform"), Description("platform")]
        public string Platform { get; set; }

        [JsonProperty("rank"), Description("rank")]
        public int Rank { get; set; }

        [JsonProperty("best_score"), Description("best_score")]
        public int BestScore { get; set; }

        [JsonProperty("created"), Description("created")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updated"), Description("updated")]
        public DateTime UpdatedDate { get; set; }
    }
}
