namespace finalinternshipproject.Dtos.Collection
{
    public class CollectionEditDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CollectionTopic Topic { get; set; }
        public string? ImageHandle { get; set; } = null;
    }
}
