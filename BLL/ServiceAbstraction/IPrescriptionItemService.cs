using BLL.Dtos.PrescriptionItem;

namespace BLL.ServiceAbstraction
{
    public interface IPrescriptionItemService
    {
        Task<IEnumerable<PrescriptionItemResultDto>> GetAll();
        Task<PrescriptionItemResultDto> GetById(Guid id);
        Task<PrescriptionItemResultDto> Create(CreateOrUpdatePrescriptionItemDto dto);
        Task Update(Guid id, CreateOrUpdatePrescriptionItemDto dto);
        Task Delete(Guid id);
    }
}
