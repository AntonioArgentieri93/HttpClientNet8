using System.Text.Json.Serialization;

namespace HttpClient_Esercitazione.Models
{
    public class Data
    {
        public int Id { get; init; }

        public string Name { get; init; } = null!;

        public int Year { get; init; }

        public string Color { get; init; } = null!;

        [JsonPropertyName("pantone_value")]
        public string PantoneValue { get; init; } = null!;
    }
}
