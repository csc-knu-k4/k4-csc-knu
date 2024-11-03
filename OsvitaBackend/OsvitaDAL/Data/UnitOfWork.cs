using System;
using OsvitaDAL.Interfaces;
using OsvitaDAL.Repositories;

namespace OsvitaDAL.Data
{
	public class UnitOfWork : IUnitOfWork
    {
        private readonly OsvitaDbContext context;

        public UnitOfWork(OsvitaDbContext context)
		{
			this.context = context;
		}

        ISubjectRepository subjectRepository;
        IChapterRepository chapterRepository;
        ITopicRepository topicRepository;
        IMaterialRepository materialRepository;
        IContentBlockRepository contentBlockRepository;

        public ISubjectRepository SubjectRepository
        {
            get
            {
                if (subjectRepository is null)
                {
                    subjectRepository = new SubjectRepository(context);
                }
                return subjectRepository;
            }
        }

        public IChapterRepository ChapterRepository
        {
            get
            {
                if (chapterRepository is null)
                {
                    chapterRepository = new ChapterRepository(context);
                }
                return chapterRepository;
            }
        }

        public ITopicRepository TopicRepository
        {
            get
            {
                if (topicRepository is null)
                {
                    topicRepository = new TopicRepository(context);
                }
                return topicRepository;
            }
        }

        public IMaterialRepository MaterialRepository
        {
            get
            {
                if (materialRepository is null)
                {
                    materialRepository = new MaterialRepository(context);
                }
                return materialRepository;
            }
        }

        public IContentBlockRepository ContentBlockRepository
        {
            get
            {
                if (contentBlockRepository is null)
                {
                    contentBlockRepository = new ContentBlockRepository(context);
                }
                return contentBlockRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}

