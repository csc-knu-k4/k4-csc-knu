using System;
using OsvitaDAL.Entities;

namespace OsvitaDAL.Interfaces
{
	public interface IEducationClassPlanRepository : IRepository<EducationClassPlan>
    {
        Task<List<AssignmentSetPlanDetail>> GetAssignmentSetPlanDetailsByEducationClassPlanIdAsync(int id);
        Task<EducationClassPlan> GetEducationClassPlanByEducationClassIdWithDetailsAsync(int educationClassId);
    }
}

