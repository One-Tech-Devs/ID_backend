using Azure.Core;
using ID_model.DTOs;
using ID_model.Models;
using ID_repository.Data;
using ID_service.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<BasicDataRequestInfosDTO>> GetAllDataRequest()
        {
            var requests = await _context.DataRequests.ToListAsync();

            var responseList = new List<BasicDataRequestInfosDTO>();

            foreach (var request in requests)
            {
                var response = TrasnformToDTO(request);

                responseList.Add(response);
            }            

            return responseList;
        }

        public async Task<BasicDataRequestInfosDTO?> GetDataRequestById(Guid id)
        {
            var request = await _context.DataRequests.FindAsync(id);

            if (request is null) return null;

            var response = TrasnformToDTO(request);

            return response;
        }
        public async Task<BasicDataRequestInfosDTO?> ChangeStatusDataRequestById(Guid id, string status)
        {
            var request = await _context.DataRequests.FindAsync(id);

            if (request == null){return null;}

            request.Status = status;

            if (request.RequestExpiration < DateTime.Now) request.Status = "Expired";

            _context.DataRequests.Update(request);
            await _context.SaveChangesAsync();

            var response = TrasnformToDTO(request);             

            return response;
        }

        private static BasicDataRequestInfosDTO? TrasnformToDTO(DataRequestModel? request)
        {
            if (request is null) return null;

            var DTO = new BasicDataRequestInfosDTO()
            {
                Id = request.Id,
                CompanyName = request.Company?.CompanyName,
                ClientId = request.ClientId,
                ClientData = request.ClientData,
                RequestCreationDate = request.RequestCreation,
                RequestExpirationDate = request.RequestExpiration,
                Status = request.Status,
            };

            return DTO;
        }

        public async Task<List<BasicDataRequestInfosDTO?>?> GetDataRequestByClient(Guid clientId)
        {
            var requests = await _context.DataRequests
                .Where(r => r.ClientId == clientId)
                .Select(r => new BasicDataRequestInfosDTO
                {
                    Id = r.Id,
                    CompanyName = r.Company.CompanyName,
                    ClientId = r.ClientId,
                    RequestCreationDate = r.RequestCreation,
                    RequestExpirationDate = r.RequestExpiration,
                    Status = r.Status,
                    ClientData = r.ClientData
                })
                .ToListAsync();

            if (requests is null) return null;

            return requests;
        }

        public async Task<List<BasicDataRequestInfosDTO>> GetDataRequestByStatus(string status)
        {
            var dataRequest = await _context.DataRequests.Where(d => d.Status == status).ToListAsync();
            if(dataRequest is null) { return null; }

            var responseList = new List<BasicDataRequestInfosDTO>();

            foreach (var request in dataRequest)
            {
                var response = TrasnformToDTO(request);

                responseList.Add(response);
            }

            return responseList;
        }

        public async Task<List<BasicDataRequestInfosDTO?>?> GetDataRequestByClientStatus(Guid clientId, string status)
        {
            var request = await GetDataRequestByClient(clientId);

            if (request is null) return null;

            var responseList = request.Where(r => r.Status == status).ToList();

            if(responseList is null) return null;

            return responseList;
        }
    }
}
