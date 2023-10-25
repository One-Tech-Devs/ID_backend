using ID_model.DTOs;
using ID_model.Models;

namespace ID_service.Interfaces
{
    public interface IDataRequestService
    {
        Task<DataRequestModel> CreateDataRequest(DataRequestDTO request);
        Task<DataRequestModel> GetDataRequestById(Guid id);
        Task<BasicDataRequestInfosDTO> GetDataRequestById(Guid id);
        Task<List<BasicDataRequestInfosDTO>> GetAllDataRequest();
        Task<BasicDataRequestInfosDTO?> ChangeStatusDataRequestById(Guid id, string status);

    }
}
