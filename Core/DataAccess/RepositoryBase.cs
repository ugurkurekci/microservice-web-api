﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core
{
    public class RepositoryBase<TEntity, TContext> : IRepository<TEntity> where TEntity : class, IEntity, new() where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            };
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            };
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using TContext orderdb = new TContext();
            return orderdb.Set<TEntity>().SingleOrDefault(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using TContext orderdb = new TContext();
            return filter == null ? orderdb.Set<TEntity>().ToList() : orderdb.Set<TEntity>().Where(filter).ToList();
        }

       
        public void Update(TEntity entity)
        {
            using TContext context = new TContext();
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
