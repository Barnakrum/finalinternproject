
using finalinternshipproject.Models.Fields;

namespace finalinternshipproject.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CollectionTopic Topic { get; set; } = CollectionTopic.Books;
        public string? ImageHandle { get; set; } = null;
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime ModifyTime { get; set; } = DateTime.Now;



        public List<Item>? Items { get; set; }

        public User User{ get; set; }





        public List<BooleanFieldName>? BooleanFieldsNames { get; set; }
        public List<DateTimeFieldName>? DateTimeFieldsNames { get; set; }
        public List<IntegerFieldName>? IntegerFieldsNames { get; set; }
        public List<MultilineFieldName>? MultilineFieldsNames { get; set; }
        public List<StringFieldName>? StringFieldsNames { get; set; }

    }
}
