using System;
namespace OsvitaBLL.Models
{
	public class AssistantResponseModel
	{
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string RequestText { get; set; }
        public string ResponseText { get; set; }
    }
}

