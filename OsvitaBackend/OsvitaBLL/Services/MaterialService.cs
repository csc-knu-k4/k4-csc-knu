using System;
using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaBLL.Services
{
	public class MaterialService : IMaterialService
	{
        private readonly IUnitOfWork unitOfWork;
        private readonly IMaterialRepository materialRepository;
        private readonly IMapper mapper;

        public MaterialService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            materialRepository = unitOfWork.MaterialRepository;
            this.mapper = mapper;
        }

        public async Task AddAsync(MaterialModel model)
        {
            var material = mapper.Map<Material>(model);
            await materialRepository.AddAsync(material);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(MaterialModel model)
        {
            await materialRepository.DeleteByIdAsync(model.Id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await materialRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<MaterialModel>> GetAllAsync()
        {
            var materials = await materialRepository.GetAllWithDetailsAsync();
            var materialsModels = mapper.Map<IEnumerable<Material>, IEnumerable<MaterialModel>>(materials);
            return materialsModels;
        }

        public async Task<IEnumerable<MaterialModel>> GetByFilterAsync(FilterSearchModel filterSearchModel)
        {
            var materials = await materialRepository.GetAllWithDetailsAsync();
            materials = materials?.Where(x =>
                    (filterSearchModel.SubjectId is null || x.Topic.Chapter.SubjectId == filterSearchModel.SubjectId) &&
                    (filterSearchModel.ChapterId is null || x.Topic.ChapterId == filterSearchModel.ChapterId) &&
                    (filterSearchModel.ChapterId is null || x.TopicId == filterSearchModel.TopicId) &&
                    (String.IsNullOrEmpty(filterSearchModel.SearchString) || x.Title.Contains(filterSearchModel.SearchString))
                );
            var materialsModels = materials is not null ? mapper.Map<IEnumerable<Material>, IEnumerable<MaterialModel>>(materials) : new List<MaterialModel>();
            return materialsModels;
        }

        public async Task<MaterialModel> GetByIdAsync(int id)
        {
            var material = await materialRepository.GetByIdWithDetailsAsync(id);
            var materialModel = mapper.Map<Material, MaterialModel>(material);
            return materialModel;
        }

        public async Task<IEnumerable<MaterialModel>> GetByTopicIdAsync(int topicId)
        {
            var materials = await materialRepository.GetAllAsync();
            materials = materials?.Where(x => x.TopicId == topicId);
            var topicsModels = materials is not null ? mapper.Map<IEnumerable<Material>, IEnumerable<MaterialModel>>(materials) : new List<MaterialModel>();
            return topicsModels;
        }

        public async Task UpdateAsync(MaterialModel model)
        {
            var material = mapper.Map<Material>(model);
            await materialRepository.UpdateAsync(material);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

