using System;
using AutoMapper;
using Microsoft.Extensions.Options;
using OsvitaBLL.Configurations;
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
        private readonly IEmailService emailService;
        private readonly IMapper mapper;
        private readonly HostSettings settings;

        public EducationClassService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, IOptions<HostSettings> settings)
        {
            this.unitOfWork = unitOfWork;
            this.educationClassRepository = unitOfWork.EducationClassRepository;
            this.mapper = mapper;
            this.emailService = emailService;
            this.settings = settings.Value;
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

        public async Task InviteStudentByEmailAsync(string email, int educationClassId)
        {
            var user = (await unitOfWork.UserRepository.GetAllAsync()).FirstOrDefault(x => x.Email == email);
            if (user is not null)
            {
                var invitation = new EducationClassInvitation
                {
                    Guid = Guid.NewGuid().ToString(),
                    UserId = user.Id,
                    EducationClassId = educationClassId,
                    CreatedDate = DateTime.Now
                };
                await educationClassRepository.AddEducationClassInvitationAsync(invitation);
                var subject = "Invitation to class";
                var message = $"Your invitation link is {settings.BaseUrl}/api/classes/{educationClassId}/students/{user.Id}/confirmations/{invitation.Guid}";
                await emailService.SendEmailAsync(email, subject, message);
            }
            await unitOfWork.SaveChangesAsync();
        }

        public async Task ConfirmStudentAsync(int id, int educationClassId, string guid)
        {
            var invitation = await educationClassRepository.GetEducationClassInvitationByGuidAsync(guid);
            if (invitation is not null)
            {
                var user = await unitOfWork.UserRepository.GetByIdAsync(id);
                if (user is not null)
                {
                    var educationClass = await educationClassRepository.GetByIdWithDetailsAsync(educationClassId);
                    if (educationClass is not null && !educationClass.Students.Select(x => x.Id).Contains(user.Id))
                    {
                        educationClass.Students.Add(user);
                        await educationClassRepository.DeleteEducationClassInvitationByGuidAsync(guid);
                        await unitOfWork.SaveChangesAsync();
                    }
                }
            }
        }
    }
}

