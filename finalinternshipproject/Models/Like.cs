namespace finalinternshipproject.Models
{
    public class Like
    {
        public int Id { get; set; }
        public List<User>? User { get; set; }
        public Item Item { get; set; }
    }
}
