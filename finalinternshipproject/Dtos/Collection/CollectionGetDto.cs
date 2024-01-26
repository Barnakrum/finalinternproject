namespace finalinternshipproject.Dtos.Collection
{
    public class CollectionGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CollectionTopic Topic { get; set; } = CollectionTopic.Books;
        public string? ImageHandle { get; set; } = null;
        public string CreationTime { get; set; } = string.Empty;
        public string ModifyTime { get; set; } = string.Empty;

        

    }
}
