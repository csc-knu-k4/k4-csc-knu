using System;
using Microsoft.EntityFrameworkCore;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly OsvitaDbContext context;

        public Repository(OsvitaDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            var oldEntity = await context.Set<T>().FirstOrDefaultAsync(t => t.Id == entity.Id);
            if (oldEntity is not null)
            {
                context.Set<T>().Remove(entity);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var oldEntity = await context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
            if (oldEntity is not null)
            {
                context.Set<T>().Remove(oldEntity);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            var oldEntity = await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(t => t.Id == entity.Id);
            if (oldEntity is not null)
            {
                context.Set<T>().Update(entity);
            }
        }
    }
}
