using AutoMapper;
using BLL.Dtos.Hospital;
using DAL.Models;

namespace BLL.MappingProfile
{
    public class HospitalProfile : Profile
    {
        public HospitalProfile()
        {
            CreateMap<Hospital, HospitalResultDto>();
            CreateMap<CreateOrUpdateHospitalDto, Hospital>();
        }
    }
}
