namespace OsvitaDAL.Entities
{
    public class Answer : BaseEntity
    {
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
        public int AssignmentId { get; set; }
        public int? Points { get; set; }

        public Assignment Assignment { get; set; }
    }
}
