using System;
namespace OsvitaDAL.Entities
{
    public class Subject : BaseEntity
    {
        public string Title { get; set; }
        public List<Chapter> Chapters { get; set; }
    }
}

