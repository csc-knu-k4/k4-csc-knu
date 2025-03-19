using OsvitaDAL.Entities;

namespace OsvitaBLL.Models
{
    public class EducationPlanModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<TopicPlanDetailModel> TopicPlanDetails { get; set; }
        public List<AssignmentSetPlanDetailModel> AssignmentSetPlanDetails { get; set; }
    }
}
