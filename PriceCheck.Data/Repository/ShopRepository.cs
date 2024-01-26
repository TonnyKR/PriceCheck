using Microsoft.EntityFrameworkCore;
using PriceCheck.Data.Entities;
using PriceCheck.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzySharp;

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

        public async Task<IEnumerable<TEntity>> GetByName<TEntity>(string name) where TEntity : Shop
        {
            return PCDbContext.Set<TEntity>().Where(e => e.ProductName.Contains(name));
        }
        public async Task<IEnumerable<TEntity>> GetByNameFuzzy<TEntity>(string name) where TEntity : Shop
        {
            List<TEntity> resultList = (await GetByName<TEntity>(name)).ToList();
            List<TEntity> products = await PCDbContext.Set<TEntity>().ToListAsync();
            foreach (var product in products) 
            {
                if (product.ProductName != null && !resultList.Contains(product) && Fuzz.PartialRatio(name, product.ProductName) > 75)
                {
                    resultList.Add(product);
                }
            }
            return resultList;
        }
    }
}
