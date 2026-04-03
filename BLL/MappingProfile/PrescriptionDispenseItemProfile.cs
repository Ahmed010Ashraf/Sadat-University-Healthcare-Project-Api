using AutoMapper;
using BLL.Dtos.PrescriptionDispenseItem;
using DAL.Models;

namespace BLL.MappingProfile
{
    public class PrescriptionDispenseItemProfile : Profile
    {
        public PrescriptionDispenseItemProfile()
        {
            CreateMap<CreateOrUpdatePrescriptionDispenseItemDto, PrescriptionDispenseItem>();
            CreateMap<PrescriptionDispenseItem, PrescriptionDispenseItemResultDto>();

        }
    }
}
