using System;
using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaBLL.Services
{
	public class ContentBlockService : IContentBlockService
	{
        private readonly IUnitOfWork unitOfWork;
        private readonly IContentBlockRepository contentBlockRepository;
        private readonly IMapper mapper;

        public ContentBlockService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            contentBlockRepository = unitOfWork.ContentBlockRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddAsync(ContentBlockModel model)
        {
            var contentBlock = mapper.Map<ContentBlock>(model);
            await contentBlockRepository.AddAsync(contentBlock);
            await unitOfWork.SaveChangesAsync();
            return contentBlock.Id;
        }

        public async Task DeleteAsync(ContentBlockModel model)
        {
            await contentBlockRepository.DeleteByIdAsync(model.Id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await contentBlockRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContentBlockModel>> GetAllAsync()
        {
            var contentBlocks = await contentBlockRepository.GetAllAsync();
            var contentBlocksModels = mapper.Map<IEnumerable<ContentBlock>, IEnumerable<ContentBlockModel>>(contentBlocks);
            return contentBlocksModels;
        }

        public async Task<IEnumerable<ContentBlockModel>> GetByFilterAsync(FilterSearchModel filterSearchModel)
        {
            var contentBlocks = await contentBlockRepository.GetAllAsync();
            contentBlocks = contentBlocks?.Where(x =>
                    (filterSearchModel.SubjectId is null || x.Material.Topic.Chapter.SubjectId == filterSearchModel.SubjectId) &&
                    (filterSearchModel.ChapterId is null || x.Material.Topic.ChapterId == filterSearchModel.ChapterId) &&
                    (filterSearchModel.ChapterId is null || x.Material.TopicId == filterSearchModel.TopicId) &&
                    (filterSearchModel.MaterialId is null || x.MaterialId == filterSearchModel.MaterialId) &&
                    (String.IsNullOrEmpty(filterSearchModel.SearchString) || x.Title.Contains(filterSearchModel.SearchString))
                );
            var contentBlocksModels = contentBlocks is not null ? mapper.Map<IEnumerable<ContentBlock>, IEnumerable<ContentBlockModel>>(contentBlocks) : new List<ContentBlockModel>();
            return contentBlocksModels;
        }

        public async Task<ContentBlockModel> GetByIdAsync(int id)
        {
            var contentBlock = await contentBlockRepository.GetByIdAsync(id);
            var contentBlockModel = mapper.Map<ContentBlock, ContentBlockModel>(contentBlock);
            return contentBlockModel;
        }

        public async Task<IEnumerable<ContentBlockModel>> GetByMaterialIdAsync(int materialId)
        {
            var contentBlocks = await contentBlockRepository.GetAllAsync();
            contentBlocks = contentBlocks?.Where(x => x.MaterialId == materialId);
            var contentBlocksModels = contentBlocks is not null ? mapper.Map<IEnumerable<ContentBlock>, IEnumerable<ContentBlockModel>>(contentBlocks) : new List<ContentBlockModel>();
            return contentBlocksModels;
        }

        public async Task UpdateAsync(ContentBlockModel model)
        {
            var contentBlock = mapper.Map<ContentBlock>(model);
            await contentBlockRepository.UpdateAsync(contentBlock);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

