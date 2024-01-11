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
    public class ATBRepository : EFCoreRepository, IShopRepository
    {
        private readonly PriceCheckContext PCDbContext;
        public ATBRepository(PriceCheckContext DbContext) : base(DbContext)
        {
            PCDbContext = DbContext;
        }
        public async Task<IShop> GetByName(string name)
        {
            return await PCDbContext.ATB.FirstOrDefaultAsync(e => e.ProductName == name);
        }


    }
}
