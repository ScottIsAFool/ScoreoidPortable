using Newtonsoft.Json;

namespace ScoreoidPortable.Entities
{
    internal class SuccessResponse
    {
        [JsonProperty("success")]
        public string Success { get; set; }
    }
}