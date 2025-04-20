using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaBLL.Services
{
	public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly IDailyAssignmentService dailyAssignmentService;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IDailyAssignmentService dailyAssignmentService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            userRepository = unitOfWork.UserRepository;
            this.dailyAssignmentService = dailyAssignmentService;
            this.mapper = mapper;
        }

        public async Task<int> AddAsync(UserModel model)
        {
            var user = mapper.Map<User>(model);
            user.Statistic = new Statistic();
            user.EducationPlan = new EducationPlan();
            await userRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync();
            // generate daily assignments
            for (int i = 0; i < 10; i++)
            {
                await dailyAssignmentService.AddDailyAssignmentAsync(user.Id);
            }
            return user.Id;
        }

        public async Task DeleteAsync(UserModel model)
        {
            await userRepository.DeleteByIdAsync(model.Id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await userRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await userRepository.GetAllAsync();
            var usersModels = mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(users);
            return usersModels;
        }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            var user = (await userRepository.GetAllAsync()).FirstOrDefault(x => x.Email == email);
            var userModel = mapper.Map<User, UserModel>(user);
            return userModel;
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user = await userRepository.GetByIdWithDetailsAsync(id);
            var userModel = mapper.Map<User, UserModel>(user);
            return userModel;
        }

        public async Task UpdateAsync(UserModel model)
        {
            var user = mapper.Map<User>(model);
            await userRepository.UpdateAsync(user);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

