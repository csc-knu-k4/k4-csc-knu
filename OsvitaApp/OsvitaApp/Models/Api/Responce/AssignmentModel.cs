using OsvitaApp.Enums;

namespace OsvitaApp.Models.Api.Responce;

public class AssignmentModel
{
    public int Id { get; set; }
    public int? ObjectId { get; set; }
    public string ProblemDescription { get; set; }
    public string? ProblemDescriptionImage { get; set; }
    public string Explanation { get; set; }
    public AssignmentModelType AssignmentModelType { get; set; }
    public int? ParentAssignmentId { get; set; }

    public List<AnswerModel> Answers { get; set; }
    public List<AssignmentModel> ChildAssignments { get; set; }
}