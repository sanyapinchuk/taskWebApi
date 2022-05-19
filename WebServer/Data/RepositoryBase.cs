﻿using System.Linq.Expressions;
using WebServer.Data.Interfaces;

namespace WebServer.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataContext dataContext;

        public RepositoryBase(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async void CreateAsync(T entity)
        {
            await dataContext.Set<T>().AddAsync(entity);
        }

        public void DeleteAsync(T entity)
        {
             dataContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dataContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return await dataContext.Set<T>()
                .Where(expression).ToListAsync();
        }

        public async void UpdateAsync(T entity)
        {
            await dataContext.Set<T>().Update(entity).ReloadAsync();
        }
    }
}
