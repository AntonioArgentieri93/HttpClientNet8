using System.Text.Json.Serialization;

namespace HttpClient_Esercitazione.Models
{
    public class User
    {
        public int Id { get; init; }

        public string Email { get; init; } = null!;

        [JsonPropertyName("first_name")]
        public string FirstName { get; init; } = null!;

        [JsonPropertyName("last_name")]
        public string LastName { get; init; } = null!;

        public string Avatar { get; init; } = null!;
    }
}
