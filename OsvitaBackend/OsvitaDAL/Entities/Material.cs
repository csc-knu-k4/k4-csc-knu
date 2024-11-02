using System;
namespace OsvitaDAL.Entities
{
	public class Material : BaseEntity
	{
		public string Title { get; set; }
		public int TopicId { get; set; }

		public Topic Topic { get; set; }
		public List<ContentBlock> ContentBlocks { get; set; }
	}
}

