namespace finalinternshipproject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreationTime { get; set; }
        public User User { get; set; }
        public Item Item { get; set; }
    }
}
