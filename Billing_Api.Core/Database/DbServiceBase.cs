using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Billing_API.Common.Attributes;
using Billing_Api.Core.Models.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Billing_Api.Core.Database
{
    public interface IDbServiceBase
    {
        public Task<T> GetOneByExpression<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) where T : EntityBase;

        public  Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : EntityBase;

        public void UpdateManyProperty<T>(T entity, params Expression<Func<T, object>>[] propertySelectors) where T : EntityBase;

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    [InjectService(InjectAs.ImplementingInterface)]
    public class DbServiceBase : IDbServiceBase
    {
        public BillingApiDbContext DbContext;
        public DbServiceBase(BillingApiDbContext dbContext) { DbContext = dbContext; }
        public DbServiceBase() { }

        public Task<T> GetOneByExpression<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) where T : EntityBase
            => DbContext.Set<T>().Where(expression).FirstOrDefaultAsync(cancellationToken);

        public async Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : EntityBase 
        {
            await DbContext.Set<T>().AddAsync(entity, cancellationToken);
            return entity;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        public void UpdateManyProperty<T>(T entity, params Expression<Func<T, object>>[] propertySelectors) where T : EntityBase
        {
            var entry = DbContext.Entry(entity);
            foreach (var propertySelector in propertySelectors)
            {
                var prop = entry.Property(propertySelector);
                prop.IsModified = true;
            }
        }
    }
}