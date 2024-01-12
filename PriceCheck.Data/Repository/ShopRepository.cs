using Microsoft.EntityFrameworkCore;
using PriceCheck.Data.Entities;
using PriceCheck.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.Data.Repository
{
    public class ShopRepository : EFCoreRepository, IShopRepository
    {
        private readonly PriceCheckContext PCDbContext;
        public ShopRepository(PriceCheckContext DbContext) : base(DbContext)
        {
            PCDbContext = DbContext;
        }

        public async Task<TEntity> GetByLink<TEntity>(string link) where TEntity : Shop
        {
            return await PCDbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.ProductLink == link);
        }

        public async Task<TEntity> GetByName<TEntity>(string name) where TEntity : Shop
        {
            return await PCDbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.ProductName == name);
        }
    }
}
