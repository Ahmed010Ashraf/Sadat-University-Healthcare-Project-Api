using BLL.Dtos.PrescriptionRequest;

namespace BLL.ServiceAbstraction
{
    public interface IPrescriptionRequestService
    {
        Task<IEnumerable<PrescriptionRequestResultDto>> GetAll();
        Task<PrescriptionRequestResultDto> GetById(Guid id);
        Task<PrescriptionRequestResultDto> Create(Guid userId, CreateOrUpdatePrescriptionRequestDto dto);
        Task<PrescriptionRequestResultDto> Update(Guid userId, Guid id, CreateOrUpdatePrescriptionRequestDto dto);
        Task<bool> Delete(Guid id);
    }
}
