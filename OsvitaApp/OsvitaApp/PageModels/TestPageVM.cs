using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using OsvitaApp.Enums;
using OsvitaApp.Interfaces;
using OsvitaApp.Models.Api.Responce;
using OsvitaApp.Models.Dto;
using OsvitaApp.Models.ObservableModels;

namespace OsvitaApp.PageModels
{
    public partial class TestPageVM : ObservableObject
    {
        private readonly IAssignmentsService _assignmentsService;
        private readonly TestPageDto _testPageDto;
        private readonly IMapper _mapper;

        [ObservableProperty] private TopicObservableModel _topic;
        [ObservableProperty] private AssignmentSetObservableModel _assignmentSet;

        public TestPageVM(IAssignmentsService assignmentsService, TestPageDto testPageDto, IMapper mapper)
        {
            _assignmentsService = assignmentsService;
            _testPageDto = testPageDto;
            _mapper = mapper;
            Topic = _testPageDto.Topic;
        }

        [RelayCommand]
        private async Task NavigatedTo()
        {
            var addAssgmentSetResult = await _assignmentsService.AddAssigmentSet(new AssignmentSetModel()
            {
                Id = 0,
                ObjectId = _testPageDto.Topic.Id,
                ObjectModelType = ObjectModelType.TopicModel,
                Assignments = new List<AssignmentModel>()
            });

            if (!addAssgmentSetResult.IsSuccess)
            {
                await AppShell.DisplaySnackbarAsync(addAssgmentSetResult.ErrorMessage);
                return;
            }

            var getAssgmentSetResult = await _assignmentsService.GetAssigmentSet(addAssgmentSetResult.Data);
            if (!getAssgmentSetResult.IsSuccess || getAssgmentSetResult.Data is null)
            {
                await AppShell.DisplaySnackbarAsync(getAssgmentSetResult.ErrorMessage);
                return;
            }

            var assigmentSetOM = _mapper.Map<AssignmentSetObservableModel>(getAssgmentSetResult.Data);
            for (var a = 0; a < assigmentSetOM.Assignments.Count; a++)
            {
                var asigment = assigmentSetOM.Assignments[a];
                for (var index = 0; index < asigment.Answers.Count; index++)
                {
                    var answer = asigment.Answers[index];
                    answer.AnswerLetter = (char)('А' + index);
                }
            }
            AssignmentSet = assigmentSetOM;
        }
    }
}