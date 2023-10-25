﻿using ID_model.DTOs;
using ID_model.Models;
using ID_repository.Data;
using ID_service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ID_service.Services
{
    public class ClientService : IClientService
    {
        private readonly DataContext _context;
        private readonly Generator _generator;

        public ClientService(DataContext context, Generator generator)
        {
            _context = context;
            _generator = generator;
        }

        public async Task<List<ClientModel>> GetAllClients()
        {
            var clients = await _context.Clients.Include(c => c.Address).ToListAsync();

            return clients;
        }

        public async Task<ClientModel> GetClientById(Guid clientId)
        {
            var client = await _context.Clients.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == clientId);

            return client;
        }

        public async Task<ClientModel?> GetClientByUsername(string username)
        {
            var client = await _context.Clients.Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Username == username);

            return client is not null ? client : null;
        }

        public async Task<ClientModel?> GetClientBySSN(string ssn)
        {
            var client = await _context.Clients.Include(c => c.Address).FirstOrDefaultAsync(c => c.SSN == ssn);


            return client is not null ? client : null;
        }

        public async Task<ClientModel?> GetClientByEmail(string email)
        {
            var client = await _context.Clients.Include(c => c.Address).FirstOrDefaultAsync(c => c.Email == email);

            return client is not null ? client : null;
        }
        public async Task<ClientModel?> CreateClient(ClientCreateDTO request)
        {
            if(await GetClientByUsername(request.UserName) != null) return null;
            if (await GetClientBySSN(request.SSN) != null) return null;
            if (await GetClientByEmail(request.Email) != null) return null;

            byte[] key = _generator.GenerateKey();
            byte[] iv = _generator.GenerateIV();

            var client = new ClientModel
            {
                Id = Guid.NewGuid(),
                Username = request.UserName,
                Password = Encryption.Encrypt(request.Password, key, iv),
                Role = "Client",
                Name = request.Name,
                Gender = request.Gender,
                SecurityPhrase = Encryption.Encrypt(request.SecurityPhrase, key, iv),
                Email = request.Email,
                SSN = request.SSN,
                NIC = request.NIC,
                Key = key,
                Iv = iv
            };

            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task DeleteClient(Guid id)
        {
            var client = await GetClientById(id);
            _context.Addresses.Remove(client.Address);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<ClientModel?> UpdateAddress(Guid idClient, AddressUpdateDTO request)
        {
            var client = await GetClientById(idClient);

            if (client is null) return null;

            if (client.Address is null || client.AddressId is null)
            {
                client.AddressId = Guid.NewGuid();

                var address = new AddressModel
                {
                    Id = client.AddressId.Value,
                    Country = request.Country,
                    StateOrProvince = request.StateOrProvince,
                    CityOrVillage = request.CityOrVillage,
                    PostalCode = request.PostalCode,
                    Neighborhood = request.Neighborhood,
                    Street = request.Street,
                    Number = request.Number
                };

                client.Address = address;

                await _context.Addresses.AddAsync(address);
                _context.Clients.Update(client);
            }
            else
            {
                var address = await _context.Addresses.FindAsync(client.AddressId);

                address.Country = request.Country;
                address.StateOrProvince = request.StateOrProvince;
                address.CityOrVillage = request.CityOrVillage;
                address.PostalCode = request.PostalCode;
                address.Neighborhood = request.Neighborhood;
                address.Street = request.Street;
                address.Number = request.Number;

                client.Address = address;

                _context.Addresses.Update(address);
                _context.Clients.Update(client);

            }

            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<ClientModel?> UpdateClientBasicData(Guid id, ClientUpdateDTO request)
        {
            var client = await GetClientById(id);

            if (client is null) return null;

            client.Name = request.Name;
            client.SocialName = request.SocialName;
            client.Email = request.Email;
            client.PhoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync();

            return client;
        }
        
        public async Task<DataRequestModel?> UpdateStatusRequestByUsername(string username, Guid requestId, string status)
        {
            var client = await GetClientByUsername(username);

            var request = await _context.DataRequests.FirstOrDefaultAsync(r => r.ClientId == client.Id && r.Id == requestId);

            if (request == null){return null;}

            request.Status = status;
            
            if (request.RequestExpiration < DateTime.Now) request.Status = "Expired";

            _context.DataRequests.Update(request);
            await _context.SaveChangesAsync();

            return request;
        }

        public async Task<List<BasicDataRequestInfosDTO?>?> GetDataRequestsByClientAndStatus(Guid clientId, string status)
        {
            var requests = await _context.DataRequests.Where(r => r.ClientId == clientId && r.Status == status).ToListAsync();

            if (requests is null) return null;

            var responseList = new List<BasicDataRequestInfosDTO>();

            foreach (var request in requests)
            {
                if (request is not null)
                {
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

                    responseList.Add(DTO);
                }
            }

            return responseList;
        }
    }
}
