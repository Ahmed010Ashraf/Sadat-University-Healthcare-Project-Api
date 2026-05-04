using AutoMapper;
using BLL.Dtos.PrescriptionItem;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using DAL.repositories.RepoAbstraction;

namespace BLL.ServiceImplementation
{
    public class PrescriptionItemService : IPrescriptionItemService
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public PrescriptionItemService(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PrescriptionItemResultDto>> GetAll()
        {
            var items = await _uow.GetReposatory<PrescriptionItem, Guid>().GetAll();
            return _mapper.Map<IEnumerable<PrescriptionItemResultDto>>(items);
        }

        public async Task<PrescriptionItemResultDto> GetById(Guid id)
        {
            var entity = await _uow.GetReposatory<PrescriptionItem, Guid>().GetById(id);
            if (entity is null) throw new PrescriptionItemNotFoundException(id);
            return _mapper.Map<PrescriptionItemResultDto>(entity);
        }

        public async Task<PrescriptionItemResultDto> Create(CreateOrUpdatePrescriptionItemDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            // Ensure the prescription exists
            var prescriptionRepo = _uow.GetReposatory<Prescription, Guid>();
            var prescription = await prescriptionRepo.GetById(dto.PrescriptionId);
            if (prescription is null)
                throw new InvalidOperationException($"Prescription with id {dto.PrescriptionId} not found.");

            var entity = _mapper.Map<PrescriptionItem>(dto);
            await _uow.GetReposatory<PrescriptionItem, Guid>().Create(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to create prescription item.");
            return _mapper.Map<PrescriptionItemResultDto>(entity);
        }

        public async Task Update(Guid id, CreateOrUpdatePrescriptionItemDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            var repo = _uow.GetReposatory<PrescriptionItem, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new PrescriptionItemNotFoundException(id);

            // Ensure the prescription exists
            var prescriptionRepo = _uow.GetReposatory<Prescription, Guid>();
            var prescription = await prescriptionRepo.GetById(dto.PrescriptionId);
            if (prescription is null)
                throw new InvalidOperationException($"Prescription with id {dto.PrescriptionId} not found.");

            _mapper.Map(dto, entity);
            repo.Update(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to update prescription item.");
        }

        public async Task Delete(Guid id)
        {
            var repo = _uow.GetReposatory<PrescriptionItem, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new PrescriptionItemNotFoundException(id);
            repo.Delete(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to delete prescription item.");
        }
    }
}
