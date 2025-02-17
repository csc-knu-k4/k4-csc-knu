namespace OsvitaBLL.Models
{
	public class StatisticModel
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<TopicProgressDetailModel> TopicProgressDetails { get; set; }
        public List<AssignmentSetProgressDetailModel> AssignmentSetProgressDetails { get; set; }
    }
}

