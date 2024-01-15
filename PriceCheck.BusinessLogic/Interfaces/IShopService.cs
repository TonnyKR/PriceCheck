using PriceCheck.BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.BusinessLogic.Interfaces
{
    public interface IShopService
    {
        Task<IShopDto> GetShopProduct(int id);
        Task<IShopDto> GetShopProductByName(string name);
        Task<IShopDto> GetShopProductByLink(string link);
        Task<IEnumerable<IShopDto>> GetAllShopProducts();
        Task<IShopDto> CreateShopProduct(IShopDto shopDto);

        Task UpdateShopProduct(int id, IShopUpdateDto shopUpdateDto);

        Task DeleteShopProduct(int id);
    }
}
