using ID_model.DTOs;
using ID_model.Models;
using ID_repository.Data;
using ID_service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_service.Services
{
    public class DataRequestService : IDataRequestService
    {


        private List<DataRequestModel> _dataRequests = new List<DataRequestModel>();

        //private readonly DataContext _context;

        //public DataRequestService(DataContext context)
        //{
        //    _context = context;
        //}

        private ClientModel cli = new ClientModel()
        {
            Id= Guid.Parse("dea6e22d-763c-4f72-b5b1-9422bae1de8b"),
            Name = "BERNARDO",
            SocialName = "BERNARDO",
            SSN = "000.000.000-00",
            NIC = "00.000.000",
            SecurityPhrase = "Senha"

        };

        private CompanyModel co = new CompanyModel()
        {
            Id = Guid.Parse("992071d1-37dd-42da-9676-59375d501ccd"),
            AccessInformation = new AccessInformation()
            {
                Username = "admin",
                Password = "admin",
                Role = "Company"
            },
            CompanyName = "Americanas",
            BusinessName = "Lojas Americanas",
            CorporateDocument = "000.000.000-0000",
            Email = "americanas@company.com"
        };

        //public required string Username { get; set; }
        //public required string CompanyName { get; set; }
        //public required string BusinessName { get; set; }
        //public required string CorporateDocument { get; set; }
        //public required string CorporateEmail { get; set; }

        //public string Name { get; set; }
        //public string? SocialName { get; set; }
        //public string SSN { get; set; } //cpf
        //public string NIC { get; set; } //rg
        //public string SecurityPhrase { get; set; }

        public async Task<DataRequestModel> CreateDataRequest(DataRequestDTO request)
        {
            //var client = await _context.Clients.FindAsync(request.ClientId);
            //var company = await _context.Companies.FindAsync(request.CompanyId);
            var client = cli;
            var company = co;
            
            if (client is null) return new DataRequestModel();
            if (company is null) return new DataRequestModel();

            // Obtenha o fuso horário do Brasil
            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            // Converta a hora atual para o horário do Brasil
            DateTime nowInBrazil = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, brazilTimeZone);

            var dataRequestModel = new DataRequestModel()
            {
                Id = Guid.NewGuid(),
                CompanyId = company.Id,
                ClientId = client.Id,
                RequestCreation = nowInBrazil,
                RequestExpiration = request.RequestExpiration,
                Status = "Pendente",
                ClientData = string.Join(",", request.ClientData)
            };

           // _context.DataRequests.Add(dataRequestModel);
            //await _context.SaveChangesAsync();
            //_dataRequests.Add(dataRequestModel);

            return dataRequestModel;
        }

        public async Task<List<DataRequestModel>> GetAllDataRequest()
        {
            //var requests = await _context.DataRequests.ToListAsync();

            return _dataRequests;
        }

        public async Task<DataRequestModel> GetDataRequestById(Guid id)
        {
            //var request = await _context.DataRequests.FindAsync(id);

           //if (request is null) return new DataRequestModel();

            return null;
        }
    }
}
