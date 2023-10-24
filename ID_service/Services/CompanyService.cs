using ID_model.DTOs;
using ID_service.Interfaces;
using ID_model.Models;

namespace ID_service.Services
{
    public class CompanyService : ICompanyService
    {
        private static readonly List<CompanyModel> companyModels = new();
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

                companyModels.Add(company);

                return company;
            }

            return new CompanyModel();            
        }

        private Task<bool> ValidationRegistrationInformation(CreateCompanyDTO createCompanyRequest)
        {
            //TO DO
            //realizar validações de email unico e cnpj unico quando os serviço de get estiverem prontos
            return Task.FromResult(createCompanyRequest is not null);
        }

        private static Task<bool> ValidateCompanyRegistration(string corporateDocument)
        {
            //TO DO
            //Realizar validação de status da receita federal 
            return Task.FromResult(corporateDocument is not null);
        }
    }
}
