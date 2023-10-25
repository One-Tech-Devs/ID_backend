using ID_model.DTOs;
using ID_model.Models;
using ID_service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ID_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientModel>>> GetAllClients()
        {
            var clients = await _service.GetAllClients();

            return Ok(clients);
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<ClientModel>> GetClientById(Guid clientId)
        {
            var client = await _service.GetClientById(clientId);

            return client is not null ? Ok(client) : BadRequest("Client not found!");
        }
        [HttpGet("ClientByUsername")]
        public async Task<ActionResult<ClientModel>> GetClientByUsername(string username)
        {
            var client = await _service.GetClientByUsername(username);

            return client is not null ? Ok(client) : BadRequest("Client not found!");
        }

        [HttpGet("ClientByEmail")]
        public async Task<ActionResult<ClientModel>> GetClientByEmail(string email)
        {
            var client = await _service.GetClientByEmail(email);

            return client is not null ? Ok(client) : BadRequest("Client not found!");
        }

        [HttpGet("ClientBySSN")]
        public async Task<ActionResult<ClientModel>> GetClientBySSN(string ssn)
        {
            var client = await _service.GetClientBySSN(ssn);

            return client is not null ? Ok(client) : BadRequest("Client not found!");
        }

        [HttpPost]
        public async Task<ActionResult> CreateClient(ClientCreateDTO request)
        {
            var client = await _service.CreateClient(request);

            return client is not null ? Ok(client) : BadRequest("Unable to create client, check the information provided!");
        }
        [HttpPut("ClientUpdate/{idClient}")]
        public async Task<ActionResult> UpdateClient(Guid idClient, ClientUpdateDTO request)
        {
            var client = await _service.UpdateClientBasicData(idClient, request);

            return client is not null ? Ok(client) : BadRequest("Unable to update client infos, client not found!");
        }
        [HttpPut("AddressUpdate/{idClient}")]
        public async Task<ActionResult<ClientModel?>> UpdateAddressClient(AddressUpdateDTO request, Guid idClient)
        {
            var clientUpdateAddress = await _service.UpdateAddress(idClient, request);

            return clientUpdateAddress is not null ? Ok(clientUpdateAddress) : BadRequest("Unable to update address client, client not found!");
        }

        [HttpPut("UpdateStatusRequestByUsername/{username}")]
        public async Task<ActionResult<DataRequestModel?>> UpdateStatusRequestByUsername(string username, Guid requestId, string status)
        {
           var request = await _service.UpdateStatusRequestByUsername(username, requestId, status);
           return request is not null? Ok(request) : BadRequest("Unable to update status");
        }

        [HttpGet("{clientId}/requests/{status}")]
        public async Task<ActionResult<List<BasicDataRequestInfosDTO>>> GetDataRequestByClientAndStatus(Guid clientId, string status)
        {
            var requests = await _service.GetDataRequestsByClientAndStatus(clientId, status);
            return requests is not null ? Ok(requests) : BadRequest("Data Requests not found");
        }
    }
}
