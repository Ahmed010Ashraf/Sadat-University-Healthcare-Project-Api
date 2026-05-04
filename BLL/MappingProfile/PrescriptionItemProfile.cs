using AutoMapper;
using BLL.Dtos.PrescriptionItem;
using DAL.Models;

namespace BLL.MappingProfile
{
    public class PrescriptionItemProfile : Profile
    {
        public PrescriptionItemProfile()
        {
            CreateMap<CreateOrUpdatePrescriptionItemDto, PrescriptionItem>();
            CreateMap<PrescriptionItem, PrescriptionItemResultDto>();
        }
    }
}
