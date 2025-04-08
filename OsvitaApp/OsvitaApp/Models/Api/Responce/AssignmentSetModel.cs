using OsvitaApp.Enums;

namespace OsvitaApp.Models.Api.Responce;

public class AssignmentSetModel
{
    public int Id { get; set; }
    public ObjectModelType ObjectModelType { get; set; }
    public int ObjectId { get; set; }
    public List<AssignmentModel> Assignments { get; set; }
}