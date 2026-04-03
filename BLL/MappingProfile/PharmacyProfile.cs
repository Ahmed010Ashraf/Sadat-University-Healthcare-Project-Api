using AutoMapper;
using BLL.Dtos.Pharmacy;
using DAL.Models;

namespace BLL.MappingProfile
{
    public class PharmacyProfile : Profile
    {
        public PharmacyProfile()
        {
            CreateMap<Pharmacy, PharmacyResultDto>();
            CreateMap<CreateOrUpdatePharmacyDto, Pharmacy>();
        }
    }
}
