using AutoMapper;
using BLL.Dtos.HospitalPayment;
using DAL.Models;

namespace BLL.MappingProfile
{
    public class HospitalPaymentProfile : Profile
    {
        public HospitalPaymentProfile()
        {
            CreateMap<HospitalPayment, HospitalPaymentResultDto>();
            CreateMap<CreateOrUpdateHospitalPaymentDto, HospitalPayment>();
        }
    }
}
