using System;
namespace OsvitaBLL.Models
{
	public class FilterSearchModel
	{
		public int? SubjectId { get; set; }
        public int? ChapterId { get; set; }
        public int? TopicId { get; set; }
        public int? MaterialId { get; set; }
        public string? SearchString { get; set; }
    }
}

