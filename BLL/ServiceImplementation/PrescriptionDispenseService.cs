using AutoMapper;
using BLL.Dtos.PrescriptionDispense;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using DAL.repositories.RepoAbstraction;

namespace BLL.ServiceImplementation
{
    public class PrescriptionDispenseService : IPrescriptionDispenseService
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public PrescriptionDispenseService(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PrescriptionDispenseResultDto>> GetAll()
        {
            var items = await _uow.GetReposatory<PrescriptionDispense, Guid>().GetAll();
            return _mapper.Map<IEnumerable<PrescriptionDispenseResultDto>>(items);
        }

        public async Task<PrescriptionDispenseResultDto> GetById(Guid id)
        {
            var entity = await _uow.GetReposatory<PrescriptionDispense, Guid>().GetById(id);
            if (entity is null) throw new PrescriptionDispenseNotFoundException(id);
            return _mapper.Map<PrescriptionDispenseResultDto>(entity);
        }

        public async Task<PrescriptionDispenseResultDto> Create(CreateOrUpdatePrescriptionDispenseDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            // Ensure the prescription exists
            var prescriptionRepo = _uow.GetReposatory<Prescription, Guid>();
            var prescription = await prescriptionRepo.GetById(dto.PrescriptionId);
            if (prescription is null)
                throw new InvalidOperationException($"Prescription with id {dto.PrescriptionId} not found.");

            // Ensure the pharmacy exists
            var pharmacyRepo = _uow.GetReposatory<Pharmacy, Guid>();
            var pharmacy = await pharmacyRepo.GetById(dto.PharmacyId);
            if (pharmacy is null)
                throw new InvalidOperationException($"Pharmacy with id {dto.PharmacyId} not found.");

            var entity = _mapper.Map<PrescriptionDispense>(dto);
            entity.DispensedAt = dto.DispensedAt ?? DateTime.UtcNow;

            await _uow.GetReposatory<PrescriptionDispense, Guid>().Create(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to create prescription dispense.");
            return _mapper.Map<PrescriptionDispenseResultDto>(entity);
        }

        public async Task Update(Guid id, CreateOrUpdatePrescriptionDispenseDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            var repo = _uow.GetReposatory<PrescriptionDispense, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new PrescriptionDispenseNotFoundException(id);

            // Ensure the prescription exists
            var prescriptionRepo = _uow.GetReposatory<Prescription, Guid>();
            var prescription = await prescriptionRepo.GetById(dto.PrescriptionId);
            if (prescription is null)
                throw new InvalidOperationException($"Prescription with id {dto.PrescriptionId} not found.");

            // Ensure the pharmacy exists
            var pharmacyRepo = _uow.GetReposatory<Pharmacy, Guid>();
            var pharmacy = await pharmacyRepo.GetById(dto.PharmacyId);
            if (pharmacy is null)
                throw new InvalidOperationException($"Pharmacy with id {dto.PharmacyId} not found.");

            _mapper.Map(dto, entity);
            repo.Update(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to update prescription dispense.");
        }

        public async Task Delete(Guid id)
        {
            var repo = _uow.GetReposatory<PrescriptionDispense, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null) throw new PrescriptionDispenseNotFoundException(id);
            repo.Delete(entity);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0) throw new InvalidOperationException("Failed to delete prescription dispense.");
        }
    }
}
