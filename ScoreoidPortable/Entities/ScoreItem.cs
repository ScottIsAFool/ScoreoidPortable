﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using Newtonsoft.Json;

namespace ScoreoidPortable.Entities
{
    public class ScoreItem
    {
        [JsonProperty("Player")]
        public Player Player { get; set; }

        [JsonProperty("Score")]
        public Score Score { get; set; }
    }
}