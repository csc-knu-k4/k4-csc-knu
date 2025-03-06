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
        IAssignmentRepository assignmentRepository;
        IAssignmentSetRepository assignmentSetRepository;
        IAnswerRepository answerRepository;
        IAssignmentLinkRepository assignmentLinkRepository;
        IUserRepository userRepository;
        IStatisticRepository statisticRepository;
        IEducationClassRepository educationClassRepository;

        public ISubjectRepository SubjectRepository
        {
            get
            {
                if(subjectRepository is null)
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
                if(chapterRepository is null)
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
                if(topicRepository is null)
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
                if(materialRepository is null)
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
                if(contentBlockRepository is null)
                {
                    contentBlockRepository = new ContentBlockRepository(context);
                }
                return contentBlockRepository;
            }
        }

        public IAssignmentRepository AssignmentRepository
        {
            get
            {
                if (assignmentRepository is null)
                {
                    assignmentRepository = new AssignmentRepository(context);
                }
                return assignmentRepository;
            }
        }

        public IAssignmentSetRepository AssignmentSetRepository
        {
            get
            {
                if (assignmentSetRepository is null)
                {
                    assignmentSetRepository = new AssignmentSetRepository(context);
                }
                return assignmentSetRepository;
            }
        }

        public IAnswerRepository AnswerRepository
        {
            get
            {
                if (answerRepository is null)
                {
                    answerRepository = new AnswerRepository(context);
                }
                return answerRepository;
            }
        }

        public IAssignmentLinkRepository AssignmentLinkRepository
        {
            get
            {
                if (assignmentLinkRepository is null)
                {
                    assignmentLinkRepository = new AssignmentLinkRepository(context);
                }
                return assignmentLinkRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository is null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public IStatisticRepository StatisticRepository
        {
            get
            {
                if (statisticRepository is null)
                {
                    statisticRepository = new StatisticRepository(context);
                }
                return statisticRepository;
            }
        }

        public IEducationClassRepository EducationClassRepository
        {
            get
            {
                if (educationClassRepository is null)
                {
                    educationClassRepository = new EducationClassRepository(context);
                }
                return educationClassRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}

