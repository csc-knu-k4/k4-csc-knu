using System;
namespace OsvitaBLL.Models.ReportModels
{
	public class AssignmentSetReportModel
	{
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserSecondName { get; set; }
        public int ObjectId { get; set; }
		public string ObjectName { get; set; }
        public ObjectModelType ObjectType { get; set; }
        public DateTime CompletedDate { get; set; }
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public List<AssignmentReportModel> Assignments { get; set; }
    }
}

