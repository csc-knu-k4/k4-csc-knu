using System;
namespace OsvitaBLL.Models
{
    public class MaterialModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TopicId { get; set; }
        public int OrderPosition { get; set; }

        public List<int> ContentBlocksIds { get; set; }
    }
}

