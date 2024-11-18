namespace OsvitaBLL.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
        public int? Points { get; set; }
        public int AssignmentId { get; set; }
    }
}
