using ID_model.DTOs;
using ID_model.Models;
using ID_repository.Data;
using ID_service.Interfaces;

namespace ID_service.Services
{
    public class ClientService : IClientService
    {
        private readonly DataContext _context;

        public ClientService(DataContext context)
        {
            _context = context;
        }
        public async Task<ClientModel?> GetClientByUsername(string username)
        {
            var client = await _context.Clients.FindAsync(username);

            return client is not null ? client : null;
        }

        public async Task CreateClient(ClientCreateDTO request)
        {
            if (_context.Clients.FirstOrDefault(c => c.SSN == request.SSN) != null) throw new Exception("CPF  já cadastrado");
            if (_context.Clients.FirstOrDefault(c => c.Email == request.Email) != null) throw new Exception("Email  já cadastrado");
            var client = new ClientModel
            {
                Id = Guid.NewGuid(),
                AccessInformation = new AccessInformation()
                {
                    Username = request.UserName,
                    Password = request.Password
                },
                Name = request.Name,
                SecurityPhrase = request.SecurityPhrase,
                Email = request.Email,
                SSN = request.SSN,
                NIC = request.NIC,
                PhoneNumber = request.PhoneNumber,
                SocialName = request.SocialName
            };
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClient(Guid id)
        {
            var client = await _context.Clients.FindAsync(id) ?? throw new Exception ("Usuário não encontrado");
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAddress(Guid idClient, AddressUpdateDTO request)

        {
            var client = await _context.Clients.FindAsync(idClient) ?? throw new Exception("Usuário não encontrado");
            client.Address.Country = request.Country;
            client.Address.StateOrProvince = request.StateOrProvince;
            client.Address.CityOrVillage = request.CityOrVillage;
            client.Address.PostalCode = request.PostalCode;
            client.Address.Neighborhood = request.Neighborhood;
            client.Address.Street = request.Street;
            client.Address.Number = request.Number;

            await _context.SaveChangesAsync();

        }

        public async Task UpdateClientBasicData(Guid id, ClientUpdateDTO request)
        {
            var client = await _context.Clients.FindAsync(id) ?? throw new Exception("Usuário não encontrado");
            client.Name = request.Name;
            client.SocialName = request.SocialName;
            client.Email = request.Email;
            client.PhoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync();
        }
    }
}
