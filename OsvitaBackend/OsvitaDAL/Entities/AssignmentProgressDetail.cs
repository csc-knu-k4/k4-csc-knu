using System;
namespace OsvitaDAL.Entities
{
	public class AssignmentProgressDetail : BaseEntity
	{
        public int AssignmentSetProgressDetailId { get; set; }
        public int AssignmentId { get; set; }
        public string AnswerValue { get; set; }
        public bool IsCorrect { get; set; }
        public int Points { get; set; }
    }
}

