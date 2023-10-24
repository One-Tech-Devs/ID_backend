using ID_model.DTOs;
using ID_model.Models;

namespace ID_service.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyModel> CreateCompany(CreateCompanyDTO createCompanyRequest);
        Task<CompanyModel?> GetCompanyByCorporateDocument(string corporateDocument);
        Task<CompanyModel?> GetCompanyByEmail(string corporateEmail);
        Task<CompanyModel?> GetCompanyByUsername(string username);
    }
}
