using AutoMapper;
using BLL.Dtos.PharmacyPayment;
using DAL.Models;

namespace BLL.MappingProfile
{
    public class PharmacyPaymentProfile : Profile
    {
        public PharmacyPaymentProfile()
        {
            CreateMap<CreateOrUpdatePharmacyPaymentDto, PharmacyPayment>();
            CreateMap<PharmacyPayment, PharmacyPaymentResultDto>();
        }
    }
}
