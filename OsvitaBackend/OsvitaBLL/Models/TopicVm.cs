using System;
namespace OsvitaBLL.Models
{
	public class TopicVm
	{
		public int Id { get; set; }
        public int TopicId { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}

