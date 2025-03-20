using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;

namespace OsvitaBLL.Configurations
{
    public class OsvitaAutomapperProfile : Profile
    {
        public OsvitaAutomapperProfile()
        {
            CreateMap<Subject, SubjectModel>()
                .ForMember(sm => sm.ChaptersIds, s => s.MapFrom(x => x.Chapters.Select(c => c.Id)))
                .ReverseMap();

            CreateMap<Chapter, ChapterModel>()
                .ForMember(cm => cm.TopicsIds, c => c.MapFrom(x => x.Topics.Select(t => t.Id)))
                .ReverseMap();

            CreateMap<Topic, TopicModel>()
                .ForMember(tm => tm.MaterialsIds, t => t.MapFrom(x => x.Materials.Select(m => m.Id)))
                .ReverseMap();

            CreateMap<Material, MaterialModel>()
                .ForMember(mm => mm.ContentBlocksIds, m => m.MapFrom(x => x.ContentBlocks.Select(cb => cb.Id)))
                .ReverseMap();

            CreateMap<ContentBlock, ContentBlockModel>()
                .ForMember(cbm => cbm.ContentBlockModelType, m => m.MapFrom(x => x.ContentType))
                .ReverseMap();

            CreateMap<ContentType, ContentBlockModelType>()
                .ConvertUsingEnumMapping(opt => opt
                    .MapValue(ContentType.TextBlock, ContentBlockModelType.TextBlock)
                    .MapValue(ContentType.ImageBlock, ContentBlockModelType.ImageBlock)
                )
                .ReverseMap();

            CreateMap<Answer, AnswerModel>()
                .ReverseMap();

            CreateMap<Assignment, AssignmentModel>()
                .ForMember(am => am.AssignmentModelType, a => a.MapFrom(x => x.AssignmentType))
                .ReverseMap();

            CreateMap<AssignmentType, AssignmentModelType>()
                .ConvertUsingEnumMapping(opt => opt
                    .MapValue(AssignmentType.OneAnswerAsssignment, AssignmentModelType.OneAnswerAsssignment)
                    .MapValue(AssignmentType.OpenAnswerAssignment, AssignmentModelType.OpenAnswerAssignment)
                    .MapValue(AssignmentType.MatchComplianceAssignment, AssignmentModelType.MatchComplianceAssignment)
                )
                .ReverseMap();

            CreateMap<ObjectType, ObjectModelType>()
                .ConvertUsingEnumMapping(opt => opt
                    .MapValue(ObjectType.Material, ObjectModelType.MaterialModel)
                    .MapValue(ObjectType.Topic, ObjectModelType.TopicModel)
                    .MapValue(ObjectType.AssignmentSet, ObjectModelType.AssignmentSetModel)
                )
                .ReverseMap();

            CreateMap<User, UserModel>()
                .ForMember(um => um.StatisticModelId, m => m.MapFrom(x => x.Statistic.Id))
                .ForMember(um => um.EducationClassesIds, m => m.MapFrom(x => x.EducationClasses.Select(ec => ec.Id)))
                .ReverseMap();

            CreateMap<Statistic, StatisticModel>()
                .ReverseMap();

            CreateMap<TopicProgressDetail, TopicProgressDetailModel>()
                .ReverseMap();

            CreateMap<AssignmentSet, AssignmentSetModel>()
                .ForMember(am => am.ObjectModelType, a => a.MapFrom(x => x.ObjectType))
                .ReverseMap();

            CreateMap<AssignmentSetProgressDetail, AssignmentSetProgressDetailModel>()
                .ReverseMap();

            CreateMap<AssignmentProgressDetail, AssignmentProgressDetailModel>()
                .ReverseMap();

            CreateMap<EducationClass, EducationClassModel>()
                .ForMember(ecm => ecm.StudentsIds, ec => ec.MapFrom(x => x.Students.Select(s => s.Id)))
                .ForMember(ecm => ecm.EducationClassPlanId, ec => ec.MapFrom(x => x.EducationClassPlan.Id))
                .ReverseMap();

            CreateMap<AssignmentSetPlanDetail, AssignmentSetPlanDetailModel>()
                .ReverseMap();

            CreateMap<EducationClassPlan, EducationClassPlanModel>()
                .ReverseMap();

            CreateMap<EducationPlan, EducationPlanModel>() .ReverseMap();

            CreateMap<TopicPlanDetail, TopicPlanDetailModel>() .ReverseMap();

            CreateMap<RecomendationMessage, RecomendationMessageModel>().ReverseMap();
        }
    }
}

