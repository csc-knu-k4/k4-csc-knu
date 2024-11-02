using System;
namespace OsvitaDAL.Entities
{
	public class ContentBlock : BaseEntity
	{
		public string Title { get; set; }
		public string Value { get; set; }
		public ContentType ContentType { get; set; }
		public int MaterialId { get; set; }

		public Material Material { get; set; }
	}
}

