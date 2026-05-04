using AutoMapper;
using BLL.Dtos.MedicalExamination;
using BLL.Dtos.MedicalExaminationRequest;
using BLL.ServiceAbstraction;
using DAL.Exceptions;
using DAL.Models;
using DAL.Models.Enums;
using DAL.repositories.RepoAbstraction;
<<<<<<< HEAD

namespace BLL.ServiceImplementation
{
    public class MedicalExaminationRequestService(IAttachmentService _attach, IUOW _uow, IMapper _mapper) : IMedicalExaminationRequestService
=======
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceImplementation
{
    public class MedicalExaminationRequestService( IAttachmentService _attach,IUOW _uow , IMapper _mapper , UserManager<AppUser> usermanager) : IMedicalExaminationRequestService
>>>>>>> origin/main
    {

        public async Task<IEnumerable<MedicalExaminationRequestResultDto>> GetAll()
        {
            var requests = await _uow.GetReposatory<MedicalExaminationRequest, Guid>().GetAll();
            var result = _mapper.Map<IEnumerable<MedicalExaminationRequestResultDto>>(requests);
            return result;
        }

        public async Task<MedicalExaminationRequestResultDto> GetById(Guid id)
        {
            var request = await _uow.GetReposatory<MedicalExaminationRequest, Guid>().GetById(id) ?? throw new MedicalExaminationRequestNotFoundException(id);
            var result = _mapper.Map<MedicalExaminationRequestResultDto>(request);
            return result;
        }

        public async Task<IEnumerable<MedicalExaminationRequestResultDto>> GetMedicalExaminationRequestByUserId(Guid userId)
        {
            var user = await usermanager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var medicalExmaination = await _uow.GetReposatory<MedicalExaminationRequest, Guid>().GetAll(me => me.UserId == userId);

            if (medicalExmaination is null)
            {
                throw new NotFoundException("there is no medical examination requests for this user");
            }

            return _mapper.Map<IEnumerable<MedicalExaminationRequestResultDto>>(medicalExmaination);
        }


        public async Task<MedicalExaminationRequestResultDto> Create(Guid UserId, CreateOrUpdateMedicalExaminationRequestDto createOrUpdateMedicalExaminationRequestDto)
        {
            var request = _mapper.Map<MedicalExaminationRequest>(createOrUpdateMedicalExaminationRequestDto);
            if (createOrUpdateMedicalExaminationRequestDto.RequestType == (MedicalExaminationRequestType)1)
            {
                request.Status = MedicalExaminationRequestStatus.AutoApproved;
                request.MedicalExamination = new MedicalExamination()
                {
                    UserId = UserId,
                    MedicalExaminationStatus = MedicalExaminationStatus.Approved,
                    MedicalExaminationRequestId = request.Id

                };
            }
            else
            {
                request.Status = MedicalExaminationRequestStatus.Pending;
            }
            request.UserId = UserId;
            if (createOrUpdateMedicalExaminationRequestDto.MedicalReportPath is not null)
            {
                request.MedicalReportPath = _attach.Upload(createOrUpdateMedicalExaminationRequestDto.MedicalReportPath, "Images");
            }
            //else
            //{
            //    throw new Exception("image not found ");
            //}




            await _uow.GetReposatory<MedicalExaminationRequest, Guid>().Create(request);
            var res = await _uow.SaveChnagesAsync();
            if (res <= 0)
            {
                throw new Exception("Can't create new MedicalExaminationRequest");
            }

            return _mapper.Map<MedicalExaminationRequestResultDto>(request);
        }

        public async Task<MedicalExaminationRequestResultDto> Update(Guid UserId, Guid id, CreateOrUpdateMedicalExaminationRequestDto createOrUpdateMedicalExaminationRequestDto)
        {
            var request = await _uow.GetReposatory<MedicalExaminationRequest, Guid>().GetById(id) ?? throw new MedicalExaminationRequestNotFoundException(id);
            if (request.MedicalReportPath is not null && createOrUpdateMedicalExaminationRequestDto.MedicalReportPath is not null)
            {
                _attach.Delete(request.MedicalReportPath);
            }

            if (request.Status == MedicalExaminationRequestStatus.Approved || request.Status == MedicalExaminationRequestStatus.AutoApproved)
            {
                throw new Exception("this request already completed");
            }
            var oldImg = request.MedicalReportPath;
            var res = _mapper.Map(createOrUpdateMedicalExaminationRequestDto, request);

            if (createOrUpdateMedicalExaminationRequestDto.MedicalReportPath is null)
            {
                request.MedicalReportPath = oldImg;
            }

            if (createOrUpdateMedicalExaminationRequestDto.MedicalReportPath is not null)
            {
                request.MedicalReportPath = _attach.Upload(createOrUpdateMedicalExaminationRequestDto.MedicalReportPath, "Images");
            }

            if (res.Status == MedicalExaminationRequestStatus.Approved && res.MedicalExamination is null)
            {
                var MedicalExamination = new MedicalExamination()
                {
                    MedicalExaminationRequestId = res.Id,
                    MedicalExaminationStatus = MedicalExaminationStatus.Approved,
                    UserId = res.UserId,
                };
                await _uow.GetReposatory<MedicalExamination, Guid>().Create(MedicalExamination);

            }
            _uow.GetReposatory<MedicalExaminationRequest, Guid>().Update(res);
            var result = await _uow.SaveChnagesAsync();
            if (result <= 0)
            {
                throw new Exception("Can't update new MedicalExaminationRequest");
            }

            return _mapper.Map<MedicalExaminationRequestResultDto>(res);
        }

        public async Task<bool> Delete(Guid id)
        {
            var request = await _uow.GetReposatory<MedicalExaminationRequest, Guid>().GetById(id) ?? throw new MedicalExaminationRequestNotFoundException(id);
            _uow.GetReposatory<MedicalExaminationRequest, Guid>().Delete(request);

            var res = await _uow.SaveChnagesAsync();
            if (res < 0)
            {
                throw new Exception("Can't delete new MedicalExaminationRequest");
            }
            return true;


        }



        public async Task<IEnumerable<MedicalExaminationRequestResultDto>> GetByUserId(Guid userId)
        {
            var requests = await _uow.GetReposatory<MedicalExaminationRequest, Guid>()
                .GetAll(r => r.UserId == userId);
            return _mapper.Map<IEnumerable<MedicalExaminationRequestResultDto>>(requests);
        }

    }
}
