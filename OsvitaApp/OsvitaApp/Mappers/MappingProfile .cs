using AutoMapper;
using OsvitaApp.Models;
using OsvitaApp.Models.ObservableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsvitaApp.Models.Api.Responce;

namespace OsvitaApp.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SubjectModel, SubjectObservableModel>().ReverseMap();
            CreateMap<ChapterModel, ChapterObservableModel>().ReverseMap();
            CreateMap<TopicModel, TopicObservableModel>().ReverseMap();
            CreateMap<MaterialModel, MaterialObservableModel>().ReverseMap();
            CreateMap<ContentBlockModel, ContentBlockObservableModel>().ReverseMap();
            CreateMap<AssignmentSetModel, AssignmentSetObservableModel>().ReverseMap();
            CreateMap<AssignmentModel, AssignmentObservableModel>().ReverseMap();
            CreateMap<AnswerModel, AnswerObservableModel>().ReverseMap();
        }
    }
}
