using AutoMapper;
using Azure.Core;
using DenounceBeasts.API.Models.Dtos;
using DenounceBeasts.API.Models.Responses;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure;
using DenounceBeasts.Infrasctructure.Data;
using DenounceBeasts.Infrasctructure.Repositories;
using DenounceBeasts.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectorsController : ControllerBase
    {
        //private readonly SectorRepository _repo;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly ApplicationDbContext context;

        public SectorsController(//SectorRepository repo,
            UnitOfWork unitOfWork, IMapper mapper)//, ApplicationDbContext context)
        {

            //_repo = repo;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            //this.context = context;
        }

        [HttpGet]
        public ApiResponse<List<SectorDto>> GetAll()
        {
            //var sectors = _repo.GetAll();
            var sectors = _unitOfWork.SectorRepository.GetAll();

            var status = _unitOfWork.StatusRepository.GetAll();

            var selectedSectors = _mapper.Map<List<SectorDto>>(sectors);
            return ApiResponse<List<SectorDto>>.SuccessResponse(selectedSectors);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ApiResponse<SectorDto>> GetById(int id)
        {
            //var sector = _repo.GetById(id);
            var sector = _unitOfWork.SectorRepository.GetById(id);

            if (sector == null) return NotFound();

            return ApiResponse<SectorDto>.SuccessResponse(_mapper.Map<SectorDto>(sector));

        }

        [HttpGet]
        [Route("getSectorsByMunicipality/{id}")]
        public ActionResult<ApiResponse<SectorDto>> GetSectorsByMunicipality(int id)
        {
            var sector = _unitOfWork.SectorRepository.GetSectorsByMunicipality(id);

            if (sector == null) return NotFound();

            return ApiResponse<SectorDto>.SuccessResponse(_mapper.Map<SectorDto>(sector));

        }

        [HttpPost]
        public ActionResult<ApiResponse<int>> Create(SectorDto request)
        {
            var sectorAtDb = _mapper.Map<Sector>(request);
            var response = _unitOfWork.SectorRepository.Create(sectorAtDb);
            _unitOfWork.Complete();
            return ApiResponse<int>.SuccessResponse(response);

        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, SectorDto updatedSector)
        {
            var sectorAtDb = _mapper.Map<Sector>(updatedSector);
            _unitOfWork.SectorRepository.Update(id,sectorAtDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            _unitOfWork.SectorRepository.Delete(id);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}
