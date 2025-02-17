using System;
namespace OsvitaDAL.Entities
{
	public class AssignmentSetProgressDetail : BaseEntity
	{
        public int StatisticId { get; set; }
        public int AssignmentSetId { get; set; }
        public int AssignmentId { get; set; }
        public int AnswerId { get; set; }
    }
}

