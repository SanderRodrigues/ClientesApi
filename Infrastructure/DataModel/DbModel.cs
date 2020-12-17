using System;
using System.Text.Json.Serialization;

namespace Infrastructure.DataModel
{
    [Serializable]
    internal abstract class DbModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
