using AutoMapper;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OsvitaApp.Interfaces;
using OsvitaApp.Models;
using OsvitaApp.Models.ObservableModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.PageModels
{
    public partial class SubjectsPageVM : ObservableObject
    {
        private readonly ISubjectsService _subjectsService;
        private readonly IMapper _mapper;

        [ObservableProperty] private ObservableCollection<SubjectObservableModel> _subjects;

        public SubjectsPageVM(ISubjectsService subjectsService, IMapper mapper)
        {
            _subjectsService = subjectsService;
            _mapper = mapper;
        }

        [RelayCommand]
        private async Task NavigatedTo()
        {
            var res = await _subjectsService.GetSubjectsAsync();
            if(res.IsSuccess)
            {
                Subjects = _mapper.Map<List<SubjectObservableModel>>(res.Data).ToObservableCollection();
            }
            else
            {
                await AppShell.DisplaySnackbarAsync(res.ErrorMessage);
            }
        }

        [RelayCommand]
        private async Task SubjectTapped(SubjectObservableModel subject)
        {
            
        }
    }
}
