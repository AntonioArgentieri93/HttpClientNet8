using System.Text.Json.Serialization;

namespace HttpClient_Esercitazione.Models
{
    public class NewUserCreated : NewUser
    {
        public string Id { get; set; }

        [JsonPropertyName("createdAt")]
        public string Created { get; set; }
    }
}
