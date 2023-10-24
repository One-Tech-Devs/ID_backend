using ID_model.DTOs;

namespace ID_service.Interfaces
{
    public interface IClientService
    {
        Task CreateClient(ClientCreateDTO request);
        Task DeleteClient(Guid id);
        Task UpdateClientBasicData(Guid id, ClientUpdateDTO request);
        Task UpdateAddress(Guid idClient, AddressUpdateDTO request);
    }
}
