using CommunityToolkit.Mvvm.Input;
using OsvitaApp.Models;

namespace OsvitaApp.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}