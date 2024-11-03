using System;
using AutoMapper;
using OsvitaDAL.Interfaces;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;

namespace OsvitaBLL.Services
{
	public class SubjectService : ISubjectService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly ISubjectRepository subjectRepository;
		private readonly IMapper mapper;

		public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			subjectRepository = unitOfWork.SubjectRepository;
			this.mapper = mapper;
		}

        public async Task AddAsync(SubjectModel model)
        {
            var subject = mapper.Map<Subject>(model);
            await subjectRepository.AddAsync(subject);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(SubjectModel model)
        {
            await subjectRepository.DeleteByIdAsync(model.Id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await subjectRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<SubjectModel>> GetAllAsync()
        {
            var subjects = await subjectRepository.GetAllAsync();
            var subjectsModels = mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectModel>>(subjects);
            return subjectsModels;

        }

        public async Task<IEnumerable<SubjectModel>> GetByFilterAsync(FilterSearchModel filterSearchModel)
        {
            var subjects = await subjectRepository.GetAllAsync();
            subjects = subjects?.Where(x => String.IsNullOrEmpty(filterSearchModel.SearchString) || x.Title.Contains(filterSearchModel.SearchString));
            var subjectsModels = subjects is not null ? mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectModel>>(subjects) : new List<SubjectModel>();
            return subjectsModels;
        }

        public async Task<SubjectModel> GetByIdAsync(int id)
        {
            var subject = await subjectRepository.GetByIdAsync(id);
            var subjectModel = mapper.Map<Subject, SubjectModel>(subject);
            return subjectModel;
        }

        public async Task UpdateAsync(SubjectModel model)
        {
            var subject = mapper.Map<Subject>(model);
            await subjectRepository.UpdateAsync(subject);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

