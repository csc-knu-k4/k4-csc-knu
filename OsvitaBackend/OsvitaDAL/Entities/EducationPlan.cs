namespace OsvitaDAL.Entities
{
    public class EducationPlan : BaseEntity
    {
        public int UserId { get; set; }
        public List<TopicPlanDetail> TopicPlanDetails { get; set; }
    }
}
