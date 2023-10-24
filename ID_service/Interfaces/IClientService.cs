using ID_model.DTOs;
using ID_model.Models;

namespace ID_service.Interfaces
{
    public interface IClientService
    {
        Task <ClientModel?> GetClientByUsername(string username);
        Task CreateClient(ClientCreateDTO request);
        Task DeleteClient(Guid id);
        Task UpdateClientBasicData(Guid id, ClientUpdateDTO request);
        Task UpdateAddress(Guid idClient, AddressUpdateDTO request);
    }
}
