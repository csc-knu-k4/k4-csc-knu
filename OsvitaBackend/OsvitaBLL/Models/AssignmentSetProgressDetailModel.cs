using System;
namespace OsvitaBLL.Models
{
	public class AssignmentSetProgressDetailModel
	{
        public int Id { get; set; }
        public int StatisticId { get; set; }
        public int AssignmentSetId { get; set; }
        public int AssignmentId { get; set; }
        public int AnswerId { get; set; }
    }
}

