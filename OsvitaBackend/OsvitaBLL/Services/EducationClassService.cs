using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaBLL.Services
{
	public class EducationClassService : IEducationClassService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEducationClassRepository educationClassRepository;
        private readonly IMapper mapper;

        public EducationClassService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.educationClassRepository = unitOfWork.EducationClassRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddAsync(EducationClassModel model)
        {
            var educationClass = mapper.Map<EducationClass>(model);
            await educationClassRepository.AddAsync(educationClass);
            await unitOfWork.SaveChangesAsync();
            return educationClass.Id;
        }

        public async Task DeleteAsync(EducationClassModel model)
        {
            await educationClassRepository.DeleteByIdAsync(model.Id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await educationClassRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<EducationClassModel>> GetAllAsync()
        {
            var educationClasses = await educationClassRepository.GetAllWithDetailsAsync();
            var educationClassesModels = mapper.Map<IEnumerable<EducationClass>, IEnumerable<EducationClassModel>>(educationClasses);
            return educationClassesModels;
        }

        public async Task<EducationClassModel> GetByIdAsync(int id)
        {
            var educationClass = await educationClassRepository.GetByIdWithDetailsAsync(id);
            var educationClassesModel = mapper.Map<EducationClass, EducationClassModel>(educationClass);
            return educationClassesModel;
        }

        public async Task UpdateAsync(EducationClassModel model)
        {
            var educationClass = mapper.Map<EducationClass>(model);
            educationClass.Students = (await unitOfWork.UserRepository.GetAllAsync()).Where(u => model.StudentsIds.Contains(u.Id)).ToList();
            await educationClassRepository.UpdateAsync(educationClass);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteStudentByIdAsync(int id, int educationClassId)
        {
            var educationClass = await educationClassRepository.GetByIdWithDetailsAsync(educationClassId);
            var userToDelete = educationClass.Students.FirstOrDefault(x => x.Id == id);
            if (userToDelete is not null)
            {
                educationClass.Students.Remove(userToDelete);
            }
            await unitOfWork.SaveChangesAsync();
        }
    }
}

