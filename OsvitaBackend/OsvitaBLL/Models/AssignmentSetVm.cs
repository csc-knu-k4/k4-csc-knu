using System;
namespace OsvitaBLL.Models
{
	public class AssignmentSetVm
	{
		public int Id { get; set; }
		public string Title { get; set; }
        public ObjectModelType ObjectModelType { get; set; }
		public bool IsCompleted { get; set; }
    }
}

