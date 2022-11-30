using Core.Entity;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public abstract class Repository
    {
        private readonly ApplicationContext applicationContext;

        protected Repository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        protected DbSet<T> AllSqlEntities<T>() where T : class, IEntity => applicationContext.Set<T>();
        protected void SaveChanges() => applicationContext.SaveChanges();
        protected Task<int> SaveChangesAsync() => applicationContext.SaveChangesAsync();
    }
}
