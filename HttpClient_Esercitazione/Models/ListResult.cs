﻿using System.Text.Json.Serialization;

namespace HttpClient_Esercitazione.Models
{
    public class ListResult<T>
    {
        public int Page { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        public int Total { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
