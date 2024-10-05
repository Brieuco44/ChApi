using System.Text.Json.Serialization;
using ChApi.Models;
using System.Collections.Generic;

namespace ChApi.Models
{
    public class Chat
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("url")]
        public required string Url { get; set; }

        [JsonPropertyName("breeds")]
        public required List<Race> Race { get; set; }
    }
}
