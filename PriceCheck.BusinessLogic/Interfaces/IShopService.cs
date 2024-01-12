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
        Task<IShopDto> GetShopPosition(int id);
        Task<IShopDto> GetShopPositionByName(string name);
        Task<IShopDto> GetShopPositionByLink(string link);
        Task<IEnumerable<IShopDto>> GetAllShopPositions();
        Task<IShopDto> CreateShopPosition(IShopDto shopDto);

        Task UpdateShopPosition(int id, IShopUpdateDto shopUpdateDto);

        Task DeleteShopPosition(int id);
    }
}
