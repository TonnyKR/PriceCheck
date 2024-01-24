using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceCheck.Data.Entities;
using PriceCheck.Data.Interfaces;


namespace PriceCheck.Data.Repository
{
    public class EFCoreRepository : IRepository
    {
        private readonly PriceCheckContext priceCheckDbContext;

        public EFCoreRepository(PriceCheckContext DbContext)
        {
            priceCheckDbContext = DbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>() where TEntity : BaseEntity
        {
            return await priceCheckDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById<TEntity>(int? id) where TEntity : BaseEntity
        {
            return await priceCheckDbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await priceCheckDbContext.SaveChangesAsync();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            priceCheckDbContext.Set<TEntity>().Add(entity);
        }

        public async Task<TEntity> Delete<TEntity>(int id) where TEntity : BaseEntity
        {
            var entity = await priceCheckDbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new ValidationException($"Object of type {typeof(TEntity)} with id { id } not found");
            }

            priceCheckDbContext.Set<TEntity>().Remove(entity);

            return entity;
        }

        
    }
}
