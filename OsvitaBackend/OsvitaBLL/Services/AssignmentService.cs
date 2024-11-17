using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;
using OsvitaDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OsvitaBLL.Services
{
    public partial class AssignmentService : IAssignmentService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IAssignmentLinkRepository assignmentLinkRepository;
        private readonly IMapper mapper;

        public AssignmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            assignmentRepository = unitOfWork.AssignmentRepository;
            answerRepository = unitOfWork.AnswerRepository;
            assignmentLinkRepository = unitOfWork.AssignmentLinkRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddAssignmentAsync(AssignmentModel model)
        {
            int id = await AddOneAssignmentAsync(model);
            if (model.AssignmentModelType == AssignmentModelType.MatchComplianceAssignment)
            {
                foreach (var childAssignment in model.ChildAssignments)
                {
                    childAssignment.ParentAssignmentId = id;
                    childAssignment.ObjectId = null;
                    await AddOneAssignmentAsync(childAssignment);
                }
            }
            return id;
        }

        public async Task<IEnumerable<AssignmentModel>> GetAllAssignmentsAsync()
        {
            var assignments = (await assignmentRepository.GetAllWithDetailsAsync()).ToList().Where(x => x.AssignmentType != AssignmentType.ChildAssignment);
            var assignmentsModels = mapper.Map<IEnumerable<Assignment>, IEnumerable<AssignmentModel>>(assignments);
            foreach (var assignmentModel in assignmentsModels)
            {
                assignmentModel.ChildAssignments = (await GetChildAssignmentsAsync(assignmentModel.Id)).ToList();
            }
            return assignmentsModels;
        }

        public async Task<IEnumerable<AssignmentModel>> GetAssignmentsByObjectIdAsync(int objectId, ObjectModelType objectModelType)
        {
            var objectType = mapper.Map<ObjectModelType, ObjectType>(objectModelType);
            var assignmentsIds = (await assignmentLinkRepository.GetAllAsync()).Where(x => x.ObjectId == objectId && x.ObjectType == objectType).Select(x => x.AssignmentId);
            var assignments = (await assignmentRepository.GetAllWithDetailsAsync()).ToList().Where(x => x.AssignmentType != AssignmentType.ChildAssignment && assignmentsIds.Contains(x.Id));
            var assignmentsModels = mapper.Map<IEnumerable<Assignment>, IEnumerable<AssignmentModel>>(assignments);
            foreach (var assignmentModel in assignmentsModels)
            {
                assignmentModel.ChildAssignments = (await GetChildAssignmentsAsync(assignmentModel.Id)).ToList();
            }
            return assignmentsModels;
        }

        public async Task<AssignmentModel> GetAssignmentByIdAsync(int id)
        {
            var assignment = await assignmentRepository.GetByIdWithDetailsAsync(id);
            var assignmentModel = mapper.Map<Assignment, AssignmentModel>(assignment);
            assignmentModel.ChildAssignments = (await GetChildAssignmentsAsync(assignmentModel.Id)).ToList();
            return assignmentModel;
        }

        public async Task DeleteAssignmentByIdAsync(int id)
        {
            var assignment = await assignmentRepository.GetByIdAsync(id);
            if (assignment.AssignmentType == AssignmentType.MatchComplianceAssignment)
            {
                var childAssignments = await GetChildAssignmentsAsync(id);
                foreach (var childAssignment in childAssignments)
                {
                    await assignmentRepository.DeleteByIdAsync(childAssignment.Id);
                }
            }
            var assignmentLinks = (await assignmentLinkRepository.GetAllAsync()).Where(x => x.AssignmentId == id);
            foreach (var assignmentLink in assignmentLinks)
            {
                await assignmentLinkRepository.DeleteByIdAsync(assignmentLink.Id);
            }
            await assignmentRepository.DeleteByIdAsync(id);

        }

        public async Task UpdateAssignmentAsync(AssignmentModel model)
        {
            await UpdateOneAssignmentAsync(model);
            if (model.AssignmentModelType == AssignmentModelType.MatchComplianceAssignment)
            {
                foreach (var childAssignment in model.ChildAssignments)
                {
                    childAssignment.ParentAssignmentId = model.Id;
                    childAssignment.ObjectId = null;
                    var oldChildAssignmentsIds = (await assignmentRepository.GetAllAsync()).Where(x => x.Id == childAssignment.Id).Select(x => x.Id);
                    if (oldChildAssignmentsIds.Contains(childAssignment.Id))
                    {
                        await UpdateOneAssignmentAsync(childAssignment);
                    }
                    else
                    {
                        await AddOneAssignmentAsync(childAssignment);
                    }
                }
            }
        }

        private async Task<IEnumerable<AssignmentModel>> GetChildAssignmentsAsync(int parentAssignmentId)
        {
            var childAssignments = (await assignmentRepository.GetAllWithDetailsAsync()).Where(x => x.ParentAssignmentId == parentAssignmentId);
            var childAssignmentsModels = mapper.Map<IEnumerable<Assignment>, IEnumerable<AssignmentModel>>(childAssignments);
            return childAssignmentsModels;
        }

        private async Task<int> AddOneAssignmentAsync(AssignmentModel model)
        {
            var assignment = mapper.Map<Assignment>(model);
            await assignmentRepository.AddAsync(assignment);
            await unitOfWork.SaveChangesAsync();
            if (model.ObjectId is not null)
            {
                var assignmentLink = new AssignmentLink { AssignmentId = assignment.Id, ObjectId = (int)model.ObjectId, ObjectType = ObjectType.Material };
                await assignmentLinkRepository.AddAsync(assignmentLink);
                await unitOfWork.SaveChangesAsync();
            }
            return assignment.Id;
        }

        private async Task UpdateOneAssignmentAsync(AssignmentModel model)
        {
            var assignment = mapper.Map<Assignment>(model);
            await assignmentRepository.UpdateAsync(assignment);
            await unitOfWork.SaveChangesAsync();
            if (model.ObjectId is not null)
            {
                var assignmentLink = (await assignmentLinkRepository.GetAllAsync()).FirstOrDefault(x => x.AssignmentId == model.Id);
                if (assignmentLink is not null)
                {
                    assignmentLink.ObjectId = (int)model.ObjectId;
                    await assignmentLinkRepository.UpdateAsync(assignmentLink);
                    await unitOfWork.SaveChangesAsync();
                }
                else
                {
                    assignmentLink = new AssignmentLink { AssignmentId = assignment.Id, ObjectId = (int)model.ObjectId, ObjectType = ObjectType.Material };
                    await assignmentLinkRepository.AddAsync(assignmentLink);
                    await unitOfWork.SaveChangesAsync();
                }
            }
        }
    }
}
