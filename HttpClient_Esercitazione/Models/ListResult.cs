using System.Text.Json.Serialization;

namespace HttpClient_Esercitazione.Models
{
    public class ListResult<T>
    {
        public int Page { get; init; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; init; }

        public int Total { get; init; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; init; }

        public IEnumerable<T> Data { get; init; } = null!;
    }
}
