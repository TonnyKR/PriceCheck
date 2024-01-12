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
    public class ATBService : IShopService
    {
        private readonly IShopRepository _repository;
        private readonly IMapper _mapper;
        public ATBService(IShopRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IShopDto> CreateShopPosition(IShopDto shopDto)
        {
            var position = _mapper.Map<ATB>(shopDto);
            _repository.Add(position);
            await _repository.SaveChangesAsync();
            var positionDto = _mapper.Map<ATBDto>(position);
            return positionDto;

        }

        public async Task DeleteShopPosition(int id)
        {
            await _repository.Delete<ATB>(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<IShopDto>> GetAllShopPositions()
        {
            var positionList = await _repository.GetAll<ATB>();
            var positionDtoList = _mapper.Map<List<ATBDto>>(positionList);
            return positionDtoList;
        }

        public async Task<IShopDto> GetShopPosition(int id)
        {
            var position  = await _repository.GetById<ATB>(id);
            var positionDto = _mapper.Map<ATBDto>(position);
            return positionDto;
        }

        public async Task<IShopDto> GetShopPositionByLink(string link)
        {
            var position = await _repository.GetByLink<ATB>(link);
            var positionDto = _mapper.Map<ATBDto>(position);
            return positionDto;
        }

        public async Task<IShopDto> GetShopPositionByName(string name)
        {
            var position = await _repository.GetByName<ATB>(name);
            var positionDto = _mapper.Map<ATBDto>(position);
            return positionDto;
        }

        public async Task UpdateShopPosition(int id, IShopUpdateDto shopUpdateDto)
        {
            var position = await _repository.GetById<ATB>(id);
            var positionDto = _mapper.Map<ATBDto>(position);
            await _repository.SaveChangesAsync();
        }
    }
}
