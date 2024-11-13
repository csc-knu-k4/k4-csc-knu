namespace OsvitaBLL.Models
{
    public class ContentBlockModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ContentBlockModelType ContentBlockModelType { get; set; }
        public int OrderPosition { get; set; }
        public int MaterialId { get; set; }
        public string Value { get; set; }
    }
}

