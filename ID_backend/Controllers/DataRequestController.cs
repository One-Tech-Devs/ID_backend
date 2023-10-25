using Azure.Core;
using ID_model.DTOs;
using ID_model.Models;
using ID_service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ID_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataRequestController : ControllerBase
    {
        private readonly IDataRequestService _service;

        public DataRequestController(IDataRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<BasicDataRequestInfosDTO>>> GetAllDataRequest()
        {
            var requests = await _service.GetAllDataRequest();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BasicDataRequestInfosDTO>> GetDataRequestById(Guid id)
        {
            var request = await _service.GetDataRequestById(id);
            if (request is null) return BadRequest("Request not found");
            return Ok(request);
        }

        [HttpPost]
        public async Task<ActionResult<DataRequestModel>> CreateDataRequest(DataRequestDTO requestDto)
        {
            var request = await _service.CreateDataRequest(requestDto);
            if (request is null) return BadRequest("Client/Company not found");
            return Ok(request);
        }

        [HttpPut("ChangeStatusRequestById")]
        public async Task<ActionResult<BasicDataRequestInfosDTO>> ChangeStatusDataRequestById(Guid requestId, string status)
        {
            var request = await _service.ChangeStatusDataRequestById(requestId, status);

            return request is not null ? Ok(request) : BadRequest("Data Request not found"); ;
        }

        [HttpGet("Client/{clientId}")]
        public async Task<ActionResult<List<BasicDataRequestInfosDTO>>> GetDataRequestByClient(Guid clientId)
        {
            var requests = await _service.GetDataRequestById(clientId);

            return requests is not null ? Ok(requests) : BadRequest("Data Request not found");
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<List<DataRequestModel>>> GetDataRequestByStatus(string status)
        {
            var requests = await _service.GetDataRequestByStatus(status);
            return requests is not null ? Ok(requests) : BadRequest("Data Requests not found"); ;
        }
    }
}
