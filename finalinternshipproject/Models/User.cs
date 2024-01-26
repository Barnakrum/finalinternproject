namespace finalinternshipproject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool isAdmin { get; set; } = false;

        public bool isBlocked { get; set; } = false;

        public List<Like>? Like{ get; set; }

        public List<Comment>? Comment{ get; set; }



        public List<Collection>? Collections{ get; set; }

    }
}
