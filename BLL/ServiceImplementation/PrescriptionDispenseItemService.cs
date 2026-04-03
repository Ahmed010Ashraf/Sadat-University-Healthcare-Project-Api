using AutoMapper;
using BLL.Dtos.PrescriptionDispenseItem;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using DAL.repositories.RepoAbstraction;

namespace BLL.ServiceImplementation
{
    public class PrescriptionDispenseItemService : IPrescriptionDispenseItemService
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public PrescriptionDispenseItemService(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PrescriptionDispenseItemResultDto>> GetAll()
        {
            var items = await _uow.GetReposatory<PrescriptionDispenseItem, Guid>().GetAll();
            return _mapper.Map<IEnumerable<PrescriptionDispenseItemResultDto>>(items);
        }

        public async Task<PrescriptionDispenseItemResultDto> GetById(Guid id)
        {
            var entity = await _uow.GetReposatory<PrescriptionDispenseItem, Guid>().GetById(id);
            if (entity is null) throw new PrescriptionDispenseItemNotFoundException(id);
            return _mapper.Map<PrescriptionDispenseItemResultDto>(entity);
        }

        public async Task<PrescriptionDispenseItemResultDto> Create(CreateOrUpdatePrescriptionDispenseItemDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            // Ensure the prescription dispense exists
            var dispenseRepo = _uow.GetReposatory<PrescriptionDispense, Guid>();
            var dispense = await dispenseRepo.GetById(dto.PrescriptionDispenseId);
            if (dispense is null)
                throw new InvalidOperationException($"PrescriptionDispense with id {dto.PrescriptionDispenseId} not found.");

            // Ensure the prescription item exists
            var itemRepo = _uow.GetReposatory<PrescriptionItem, Guid>();
            var item = await itemRepo.GetById(dto.PrescriptionItemId);
            if (item is null)
                throw new InvalidOperationException($"PrescriptionItem with id {dto.PrescriptionItemId} not found.");

            var entity = _mapper.Map<PrescriptionDispenseItem>(dto);
            await _uow.GetReposatory<PrescriptionDispenseItem, Guid>().Create(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to create prescription dispense item.");
            return _mapper.Map<PrescriptionDispenseItemResultDto>(entity);
        }

        public async Task Update(Guid id, CreateOrUpdatePrescriptionDispenseItemDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            var repo = _uow.GetReposatory<PrescriptionDispenseItem, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new PrescriptionDispenseItemNotFoundException(id);

            // Ensure the prescription dispense exists
            var dispenseRepo = _uow.GetReposatory<PrescriptionDispense, Guid>();
            var dispense = await dispenseRepo.GetById(dto.PrescriptionDispenseId);
            if (dispense is null)
                throw new InvalidOperationException($"PrescriptionDispense with id {dto.PrescriptionDispenseId} not found.");

            // Ensure the prescription item exists
            var itemRepo = _uow.GetReposatory<PrescriptionItem, Guid>();
            var item = await itemRepo.GetById(dto.PrescriptionItemId);
            if (item is null)
                throw new InvalidOperationException($"PrescriptionItem with id {dto.PrescriptionItemId} not found.");

            _mapper.Map(dto, entity);
            repo.Update(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to update prescription dispense item.");
        }

        public async Task Delete(Guid id)
        {
            var repo = _uow.GetReposatory<PrescriptionDispenseItem, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new PrescriptionDispenseItemNotFoundException(id);
            repo.Delete(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to delete prescription dispense item.");
        }
    }
}
