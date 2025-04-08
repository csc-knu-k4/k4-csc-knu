using OsvitaApp.Enums;
using OsvitaApp.Models.Api.Responce;

namespace OsvitaApp.Models.ObservableModels;

public class  AssignmentSetObservableModel
{
    public int Id { get; set; }
    public ObjectModelType ObjectModelType { get; set; }
    public int ObjectId { get; set; }
    public List<AssignmentObservableModel> Assignments { get; set; }
}