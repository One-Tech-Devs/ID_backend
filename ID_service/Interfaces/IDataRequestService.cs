﻿using ID_model.DTOs;
using ID_model.Models;

namespace ID_service.Interfaces
{
    public interface IDataRequestService
    {
        Task<DataRequestModel> CreateDataRequest(DataRequestDTO request);
        Task<DataRequestModel> GetDataRequestById(Guid id);
        Task<List<DataRequestModel>> GetAllDataRequest();
    }
}
