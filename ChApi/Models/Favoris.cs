using System.Text.Json.Serialization;

namespace ChApi.Models
{
    public class Favoris
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("url")]
        public required string Url { get; set; }

        [JsonPropertyName("breeds")]
        public required List<Race> Race { get; set; }
    }
}
