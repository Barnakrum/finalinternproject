using finalinternshipproject.Models.Fields;

namespace finalinternshipproject.Dtos.Item
{
    public class ItemPostDto
    {
        public string Name { get; set; } = string.Empty;
        public List<string>? Tags { get; set; }

        


        public List<string>? BooleanFields { get; set; }
        public List<string>? DateFields { get; set; }
        public List<string>? IntegerFields { get; set; }
        public List<string>? MultilineFields { get; set; }
        public List<string>? StringFields { get; set; }
    }
}
