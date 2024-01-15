using AutoMapper;
using PriceCheck.BusinessLogic.Dtos;
using PriceCheck.BusinessLogic.Dtos.ATB;
using PriceCheck.BusinessLogic.Interfaces;
using PriceCheck.Data.Entities;
using PriceCheck.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.BusinessLogic.Services
{
    public class ATBService : IATBService
    {
        private readonly IShopRepository _repository;
        private readonly IMapper _mapper;
        public ATBService(IShopRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IShopDto> CreateShopProduct(IShopDto shopDto)
        {
            var product = _mapper.Map<ATB>(shopDto);
            _repository.Add(product);
            await _repository.SaveChangesAsync();
            var productDto = _mapper.Map<ATBDto>(product);
            return productDto;

        }

        public async Task DeleteShopProduct(int id)
        {
            await _repository.Delete<ATB>(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<IShopDto>> GetAllShopProducts()
        {
            var productList = await _repository.GetAll<ATB>();
            var productDtoList = _mapper.Map<List<ATBDto>>(productList);
            return productDtoList;
        }

        public async Task<IShopDto> GetShopProduct(int id)
        {
            var product = await _repository.GetById<ATB>(id);
            var productDto = _mapper.Map<ATBDto>(product);
            return productDto;
        }

        public async Task<IShopDto> GetShopProductByLink(string link)
        {
            var product = await _repository.GetByLink<ATB>(link);
            var productDto = _mapper.Map<ATBDto>(product);
            return productDto;
        }

        public async Task<IShopDto> GetShopProductByName(string name)
        {
            var product = await _repository.GetByName<ATB>(name);
            var productDto = _mapper.Map<ATBDto>(product);
            return productDto;
        }

        public async Task UpdateShopProduct(int id, IShopUpdateDto shopUpdateDto)
        {
            var product = await _repository.GetById<ATB>(id);
            product = _mapper.Map(shopUpdateDto, product);
            await _repository.SaveChangesAsync();
        }
    }
}
