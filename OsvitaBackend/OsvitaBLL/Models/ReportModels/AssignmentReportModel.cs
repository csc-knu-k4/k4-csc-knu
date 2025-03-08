namespace OsvitaBLL.Models.ReportModels
{
	public class AssignmentReportModel
	{
        public int AssignmentId { get; set; }
        public int AssignmentNumber { get; set; }
        public AssignmentModelType AssignmentType { get; set; }
        public bool IsCorrect { get; set; }
        public int Points { get; set; }
        public int MaxPoints { get; set; }
    }
}

