
using finalinternshipproject.Models.Fields;

namespace finalinternshipproject.Dtos.Collection
{
    public class CollectionPostDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageHandle { get; set; } = string.Empty;
        public CollectionTopic Topic { get; set; }

        public List<string>? BooleanFieldsNames { get; set; }
        public List<string>? DateTimeFieldsNames { get; set; }
        public List<string>? IntegerFieldsNames { get; set; }
        public List<string>? MultilineFieldsNames { get; set; }
        public List<string>? StringFieldsNames { get; set; }
    }
}
