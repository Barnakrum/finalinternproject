
namespace finalinternshipproject.Dtos.Collection
{
    public class CollectionAddItemDto
    {
        public string Name { get; set; } = string.Empty;

        public string Usersname { get; set; }
        public List<string>? BooleanFieldsNames { get; set; }
        public List<string>? DateFieldsNames { get; set; }
        public List<string>? IntegerFieldsNames { get; set; }
        public List<string>? MultilineFieldsNames { get; set; }
        public List<string>? StringFieldsNames { get; set; }
    }
}
