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
        Task<IShop> GetByName(string name);

    }
}
