using System;
namespace OsvitaDAL.Entities
{
    public class Topic : BaseEntity
    {
        public string Title { get; set; }
        public int ChapterId { get; set; }
        public int OrderPosition { get; set; }

        public Chapter Chapter { get; set; }
        public List<Material> Materials { get; set; }
    }
}

