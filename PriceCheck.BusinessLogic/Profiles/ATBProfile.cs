using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PriceCheck.BusinessLogic.Dtos.ATB;
using PriceCheck.Data.Entities;

namespace PriceCheck.BusinessLogic.Profiles
{
    public class ATBProfile : Profile
    {
        public ATBProfile() 
        {
            CreateMap<ATB, ATBDto>();
            CreateMap<ATBDto, ATB>();
            CreateMap<ATBUpdateDto, ATB>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<string?, string>().ConvertUsing((src, dest) => src ?? dest);
        }
    }
}
