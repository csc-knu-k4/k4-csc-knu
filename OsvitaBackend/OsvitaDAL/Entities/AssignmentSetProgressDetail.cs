using System;
namespace OsvitaDAL.Entities
{
	public class AssignmentSetProgressDetail : BaseEntity
	{
        public int StatisticId { get; set; }
        public int AssignmentSetId { get; set; }
        public int Score { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletedDate { get; set; }
        public List<AssignmentProgressDetail> AssignmentProgressDetails { get; set; }
    }
}

