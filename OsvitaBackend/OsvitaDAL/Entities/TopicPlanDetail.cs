namespace OsvitaDAL.Entities
{
    public class TopicPlanDetail : BaseEntity
    {
        public int? EducationPlanId { get; set; }
        public int TopicId { get; set; }
        public int? EducationClassPlanId { get; set; }
        public EducationPlan? EducationPlan { get; set; }
        public Topic Topic { get; set; }
        public EducationClassPlan? EducationClassPlan { get; set; }
    }
}
