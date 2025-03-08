using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;
using OsvitaDAL.Repositories;

namespace OsvitaBLL.Services
{
	public class EducationClassPlanService : IEducationClassPlanService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEducationClassPlanRepository educationClassPlanRepository;
        private readonly IMapper mapper;

        public EducationClassPlanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.educationClassPlanRepository = unitOfWork.EducationClassPlanRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddAsync(EducationClassPlanModel model)
        {
            var educationClassPlan = mapper.Map<EducationClassPlan>(model);
            await educationClassPlanRepository.AddAsync(educationClassPlan);
            await unitOfWork.SaveChangesAsync();
            return educationClassPlan.Id;
        }

        public async Task<int> AddAssignmentSetPlanDetailAsync(AssignmentSetPlanDetailModel model, int educationClassId)
        {
            var assignmentSetPlanDetail = mapper.Map<AssignmentSetPlanDetail>(model);
            var educationClassPlan = await educationClassPlanRepository.GetEducationClassPlanByEducationClassIdWithDetailsAsync(educationClassId);
            educationClassPlan.AssignmentSetPlanDetails.Add(assignmentSetPlanDetail);
            await educationClassPlanRepository.UpdateAsync(educationClassPlan);
            await unitOfWork.SaveChangesAsync();
            return assignmentSetPlanDetail.Id;
        }

        public async Task DeleteAsync(EducationClassPlanModel model)
        {
            await educationClassPlanRepository.DeleteByIdAsync(model.Id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await educationClassPlanRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<EducationClassPlanModel>> GetAllAsync()
        {
            var educationClassPlans = await educationClassPlanRepository.GetAllAsync();
            foreach (var educationClassPlan in educationClassPlans)
            {
                educationClassPlan.AssignmentSetPlanDetails = (await unitOfWork.EducationClassPlanRepository.GetAssignmentSetPlanDetailsByEducationClassPlanIdAsync(educationClassPlan.Id)).ToList();
            }
            var educationClassPlansModels = mapper.Map<IEnumerable<EducationClassPlan>, IEnumerable<EducationClassPlanModel>>(educationClassPlans);
            return educationClassPlansModels;
        }

        public async Task<EducationClassPlanModel> GetByIdAsync(int id)
        {
            var educationClassPlan = await educationClassPlanRepository.GetByIdAsync(id);
            educationClassPlan.AssignmentSetPlanDetails = (await unitOfWork.EducationClassPlanRepository.GetAssignmentSetPlanDetailsByEducationClassPlanIdAsync(educationClassPlan.Id)).ToList();
            var educationClassPlanModel = mapper.Map<EducationClassPlan, EducationClassPlanModel>(educationClassPlan);
            return educationClassPlanModel;
        }

        public async Task<EducationClassPlanModel> GetEducationPlanByEducationClassIdAsync(int educationClassId)
        {
            var educationClassPlan = await educationClassPlanRepository.GetEducationClassPlanByEducationClassIdWithDetailsAsync(educationClassId);
            educationClassPlan.AssignmentSetPlanDetails = (await unitOfWork.EducationClassPlanRepository.GetAssignmentSetPlanDetailsByEducationClassPlanIdAsync(educationClassPlan.Id)).ToList();
            var educationClassPlanModel = mapper.Map<EducationClassPlan, EducationClassPlanModel>(educationClassPlan);
            return educationClassPlanModel;
        }

        public async Task UpdateAsync(EducationClassPlanModel model)
        {
            var educationClassPlan = mapper.Map<EducationClassPlan>(model);
            await educationClassPlanRepository.UpdateAsync(educationClassPlan);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

