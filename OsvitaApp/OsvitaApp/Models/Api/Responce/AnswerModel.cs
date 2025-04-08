namespace OsvitaApp.Models.Api.Responce;

public class AnswerModel
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string? ValueImage { get; set; }
    public bool IsCorrect { get; set; }
    public int? Points { get; set; }
    public int AssignmentId { get; set; }
}