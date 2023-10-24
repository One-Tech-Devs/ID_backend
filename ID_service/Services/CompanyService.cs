using ID_model.DTOs;
using ID_service.Interfaces;
using ID_model.Models;
using ID_repository.Data;
using System.IO;

namespace ID_service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly DataContext _context;

        public async Task<CompanyModel> CreateCompany(CreateCompanyDTO createCompanyRequest)
        {
            bool validStatus = await ValidateCompanyRegistration(createCompanyRequest.CorporateDocument);

            bool validInfos = await ValidationRegistrationInformation(createCompanyRequest);

            if (validStatus && validInfos)
            {
                var company = new CompanyModel() 
                {
                    Id = Guid.NewGuid(),
                    CompanyName = createCompanyRequest.CompanyName,
                    BusinessName = createCompanyRequest.BusinessName,
                    StatusRF = true,
                    CorporateDocument = createCompanyRequest.CorporateDocument,
                    Email = createCompanyRequest.CorporateEmail
                };

                _context.Companies.Add(company);
                await _context.SaveChangesAsync();

                return company;
            }

            return new CompanyModel();
        }

        public async Task<CompanyModel?> GetCompanyByUsername(string username)
        {
            var company = await _context.Companies.FindAsync(username);

            return company is not null ? company : null;
        }

        public async Task<CompanyModel?> GetCompanyByEmail(string corporateEmail)
        {
            var company = await _context.Companies.FindAsync(corporateEmail);

            return company is not null ? company : null;
        }

        public async Task<CompanyModel?> GetCompanyByCorporateDocument(string corporateDocument)
        {
            var company = await _context.Companies.FindAsync(corporateDocument);

            return company is not null ? company : null;
        }


        private async Task<bool> ValidationRegistrationInformation(CreateCompanyDTO createCompanyRequest)
        {
            var company = await GetCompanyByUsername(createCompanyRequest.Username);

            if (company is null) 
            {
                var email = await GetCompanyByEmail(createCompanyRequest.CorporateEmail);

                if (email is not null)
                {
                    var corporateDocument = await GetCompanyByCorporateDocument(createCompanyRequest.CorporateDocument);

                    return  corporateDocument is null;
                }

                return false;
            }

            return false;
        }

        private static Task<bool> ValidateCompanyRegistration(string corporateDocument)
        {
            //TO DO
            //Realizar validação de status da receita federal 
            return Task.FromResult(corporateDocument is not null);
        }
    }
}
