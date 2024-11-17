namespace OsvitaDAL.Entities
{
    public class Assignment : BaseEntity
    {
        public string ProblemDescription { get; set; }
        public string Explanation { get; set; }
        public AssignmentType AssignmentType { get; set; }
        public int? ParentAssignmentId { get; set; }

        public Assignment? ParentAssignment { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
