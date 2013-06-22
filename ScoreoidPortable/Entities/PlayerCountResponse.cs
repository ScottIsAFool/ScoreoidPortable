using Newtonsoft.Json;

namespace ScoreoidPortable.Entities
{
    internal class PlayerCountResponse
    {
        [JsonProperty("players")]
        public int PlayerCount
        {
            get;
            set;
        }
    }
}