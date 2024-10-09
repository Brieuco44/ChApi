using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ChApi.Models
{
    public class Race
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("name")]
        public required string Nom { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [JsonPropertyName("origin")]
        public required string Origine { get; set; }
    }
}
