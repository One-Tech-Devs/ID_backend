using Azure.Core;
using ID_model.DTOs;
using ID_model.Models;
using ID_repository.Data;
using ID_service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ID_service.Services
{
    public class DataRequestService : IDataRequestService
    {

        private readonly DataContext _context;

        public DataRequestService(DataContext context)
        {
            _context = context;
        }

        public async Task<DataRequestModel?> CreateDataRequest(DataRequestDTO request)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Username == request.ClientUsername);
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Username == request.CompanyUsername);

            if (client is null) return null;
            if (company is null) return null;

            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            DateTime nowInBrazil = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, brazilTimeZone);

            var dataRequestModel = new DataRequestModel()
            {
                Id = Guid.NewGuid(),
                CompanyId = company.Id,
                Company = company,
                ClientId = client.Id,
                Client = client,
                RequestCreation = nowInBrazil,
                RequestExpiration = request.RequestExpiration,
                Status = "Pending",
                ClientData = string.Join(", ", request.ClientData)
            };

            await _context.DataRequests.AddAsync(dataRequestModel);
            await _context.SaveChangesAsync();

            return dataRequestModel;
        }

        public async Task<List<DataRequestModel>> GetAllDataRequest()
        {
            var requests = await _context.DataRequests.ToListAsync();

            return requests;
        }

        public async Task<DataRequestModel?> GetDataRequestById(Guid id)
        {
           var request = await _context.DataRequests.FindAsync(id);

           if (request is null) return null;

           return request;
        }
        public async Task<DataRequestModel?> ChangeStatusDataRequestById(Guid id, string status)
        {
            var request = await _context.DataRequests.FindAsync(id);

            if (request == null){return null;}

            request.Status = status;

            if (request.RequestExpiration < DateTime.Now) request.Status = "Expired";

            _context.DataRequests.Update(request);
            await _context.SaveChangesAsync();

            return request;
        }

        public async Task<List<GetDataRequestDTO>?> GetDataRequestByClient(Guid clientId)
        {
            var requests = await _context.DataRequests
                .Where(r => r.ClientId == clientId)
                .Select(r => new GetDataRequestDTO
                {
                    Id = r.Id,
                    BusinessName = r.Company.BusinessName,
                    ClientUsername = r.Client.Username,
                    RequestCreation = r.RequestCreation,
                    RequestExpiration = r.RequestExpiration,
                    Status = r.Status,
                    ClientData = r.ClientData
                })
                .ToListAsync();

            if (requests is null) return null;

            return requests;
        }

        public async Task<List<DataRequestModel>> GetDataRequestByStatus(string status)
        {
            var dataRequest = await _context.DataRequests.Where(d => d.Status == status).ToListAsync();
            if(dataRequest == null) { return null; }
            return dataRequest;
        }
    }
}
