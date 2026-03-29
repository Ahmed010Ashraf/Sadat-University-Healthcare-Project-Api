using AutoMapper;
using BLL.Dtos.MedicalExamination;
using DAL.Models;

namespace BLL.MappingProfile
{
    public class MedicalExaminationProfile : Profile
    {
        public MedicalExaminationProfile()
        {
            CreateMap<MedicalExamination, MedicalExaminationResultDto>().ReverseMap();
            CreateMap<CreateOrUpdateMedicalExaminationDto, MedicalExamination>();
        }
    }
}
