﻿using CRM_Core.Application.Interfaces;
using CRM_Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Core.DataAccessLayer.Repositories
{
   public class RepositoryBase<T> : Application.Interfaces.RepositoryBase<T> where T : class
    {
        protected CRM_CoreDB RepositoryContext { get; set; }
        private DbSet<T> _dbSet;


        public RepositoryBase(CRM_CoreDB repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
            _dbSet = repositoryContext.Set<T>();
        }

        public IQueryable<T> FindAll()
        {
            return _dbSet;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            this.RepositoryContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await this.RepositoryContext.SaveChangesAsync();
        }
    }
}
