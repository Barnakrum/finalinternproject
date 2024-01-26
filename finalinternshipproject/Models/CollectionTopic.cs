using System.Text.Json.Serialization;

namespace finalinternshipproject.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CollectionTopic
    {
        Books = 0,
        Silverware = 1,
        Postcards = 2,
        Figurines = 3,
        Games = 4
    }
}
