using ID_model.DTOs;
using ID_model.Models;
using ID_service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ID_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("CreateCompany")]
        public async Task<ActionResult<CompanyModel>> CreateCompany(CreateCompanyDTO request)
        {
            var company = await _companyService.CreateCompany(request);

            if (!company.StatusRF) { return BadRequest("Company with 'inactive' status"); }

            return company is not null ? Ok(company) : BadRequest("Unable to register. There is already a company with this data!");
        }


    }
}
