using System.Text.Json.Serialization;

namespace HttpClient_Esercitazione.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = null!;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = null!;

        public string Avatar { get; set; } = null!;
    }
}
