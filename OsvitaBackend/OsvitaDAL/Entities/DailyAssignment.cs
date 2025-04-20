namespace OsvitaDAL.Entities
{
    public class DailyAssignment : BaseEntity
    {
        public int UserId { get; set; }
        public int AssignmentSetId { get; set; }
        public DateTime CreationDate { get; set; }
        public User User { get; set; }
        public AssignmentSet AssignmentSet { get; set; }
    }
}