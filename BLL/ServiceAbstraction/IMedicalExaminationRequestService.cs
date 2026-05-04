using BLL.Dtos.MedicalExaminationRequest;

namespace BLL.ServiceAbstraction
{
    public interface IMedicalExaminationRequestService
    {
        Task<IEnumerable<MedicalExaminationRequestResultDto>> GetAll();
        Task<MedicalExaminationRequestResultDto> GetById(Guid id);

        Task<IEnumerable<MedicalExaminationRequestResultDto>> GetMedicalExaminationRequestByUserId(Guid userId);
        Task<MedicalExaminationRequestResultDto> Create(Guid UserId, CreateOrUpdateMedicalExaminationRequestDto createOrUpdateMedicalExaminationRequestDto);
        Task<MedicalExaminationRequestResultDto> Update(Guid UserId, Guid id, CreateOrUpdateMedicalExaminationRequestDto createOrUpdateMedicalExaminationRequestDto);
        Task<IEnumerable<MedicalExaminationRequestResultDto>> GetByUserId(Guid userId);
        Task<bool> Delete(Guid id);
    }
}
