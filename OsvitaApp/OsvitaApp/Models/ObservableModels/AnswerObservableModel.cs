using CommunityToolkit.Mvvm.ComponentModel;

namespace OsvitaApp.Models.ObservableModels;

public class AnswerObservableModel : ObservableObject
{
    public int Id { get; set; }
    public char AnswerLetter { get; set; }
    public string Value { get; set; }
    public string? ValueImage { get; set; }
    public bool IsCorrect { get; set; }
    public int? Points { get; set; }
    public int AssignmentId { get; set; }
}