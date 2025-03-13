using System;
namespace OsvitaDAL.Interfaces
{
	public interface IUnitOfWork
	{
		ISubjectRepository SubjectRepository { get; }
		IChapterRepository ChapterRepository { get; }
		ITopicRepository TopicRepository { get; }
		IMaterialRepository MaterialRepository { get; }
		IContentBlockRepository ContentBlockRepository { get; }
		IAssignmentRepository AssignmentRepository { get; }
        IAssignmentSetRepository AssignmentSetRepository { get; }
        IAnswerRepository AnswerRepository { get; }
		IAssignmentLinkRepository AssignmentLinkRepository { get; }
        IUserRepository UserRepository { get; }
        IStatisticRepository StatisticRepository { get; }
        IEducationClassRepository EducationClassRepository { get; }
        IEducationClassPlanRepository EducationClassPlanRepository { get; }
		IEducationPlanRepository EducationPlanRepository { get; }
        Task SaveChangesAsync();
    }
}

