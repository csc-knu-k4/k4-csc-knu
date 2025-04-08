using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using OsvitaApp.Enums;
using OsvitaApp.Models.Api.Responce;

namespace OsvitaApp.Models.ObservableModels;

public partial class AssignmentObservableModel : ObservableObject
{
    public int Id { get; set; }
    public int? ObjectId { get; set; }
    public string ProblemDescription { get; set; }
    public string? ProblemDescriptionImage { get; set; }
    public string Explanation { get; set; }
    public AssignmentModelType AssignmentModelType { get; set; }
    public int? ParentAssignmentId { get; set; }

    [ObservableProperty] private ObservableCollection<AnswerObservableModel> _answers;
    public List<AssignmentObservableModel> ChildAssignments { get; set; }
}