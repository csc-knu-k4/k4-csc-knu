namespace OsvitaBLL.Models
{
	public class StatisticModel
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<int> ChapterProgressDetailIds { get; set; }
    }
}

