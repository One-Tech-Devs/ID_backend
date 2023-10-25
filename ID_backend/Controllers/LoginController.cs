using ID_model.DTOs;
using ID_service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ID_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost("clients")]
        public async Task<IActionResult> LoginClient(LoginDTO request)
        {
            var token = await _service.LoginClient(request);
            if (token == null) { return Unauthorized(); }
            return Ok(new { Token = token });
        }
        [HttpPost("companies")]
        public async Task<IActionResult> LoginCompany(LoginDTO request)
        {
            var token = await _service.LoginCompany(request);
            if (token == null) { return Unauthorized(); }
            return Ok(new { Token = token });
        }
    }
}
