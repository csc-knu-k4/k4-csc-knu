using System;
namespace OsvitaBLL.Models
{
	public class AssignmentProgressDetailModel
	{
        public int AssignmentSetProgressDetailId { get; set; }
        public int AssignmentId { get; set; }
        public string AnswerValue { get; set; }
        public bool IsCorrect { get; set; }
        public int Points { get; set; }
        public int MaxPoints { get; set; }
    }
}

