using System;
namespace OsvitaBLL.Models
{
	public class AssignmentSetProgressDetailModel
	{
        public int Id { get; set; }
        public int StatisticId { get; set; }
        public int AssignmentSetId { get; set; }
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletedDate { get; set; }
        public List<AssignmentProgressDetailModel> AssignmentProgressDetails { get; set; }
    }
}

