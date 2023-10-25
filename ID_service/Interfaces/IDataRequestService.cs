using ID_model.DTOs;
using ID_model.Models;

namespace ID_service.Interfaces
{
    public interface IDataRequestService
    {
        Task<DataRequestModel> CreateDataRequest(DataRequestDTO request);
        Task<BasicDataRequestInfosDTO> GetDataRequestById(Guid id);
        Task<List<BasicDataRequestInfosDTO>> GetAllDataRequest();
        Task<BasicDataRequestInfosDTO?> ChangeStatusDataRequestById(Guid id, string status);
        Task<List<DataRequestModel>> GetDataRequestByStatus(string status);

    }
}
