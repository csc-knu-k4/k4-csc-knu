using System;
using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaBLL.Services
{
    public class ChapterService : IChapterService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IChapterRepository chapterRepository;
        private readonly IMapper mapper;

        public ChapterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            chapterRepository = unitOfWork.ChapterRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddAsync(ChapterModel model)
        {
            var chapter = mapper.Map<Chapter>(model);
            await chapterRepository.AddAsync(chapter);
            await unitOfWork.SaveChangesAsync();
            return chapter.Id;
        }

        public async Task DeleteAsync(ChapterModel model)
        {
            await chapterRepository.DeleteByIdAsync(model.Id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await chapterRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChapterModel>> GetAllAsync()
        {
            var chapters = await chapterRepository.GetAllAsync();
            foreach(var chapter in chapters)
            {
                chapter.Topics = (await unitOfWork.TopicRepository.GetAllAsync()).Where(x => x.ChapterId == chapter.Id).ToList();
            }
            var chaptersModels = mapper.Map<IEnumerable<Chapter>, IEnumerable<ChapterModel>>(chapters);
            return chaptersModels;
        }

        public async Task<IEnumerable<ChapterModel>> GetByFilterAsync(FilterSearchModel filterSearchModel)
        {
            var chapters = await chapterRepository.GetAllAsync();
            chapters = chapters?.Where(x =>
                    (filterSearchModel.SubjectId is null || x.SubjectId == filterSearchModel.SubjectId) &&
                    (String.IsNullOrEmpty(filterSearchModel.SearchString) || x.Title.Contains(filterSearchModel.SearchString))
                );
            var chaptersModels = chapters is not null ? mapper.Map<IEnumerable<Chapter>, IEnumerable<ChapterModel>>(chapters) : new List<ChapterModel>();
            return chaptersModels;
        }

        public async Task<ChapterModel> GetByIdAsync(int id)
        {
            var chapter = await chapterRepository.GetByIdAsync(id);
            chapter.Topics = (await unitOfWork.TopicRepository.GetAllAsync()).Where(x => x.ChapterId == chapter.Id).ToList();
            var chapterModel = mapper.Map<Chapter, ChapterModel>(chapter);
            return chapterModel;
        }

        public async Task<IEnumerable<ChapterModel>> GetBySubjectIdAsync(int subjectId)
        {
            var chapters = await chapterRepository.GetAllAsync();
            chapters = chapters?.Where(x => x.SubjectId == subjectId);
            var chaptersModels = chapters is not null ? mapper.Map<IEnumerable<Chapter>, IEnumerable<ChapterModel>>(chapters) : new List<ChapterModel>();
            return chaptersModels;
        }

        public async Task UpdateAsync(ChapterModel model)
        {
            var chapter = mapper.Map<Chapter>(model);
            await chapterRepository.UpdateAsync(chapter);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

