using System.Text.Json.Serialization;

namespace HttpClient_Esercitazione.Models
{
    public class NewUserCreated : NewUser
    {
        public string Id { get; init; } = null!;

        [JsonPropertyName("createdAt")]
        public string Created { get; init; } = null!;
    }
}
