﻿using ID_model.DTOs;
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

        private readonly DataContext _context;

        public DataRequestService(DataContext context)
        {
            _context = context;
        }

        public async Task<DataRequestModel> CreateDataRequest(DataRequestDTO request)
        {
            var client = await _context.Clients.FindAsync(request.ClientId);
            var company = await _context.Companies.FindAsync(request.CompanyId);

            
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
                RequestExpiration = request.RequestExpiration,
                Status = "Pendente",
                ClientData = string.Join(",", request.ClientData)
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
