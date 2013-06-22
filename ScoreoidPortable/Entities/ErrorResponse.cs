using Newtonsoft.Json;

namespace ScoreoidPortable.Entities
{
    internal class ErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}