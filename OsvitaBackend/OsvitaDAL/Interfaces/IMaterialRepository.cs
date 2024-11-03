using System;
using OsvitaDAL.Entities;

namespace OsvitaDAL.Interfaces
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<IEnumerable<Material>> GetAllWithDetailsAsync();
        Task<Material> GetByIdWithDetailsAsync(int id);
    }
}

