using PriceCheck.Data.Entities;
using PriceCheck.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.Data.Interfaces
{
    public interface IShopRepository : IRepository
    {
        Task<IEnumerable<TEntity>> GetByName<TEntity>(string name) where TEntity : Shop;
        Task<TEntity> GetByLink<TEntity>(string link) where TEntity : Shop;
        Task<IEnumerable<TEntity>> GetByNameFuzzy<TEntity>(string name) where TEntity : Shop;

    }
}
