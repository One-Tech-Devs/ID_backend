using ID_model.DTOs;
using ID_model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
