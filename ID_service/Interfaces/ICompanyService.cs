using ID_model.DTOs;
using ID_model.Models;

namespace ID_service.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyModel> CreateCompany(CreateCompanyDTO createCompanyRequest);
    }
}
