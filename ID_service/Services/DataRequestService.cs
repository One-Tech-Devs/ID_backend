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

        public async Task<DataRequestModel> CreateDataRequest(DataRequestDTO request)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Username == request.ClientUsername);
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Username == request.CompanyUsername);

            if (client is null) return new DataRequestModel();
            if (company is null) return new DataRequestModel();

            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            DateTime nowInBrazil = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, brazilTimeZone);

            var dataRequestModel = new DataRequestModel()
            {
                Id = Guid.NewGuid(),
                CompanyId = company.Id,
                ClientId = client.Id,
                RequestCreation = nowInBrazil,
                RequestExpiration = nowInBrazil,
                Status = "Pendente",
                ClientData = string.Join(", ", request.ClientData)
            };

            _context.DataRequests.Add(dataRequestModel);
            await _context.SaveChangesAsync();

            return dataRequestModel;
        }

        public async Task<List<DataRequestModel>> GetAllDataRequest()
        {
            var requests = await _context.DataRequests.ToListAsync();

            return requests;
        }

        public async Task<DataRequestModel> GetDataRequestById(Guid id)
        {
           var request = await _context.DataRequests.FindAsync(id);

           if (request is null) return new DataRequestModel();

            return null;
        }
    }
}
