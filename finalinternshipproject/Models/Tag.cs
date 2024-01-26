namespace finalinternshipproject.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagValue { get; set; }
        public Item Item { get; set; }
    }
}
