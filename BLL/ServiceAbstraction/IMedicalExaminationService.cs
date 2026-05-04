using BLL.Dtos.MedicalExamination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.ServiceAbstraction
{
    public interface IMedicalExaminationService
    {
        Task<IEnumerable<MedicalExaminationResultDto>> GetAll();
        Task<MedicalExaminationResultDto> GetById(Guid id);

        Task<IEnumerable<MedicalExaminationResultDto>> GetMedicalExaminationByUserId(Guid userId);

        Task<MedicalExaminationResultDto> GetMedicalExaminationByRequestId(Guid requestId);
        Task<MedicalExaminationResultDto> Create(Guid userId, CreateOrUpdateMedicalExaminationDto dto);
        Task Update(Guid id, CreateOrUpdateMedicalExaminationDto dto);
        Task Delete(Guid id);
    }
}
