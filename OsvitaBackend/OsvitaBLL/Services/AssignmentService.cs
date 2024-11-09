using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaBLL.Services
{
    public partial class AssignmentService : IAssignmentService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IChapterRepository chapterRepository;
        private readonly IMapper mapper;

        public AssignmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;

            this.mapper = mapper;
        }
        Task ICrud<AssignmentModel>.AddAsync(AssignmentModel entity)
        {
            throw new NotImplementedException();
        }

        Task ICrud<AssignmentModel>.DeleteAsync(AssignmentModel entity)
        {
            throw new NotImplementedException();
        }

        Task ICrud<AssignmentModel>.DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<AssignmentModel>> ICrud<AssignmentModel>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<AssignmentModel> ICrud<AssignmentModel>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task ICrud<AssignmentModel>.UpdateAsync(AssignmentModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
