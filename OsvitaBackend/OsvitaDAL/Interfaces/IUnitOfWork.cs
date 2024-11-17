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
        IUserRepository UserRepository { get; }
        Task SaveChangesAsync();
    }
}

