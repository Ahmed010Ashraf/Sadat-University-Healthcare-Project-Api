using AutoMapper;
using BLL.Dtos.Prescription;
using DAL.Models;

namespace BLL.MappingProfile
{
    public class PrescriptionProfile : Profile
    {
        public PrescriptionProfile()
        {
            CreateMap<CreateOrUpdatePrescriptionDto, Prescription>();
            CreateMap<Prescription, PrescriptionResultDto>().ReverseMap(); ;
        }
    }
}
