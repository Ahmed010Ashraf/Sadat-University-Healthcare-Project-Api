using AutoMapper;
using BLL.Dtos.MedicalExamination;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using DAL.repositories.RepoAbstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.ServiceImplementation
{
    public class MedicalExaminationService : IMedicalExaminationService
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public MedicalExaminationService(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicalExaminationResultDto>> GetAll()
        {
            var items = await _uow.GetReposatory<MedicalExamination, Guid>().GetAll();
            return _mapper.Map<IEnumerable<MedicalExaminationResultDto>>(items);
        }

        public async Task<MedicalExaminationResultDto> GetById(Guid id)
        {
            var entity = await _uow.GetReposatory<MedicalExamination, Guid>().GetById(id);
            if (entity is null)
                throw new MedicalExaminationNotFounsException(id);

            return _mapper.Map<MedicalExaminationResultDto>(entity);
        }

        public async Task<MedicalExaminationResultDto> Create(Guid userId, CreateOrUpdateMedicalExaminationDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var entity = _mapper.Map<MedicalExamination>(dto);
            entity.UserId = userId;

            await _uow.GetReposatory<MedicalExamination, Guid>().Create(entity);
            var affected = await _uow.SaveChnagesAsync();
            if (affected <= 0)
                throw new InvalidOperationException("Failed to persist new MedicalExamination.");

            return _mapper.Map<MedicalExaminationResultDto>(entity);
        }

        public async Task Update(Guid id, CreateOrUpdateMedicalExaminationDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var repo = _uow.GetReposatory<MedicalExamination, Guid>();
            var entity = await repo.GetById(id);
            
            if (entity is null)
                throw new MedicalExaminationNotFounsException(id);

            if (entity.MedicalExaminationStatus == DAL.Models.Enums.MedicalExaminationStatus.completed)
            {
                throw new Exception("Already Completed");
            }

            _mapper.Map(dto, entity);

            entity.MedicalExaminationStatus = DAL.Models.Enums.MedicalExaminationStatus.completed;

            repo.Update(entity);
            var affected = await _uow.SaveChnagesAsync();
            if (affected <= 0)
                throw new InvalidOperationException("Failed to persist MedicalExamination update.");
        }

        public async Task Delete(Guid id)
        {
            var repo = _uow.GetReposatory<MedicalExamination, Guid>();
            var entity = await repo.GetById(id);
            if (entity is null)
                throw new MedicalExaminationNotFounsException(id);
            repo.Delete(entity);
            var affected = await _uow.SaveChnagesAsync();
            if (affected <= 0)
                throw new InvalidOperationException("Failed to delete MedicalExamination.");
        }

        public async Task<MedicalExaminationResultDto> GetMedicalExaminationByRequestId(Guid requestId)
        {
            var request = await _uow.GetReposatory<MedicalExaminationRequest, Guid>().GetById(requestId)??throw new MedicalExaminationRequestNotFoundException(requestId);
            var medicalExmaination = await _uow.GetReposatory<MedicalExamination,Guid>().GetAll(me=>me.MedicalExaminationRequestId == requestId);
            if(medicalExmaination is null)
            {
                throw new NotFoundException("there is no medical examination for this request");
            }

            return _mapper.Map<MedicalExaminationResultDto>(medicalExmaination.FirstOrDefault());

        }
    }
}
