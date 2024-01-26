
using finalinternshipproject.Models.Fields;

namespace finalinternshipproject.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Tag>? Tags { get; set; }

        public Collection Collection{ get; set; }


        public List<Like>? Like { get; set; }
        public List<Comment>? Comments { get; set; }

        public List<BooleanField>? BooleanFields { get; set; }
        public List<DateTimeField>? DateFields { get; set; }
        public List<IntegerField>? IntegerFields { get; set; }
        public List<MultilineField>? MultilineFields { get; set; }
        public List<StringField>? StringFields { get; set; }
    }
}
