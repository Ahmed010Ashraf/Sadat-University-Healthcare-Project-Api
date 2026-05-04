using BLL.Dtos.PrescriptionDispenseItem;

namespace BLL.ServiceAbstraction
{
    public interface IPrescriptionDispenseItemService
    {
        Task<IEnumerable<PrescriptionDispenseItemResultDto>> GetAll();
        Task<PrescriptionDispenseItemResultDto> GetById(Guid id);
        Task<PrescriptionDispenseItemResultDto> Create(CreateOrUpdatePrescriptionDispenseItemDto dto);
        Task Update(Guid id, CreateOrUpdatePrescriptionDispenseItemDto dto);
        Task Delete(Guid id);
    }
}
