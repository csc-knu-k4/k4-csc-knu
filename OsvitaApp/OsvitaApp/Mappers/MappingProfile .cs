using AutoMapper;
using OsvitaApp.Models;
using OsvitaApp.Models.ObservableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SubjectModel, SubjectObservableModel>().ReverseMap();
            CreateMap<ChapterModel, ChapterObservableModel>().ReverseMap();
            CreateMap<TopicModel, TopicObservableModel>().ReverseMap();
        }
    }
}
