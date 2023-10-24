using ID_model.DTOs;
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

        [HttpPost]
        public async Task<ActionResult> CreateClient(ClientCreateDTO request)
        {
            await _service.CreateClient(request);
            return Ok(request);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient(Guid id, ClientUpdateDTO request)
        {
            await _service.UpdateClientBasicData(id, request);
            return Ok("Dados atualizados com sucesso");
        }
        [HttpPut("endereco/{id}")]
        public async Task<ActionResult> UpdateAddressClient(Guid idClient, AddressUpdateDTO request)
        {
            await _service.UpdateAddress(idClient, request);
            return Ok("Endereço atualizado com sucesso");
        }
    }
}
