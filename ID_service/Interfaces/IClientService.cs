using ID_model.DTOs;
using ID_model.Models;
using System.Threading.Tasks;

namespace ID_service.Interfaces
{
    public interface IClientService
    {
        Task<ClientModel?> GetClientByUsername(string username);
        Task<ClientModel?> GetClientBySSN(string ssn);
        Task<ClientModel?> GetClientByEmail(string email);
        Task <ClientModel> CreateClient(ClientCreateDTO request);
        Task DeleteClient(Guid id);
        Task<ClientModel?> UpdateClientBasicData(Guid id, ClientUpdateDTO request);
        Task<ClientModel?> UpdateAddress(Guid idClient, AddressUpdateDTO request);
        Task<List<ClientModel>> GetAllClients();
        Task<ClientModel> GetClientById(Guid clientId);
        Task<DataRequestModel?> UpdateStatusRequestByUsername(string username, string status);
        //Task<DataRequestModel?> AllowDenyRequest(Guid id, string status);
    }
}
