using System;
namespace OsvitaDAL.Entities
{
	public class RecomendationMessage : BaseEntity
	{
		public string RecomendationText { get; set; }
        public bool IsRead { get; set; }
        public bool IsSent { get; set; }
        public DateTime CreationDate { get; set; }
		public int UserId { get; set; }

		public User User { get; set; }
	}
}

