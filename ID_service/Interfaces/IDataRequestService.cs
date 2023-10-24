using ID_model.DTOs;
using ID_model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_service.Interfaces
{
    public interface IDataRequestService
    {
        Task<DataRequestModel> CreateDataRequest(DataRequestDTO request);
        Task<DataRequestModel> GetDataRequestById(Guid id);
        Task<List<DataRequestModel>> GetAllDataRequest();
    }
}
