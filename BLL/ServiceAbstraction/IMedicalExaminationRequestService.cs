using BLL.Dtos.MedicalExaminationRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceAbstraction
{
    public interface IMedicalExaminationRequestService
    {
        Task<IEnumerable<MedicalExaminationRequestResultDto>> GetAll();
        Task<MedicalExaminationRequestResultDto> GetById(Guid id);

        Task<MedicalExaminationRequestResultDto> Create(Guid UserId, CreateOrUpdateMedicalExaminationRequestDto createOrUpdateMedicalExaminationRequestDto);
        Task<MedicalExaminationRequestResultDto> Update(Guid UserId,Guid id , CreateOrUpdateMedicalExaminationRequestDto createOrUpdateMedicalExaminationRequestDto );

        Task<bool> Delete(Guid id);
    }
}
