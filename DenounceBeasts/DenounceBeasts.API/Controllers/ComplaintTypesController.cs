using DenounceBeasts.API.Models.DTOs;
using DenounceBeasts.API.Models.Responses;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintTypesController : ControllerBase
    {
        readonly ApplicationDbContext _context;

        public ComplaintTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ApiResponse<List<ComplaintTypeDto>> GetAll()
        {
            var complaintTypes = _context.ComplaintTypes.ToList();

            var complaintTypesResponse = new List<ComplaintTypeDto>();
            foreach (var complaintType in complaintTypes)
            {
                complaintTypesResponse.Add(new ComplaintTypeDto
                {
                    Id = complaintType.Id,
                    Name = complaintType.Name,
                    Description = complaintType.Description
                });
            }

            complaintTypesResponse = complaintTypes.Select(s => new ComplaintTypeDto
            {
                Id = s.Id,
                Description = s.Description,
                Name = s.Name,
            }).ToList();

            return ApiResponse<List<ComplaintTypeDto>>.Success(complaintTypesResponse, 200);
        }


        [HttpGet("{id}")]
        public ApiResponse<ComplaintTypeDto> GetById(int id)
        {
            var complaintType = _context.ComplaintTypes.FirstOrDefault(s => s.Id == id);
            if (complaintType == null)
            {
                return ApiResponse<ComplaintTypeDto>.Fail(404, "Record no found");
            }

            var complaintTypeDto = new ComplaintTypeDto
            {
                Id = complaintType.Id,
                Description = complaintType.Description,
                Name = complaintType.Name
            };

            return ApiResponse<ComplaintTypeDto>.Success(complaintTypeDto);
        }

        [HttpPost]
        public ApiResponse<ComplaintTypeDto> Create(ComplaintTypeDto request)
        {
            var complaintType = new ComplaintType
            {
                Description = request.Description,
                Name = request.Name,
            };

            _context.ComplaintTypes.Add(complaintType);
            _context.SaveChanges();
            return ApiResponse<ComplaintTypeDto>.Success(new ComplaintTypeDto { Id = complaintType.Id });

        }

        [HttpPut("{id}")]
        public ApiResponse<ComplaintTypeDto> Update(int id, ComplaintTypeDto updatedComplaintType)
        {
            var complaintType = _context.ComplaintTypes.FirstOrDefault(s => s.Id == id);
            if (complaintType == null)
            {
                return ApiResponse<ComplaintTypeDto>.Fail(404, "Record no found");

            }
            complaintType.Name = updatedComplaintType.Name;
            complaintType.Description = updatedComplaintType.Description;
            _context.ComplaintTypes.Update(complaintType);
            _context.SaveChanges();
            return ApiResponse<ComplaintTypeDto>.Success(null);

        }

        [HttpDelete("{id}")]
        public ApiResponse<ComplaintTypeDto> Delete(int id)
        {
            var complaintType = _context.ComplaintTypes.FirstOrDefault(s => s.Id == id);
            if (complaintType == null)
            {
                return ApiResponse<ComplaintTypeDto>.Fail(404, "Record no found");

            }
            _context.ComplaintTypes.Remove(complaintType);
            return ApiResponse<ComplaintTypeDto>.Success(null);
        }

    }
}
