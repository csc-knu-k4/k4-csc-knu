using System;
using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
	public interface IEducationClassPlanService : ICrud<EducationClassPlanModel>
    {
        Task<int> AddAssignmentSetPlanDetailAsync(AssignmentSetPlanDetailModel model, int educationClassId);
        Task<EducationClassPlanModel> GetEducationPlanByEducationClassIdAsync(int educationClassId);
    }
}

