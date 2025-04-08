using OsvitaApp.Enums;
using OsvitaApp.Models.Api.Responce;
using OsvitaApp.Models.ObservableModels;

namespace OsvitaApp.Helpers.TemplateSelectors;

public class AssigmentModelTypeSelector  : DataTemplateSelector
{
    public DataTemplate OneAnswerAsssignmentTemplate { get; set; }
    public DataTemplate OpenAnswerAssignmentTemplate { get; set; }
    public DataTemplate MatchComplianceAssignmentTemplate { get; set; }
    
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var assignmentModel = item as AssignmentObservableModel;

        switch (assignmentModel.AssignmentModelType)
        {
            case AssignmentModelType.OneAnswerAsssignment:
                return OneAnswerAsssignmentTemplate;
            case AssignmentModelType.OpenAnswerAssignment:
                return OpenAnswerAssignmentTemplate;
            case AssignmentModelType.MatchComplianceAssignment:
                return MatchComplianceAssignmentTemplate;
            default:
                return null;
        }
        
    }
}