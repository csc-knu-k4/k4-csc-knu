using System;
namespace OsvitaBLL.Models
{
	public class RecomendationMessageModel
	{
        public int Id { get; set; }
        public string RecomendationText { get; set; }
        public bool IsRead { get; set; }
        public bool IsSent { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
    }
}

