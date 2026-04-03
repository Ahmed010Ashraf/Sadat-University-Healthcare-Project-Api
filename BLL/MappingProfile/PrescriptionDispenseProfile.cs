using AutoMapper;
using BLL.Dtos.PrescriptionDispense;
using DAL.Models;

namespace BLL.MappingProfile
{
    public class PrescriptionDispenseProfile : Profile
    {
        public PrescriptionDispenseProfile()
        {
            CreateMap<CreateOrUpdatePrescriptionDispenseDto, PrescriptionDispense>();
            CreateMap<PrescriptionDispense, PrescriptionDispenseResultDto>();
        }
    }
}
