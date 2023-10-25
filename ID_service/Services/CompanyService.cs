using ID_model.DTOs;
using ID_service.Interfaces;
using ID_model.Models;
using ID_repository.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Azure.Core;

namespace ID_service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly DataContext _context;
        private readonly Generator _generator;

        public CompanyService(DataContext context, Generator generator)
        {
            _context = context;
            _generator = generator;
        }

        public async Task<CompanyModel> CreateCompany(CreateCompanyDTO createCompanyRequest)
        {
            bool validStatus = await ValidateCompanyRegistration(createCompanyRequest.CorporateDocument);

            bool validInfos = await ValidationRegistrationInformation(createCompanyRequest);

            byte[] key = _generator.GenerateKey();
            byte[] iv = _generator.GenerateIV();

            if (validStatus && validInfos)
            {
                var company = new CompanyModel() 
                {
                    Id = Guid.NewGuid(),
                    Username = createCompanyRequest.Username,
                    Password = Encryption.Encrypt(createCompanyRequest.Password, key, iv),
                    Role = "Company",
                    CompanyName = createCompanyRequest.CompanyName,
                    BusinessName = createCompanyRequest.BusinessName,
                    StatusRF = true,
                    CorporateDocument = createCompanyRequest.CorporateDocument,
                    Email = createCompanyRequest.CorporateEmail,
                    Key = key,
                    Iv = iv
                };

                _context.Companies.Add(company);
                await _context.SaveChangesAsync();

                return company;
            }

            return new CompanyModel();
        }

        public async Task<CompanyModel?> GetCompanyByUsername(string username)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Username == username);

            return company is not null ? company : null;
        }

        public async Task<CompanyModel?> GetCompanyByEmail(string corporateEmail)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Email == corporateEmail);

            return company is not null ? company : null;
        }

        public async Task<CompanyModel?> GetCompanyByCorporateDocument(string corporateDocument)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.CorporateDocument == corporateDocument);

            return company is not null ? company : null;
            
        }
        public async Task<List<CompanyModel>> GetAllCompanies()
        {
            var companies = await _context.Companies.ToListAsync();

            return companies;
            
        }
        public async Task<List<DataRequestModel>> GetRequestsByStatus(Guid id, string statusRequest)
        {
            var requests = await _context.DataRequests.Where(r => r.CompanyId == id && r.Status == statusRequest).ToListAsync();

            return requests;
            
        }

        private async Task<bool> ValidationRegistrationInformation(CreateCompanyDTO createCompanyRequest)
        {
            var corporateDocument = await GetCompanyByCorporateDocument(createCompanyRequest.CorporateDocument);

            if (corporateDocument is null) 
            {
                var email = await GetCompanyByEmail(createCompanyRequest.CorporateEmail);

                if (email is null)
                {
                    var username = await GetCompanyByUsername(createCompanyRequest.Username);

                    return username is null;
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
