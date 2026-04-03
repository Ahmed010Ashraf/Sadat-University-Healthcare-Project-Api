using AutoMapper;
using BLL.Dtos.PrescriptionRequest;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using DAL.Models.Enums;
using DAL.repositories.RepoAbstraction;

namespace BLL.ServiceImplementation
{
    public class PrescriptionRequestService : IPrescriptionRequestService
    {
        private readonly IAttachmentService _attach;
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public PrescriptionRequestService(IAttachmentService attach, IUOW uow, IMapper mapper)
        {
            _attach = attach;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PrescriptionRequestResultDto>> GetAll()
        {
            var requests = await _uow.GetReposatory<PrescriptionRequest, Guid>().GetAll();
            return _mapper.Map<IEnumerable<PrescriptionRequestResultDto>>(requests);
        }

        public async Task<PrescriptionRequestResultDto> GetById(Guid id)
        {
            var request = await _uow.GetReposatory<PrescriptionRequest, Guid>().GetById(id)
                ?? throw new PrescriptionRequestNotFoundException(id);
            return _mapper.Map<PrescriptionRequestResultDto>(request);
        }

        public async Task<PrescriptionRequestResultDto> Create(Guid userId, CreateOrUpdatePrescriptionRequestDto dto)
        {
            var request = _mapper.Map<PrescriptionRequest>(dto);
            request.UserId = userId;
            request.RequestDate = DateTime.UtcNow;

            // Default status
            request.Status = dto.Status ?? PrescriptionRequestStatus.Pending;

            if (dto.PrescriptionImage is null)
                throw new Exception("Prescription image is required.");

            request.PrescriptionImagePath = _attach.Upload(dto.PrescriptionImage, "Images");

            await _uow.GetReposatory<PrescriptionRequest, Guid>().Create(request);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0)
                throw new Exception("Failed to create prescription request.");

            return _mapper.Map<PrescriptionRequestResultDto>(request);
        }

        public async Task<PrescriptionRequestResultDto> Update(Guid userId, Guid id, CreateOrUpdatePrescriptionRequestDto dto)
        {
            var request = await _uow.GetReposatory<PrescriptionRequest, Guid>().GetById(id)
                ?? throw new PrescriptionRequestNotFoundException(id);

            // Prevent update if already approved
            if (request.Status == PrescriptionRequestStatus.Approved)
                throw new Exception("This request has already been approved and cannot be updated.");

            // Handle file update
            var oldImagePath = request.PrescriptionImagePath;
            if (dto.PrescriptionImage is not null)
            {
                // Delete old image if exists
                if (!string.IsNullOrEmpty(oldImagePath))
                    _attach.Delete(oldImagePath);

                request.PrescriptionImagePath = _attach.Upload(dto.PrescriptionImage, "Images");
            }

            // Update other fields
            request.CommitteeNotes = dto.CommitteeNotes ?? request.CommitteeNotes;
            request.Status = dto.Status ?? request.Status;

            // If status changed to Approved and no prescription exists yet, create one
            if (request.Status == PrescriptionRequestStatus.Approved && request.Prescription == null)
            {
                var prescription = new Prescription
                {
                    PrescriptionRequestId = request.Id,
                    UserId = request.UserId,
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30)),
                    Status = PrescriptionStatus.Active
                };
                await _uow.GetReposatory<Prescription, Guid>().Create(prescription);
            }

            _uow.GetReposatory<PrescriptionRequest, Guid>().Update(request);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0)
                throw new Exception("Failed to update prescription request.");

            return _mapper.Map<PrescriptionRequestResultDto>(request);
        }

        public async Task<bool> Delete(Guid id)
        {
            var request = await _uow.GetReposatory<PrescriptionRequest, Guid>().GetById(id)
                ?? throw new PrescriptionRequestNotFoundException(id);

            // Delete associated image
            if (!string.IsNullOrEmpty(request.PrescriptionImagePath))
                _attach.Delete(request.PrescriptionImagePath);

            _uow.GetReposatory<PrescriptionRequest, Guid>().Delete(request);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0)
                throw new Exception("Failed to delete prescription request.");

            return true;
        }
    }
}
