using System;
namespace OsvitaDAL.Entities
{
	public class Chapter : BaseEntity
	{
		public string Title { get; set; }
		public int SubjectId { get; set; }
        public int OrderPosition { get; set; }

        public Subject Subject { get; set; }
		public List<Topic> Topics { get; set; }
	}
}

