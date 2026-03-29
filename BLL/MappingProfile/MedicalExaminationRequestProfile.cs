using AutoMapper;
using BLL.Dtos.MedicalExaminationRequest;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MappingProfile
{
    public class MedicalExaminationRequestProfile:Profile
    {
        public MedicalExaminationRequestProfile()
        {
            CreateMap<MedicalExaminationRequest, MedicalExaminationRequestResultDto>()
                .ForMember(dest=>dest.MedicalReportPath , opt=>opt.MapFrom<PicUrlResolver>());
            CreateMap<CreateOrUpdateMedicalExaminationRequestDto, MedicalExaminationRequest>();
        }
    }

    public class PicUrlResolver(IConfiguration _configurations) : IValueResolver<MedicalExaminationRequest, MedicalExaminationRequestResultDto, string>
    {
        public string Resolve(MedicalExaminationRequest source, MedicalExaminationRequestResultDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.MedicalReportPath)) {
                return $"{_configurations.GetSection("URLs")["BaseUrl"]}{source.MedicalReportPath}";
                
            }
            return null;
        }
    }
}
